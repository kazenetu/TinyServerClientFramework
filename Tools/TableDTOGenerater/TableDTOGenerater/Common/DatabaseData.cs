using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using WebAPIFramework.DB;
using WebAPIFramework.Interfaces;
using static WebAPIFramework.DB.DatabaseFactory;

namespace TableDTOGenerater.Common
{
  /// <summary>
  /// データベース情報
  /// </summary>
  /// <remarks>シングルトンクラス</remarks>
  public class DatabaseData
  {
    /// <summary>
    /// インスタンス
    /// </summary>
    private static DatabaseData instance = new DatabaseData();

    /// <summary>
    /// DBごとの接続文字列
    /// </summary>
    private Dictionary<string, string> dbInfos = new Dictionary<string, string>();

    /// <summary>
    /// DTO作成用DBインスタンス
    /// </summary>
    IDatabase db = null;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private DatabaseData()
    {
      dbInfos.Add(nameof(DatabaseTypes.sqlserver).ToUpper(), @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;");
      dbInfos.Add(nameof(DatabaseTypes.postgres).ToUpper(), @"Server=localhost;Port=5432;User Id=test;Password=test;Database=test;");
      dbInfos.Add(nameof(DatabaseTypes.sqlite).ToUpper(), @"..\..\..\..\..\WebAPI\WebAPI\Resource/Test.db");
    }

    /// <summary>
    /// DB名リストを取得
    /// </summary>
    public List<string> DatabaseNames
    {
      get
      {
        return dbInfos.Keys.ToList();
      }
    }

    /// <summary>
    /// DBごとの接続文字列を保存
    /// </summary>
    /// <param name="basePath">実行ファイルのパス</param>
    public void SaveDatabaseConnectionStrings(string basePath)
    {
      var sw = new DataContractJsonSerializer(typeof(Dictionary<string, string>));

      var path = Path.Combine(basePath, "names.txt");
      using (var tw = new StreamWriter(path, false))
      {
        using (var ms = new MemoryStream()) // 書き込み用のストリームを用意し、
        {
          // シリアライザーのWriteObjectメソッドにストリームと対象オブジェクトを渡す
          sw.WriteObject(ms, dbInfos);

          var output = Encoding.UTF8.GetString(ms.ToArray()); // 既定ではUTF-8で出力

          tw.Write(output);
        }
      }
    }

    /// <summary>
    /// DBごとの接続文字列の読み込み
    /// </summary>
    /// <param name="basePath">実行ファイルのパス</param>
    public void LoadDatabaseConnectionStrings(string basePath)
    {
      var path = Path.Combine(basePath, "names.txt");
      if (!File.Exists(path))
      {
        return;
      }
      using (var tr = new StreamReader(path, false))
      {
        dbInfos.Clear();

        var sw = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
        using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(tr.ReadToEnd()), false))
        {
          // シリアライザーのReadObjectメソッドに読み取りストリームを渡す
          dbInfos = sw.ReadObject(ms) as Dictionary<string, string>;
        }
      }
    }

    /// <summary>
    /// 接続文字列設定
    /// </summary>
    /// <param name="dbName"></param>
    /// <param name="connectionString"></param>
    public void SetConnectString(string dbName, string connectionString)
    {
      if (dbInfos.ContainsKey(dbName))
      {
        dbInfos[dbName] = connectionString;
      }
    }

    /// <summary>
    /// 接続文字列取得
    /// </summary>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public string GetConnectString(string dbName)
    {
      if (dbInfos.ContainsKey(dbName))
      {
        return dbInfos[dbName];
      }
      return string.Empty;
    }

    /// <summary>
    /// テーブル・カラム情報を取得
    /// </summary>
    /// <param name="dbName">DB名</param>
    /// <returns>取得結果</returns>
    public List<TableData> GetTables(string dbName)
    {
      var result = new List<TableData>();

      if (db != null)
      {
        db.Dispose();
      }
      db = null;

      // DB名が未設定の場合は終了
      if (!dbInfos.ContainsKey(dbName))
      {
        return result;
      }

      var config = dbInfos[dbName];
      var sql = string.Empty;

      switch (dbName.ToLower())
      {
        case nameof(DatabaseTypes.sqlserver):
          db = new SQLServerDB(config);

          sql = @"select name tname from sys.objects where type = 'U' and is_ms_shipped=0;";
          break;
        case nameof(DatabaseTypes.postgres):
          db = new PostgreSQLDB(config);

          sql = @"select relname tname from pg_stat_user_tables;";
          break;
        case nameof(DatabaseTypes.sqlite):
          db = new SQLiteDB(config);

          sql = @"select name tname from sqlite_master where type = 'table';";
          break;
      }

      // DBが未設定の場合は終了
      if (db == null)
      {
        return result;
      }

      // テーブル名を取得
      var dbResult = db.Fill(sql);
      foreach (DataRow row in dbResult.Rows)
      {
        var originalTableName = row["tname"].ToString();
        var tableName = snakeCase2CamelCase(originalTableName);
        var logicalName = getTableLogicalName(originalTableName, dbName);

        var tableData = new TableData() { TableName = tableName, TableOriginalName = originalTableName, TableLogicalName = logicalName };

        // カラム情報を取得
        tableData.Columns = GetColumn(originalTableName, dbName);

        result.Add(tableData);
      }

      return result;
    }

    /// <summary>
    /// テーブル論理名を取得
    /// </summary>
    /// <param name="tableName">物理テーブル名</param>
    /// <param name="dbName">DB名</param>
    /// <returns>テーブル論理名</returns>
    /// <remarks>未設定・SQLiteの場合は物理テーブル名</remarks>
    private string getTableLogicalName(string tableName, string dbName)
    {
      var sql = new StringBuilder();

      switch (dbName.ToLower())
      {
        case nameof(DatabaseTypes.sqlserver):
          // テーブル名取得SQL
          sql.AppendLine(@"SELECT");
          sql.AppendLine(@"    ep.value AS 'table_comment'");
          sql.AppendLine(@"FROM");
          sql.AppendLine(@"    sys.tables t");
          sql.AppendLine(@"INNER JOIN");
          sql.AppendLine(@"  sys.extended_properties ep");
          sql.AppendLine(@"  ON");
          sql.AppendLine(@"    ep.major_id = t.object_id");
          sql.AppendLine(@"    AND ep.minor_id = 0");
          sql.AppendLine(@"WHERE");
          sql.AppendLine($"  t.name = '{tableName}'");
          sql.AppendLine(@"  and ep.name LIKE 'MS_Description';");
          break;
        case nameof(DatabaseTypes.postgres):
          // テーブル名取得SQL
          sql.AppendLine(@"select");
          sql.AppendLine(@"  pd.description as table_comment");
          sql.AppendLine(@"from");
          sql.AppendLine(@"   pg_stat_user_tables psut");
          sql.AppendLine(@"  ,pg_description pd");
          sql.AppendLine(@"where");
          sql.AppendLine($"  psut.relname='{tableName}'");
          sql.AppendLine(@"  and psut.relid=pd.objoid");
          sql.AppendLine(@"  and pd.objsubid=0;");
          break;
        case nameof(DatabaseTypes.sqlite):
          return tableName;
      }

      var dbResult = db.Fill(sql.ToString());
      foreach (DataRow row in dbResult.Rows)
      {
        var result = row["table_comment"];
        if (result == DBNull.Value || string.IsNullOrEmpty(result.ToString()))
        {
          return tableName;
        }
        return result.ToString();
      }

      return tableName;
    }

    /// <summary>
    /// テーブル検索してカラム情報リストを取得する
    /// </summary>
    /// <param name="tableName">物理テーブル名</param>
    /// <returns></returns>
    public List<TableColumnData> GetColumn(string tableName, string dbName)
    {
      var result = new List<TableColumnData>();

      var colmunLogicalNames = getLogicalColmunName(tableName, dbName);

      var sql = $"select * from {tableName};";

      // PostgreSQLの場合は大文字テーブルを正しく表示できるようにする
      if (dbName.ToLower() == nameof(DatabaseTypes.postgres))
      {
        sql = $"select * from \"{tableName}\";";
      }

      var dbResult = db.Fill(sql);
      foreach (DataColumn column in dbResult.Columns)
      {
        var columnName = snakeCase2CamelCase(column.ColumnName);
        var dataType = column.DataType;
        var columnLogicalName = columnName;
        if (colmunLogicalNames.ContainsKey(column.ColumnName))
        {
          columnLogicalName = colmunLogicalNames[column.ColumnName];
        }

        result.Add(new TableColumnData() { ColumnName = columnName, ColumnOriginalName = column.ColumnName, ColumnType = dataType, ColumnLogicalName = columnLogicalName });
      }

      return result;
    }

    /// <summary>
    /// 物理カラムと論理カラム名のコレクションを取得
    /// </summary>
    /// <param name="tableName">テーブル名</param>
    /// <returns>物理カラムと論理カラム名のコレクション</returns>
    private Dictionary<string, string> getLogicalColmunName(string tableName, string dbName)
    {
      var result = new Dictionary<string, string>();

      var sql = new StringBuilder();
      switch (dbName.ToLower())
      {
        case nameof(DatabaseTypes.sqlserver):
          // カラム名取得SQL
          sql.AppendLine(@"SELECT");
          sql.AppendLine(@"  c.name AS 'column_name',");
          sql.AppendLine(@"  ep.value AS 'column_comment'");
          sql.AppendLine(@"FROM");
          sql.AppendLine(@"  sys.tables t");
          sql.AppendLine(@"INNER JOIN");
          sql.AppendLine(@"  sys.columns c");
          sql.AppendLine(@"  ON");
          sql.AppendLine(@"    c.object_id = t.object_id");
          sql.AppendLine(@"INNER JOIN");
          sql.AppendLine(@"  sys.extended_properties ep");
          sql.AppendLine(@"  ON");
          sql.AppendLine(@"    ep.major_id = c.object_id");
          sql.AppendLine(@"    AND ep.minor_id = c.column_id");
          sql.AppendLine(@"WHERE");
          sql.AppendLine($"  t.name = '{tableName}'");
          sql.AppendLine(@"  and ep.name LIKE 'MS_Description';");
          break;

        case nameof(DatabaseTypes.postgres):
          // カラム名取得SQL
          sql.AppendLine(@"select");
          sql.AppendLine(@"  pa.attname as column_name,");
          sql.AppendLine(@"  pd.description as column_comment");
          sql.AppendLine(@"from");
          sql.AppendLine(@"  pg_stat_all_tables psat");
          sql.AppendLine(@"  ,pg_description     pd");
          sql.AppendLine(@"  ,pg_attribute       pa");
          sql.AppendLine(@"where");
          sql.AppendLine($"  psat.relname='{tableName}'");
          sql.AppendLine(@"  and psat.relid = pd.objoid");
          sql.AppendLine(@"  and pd.objsubid <> 0");
          sql.AppendLine(@"  and pd.objoid=pa.attrelid");
          sql.AppendLine(@"  and pd.objsubid=pa.attnum");
          break;

        case nameof(DatabaseTypes.sqlite):
          return result;
      }

      var dbResult = db.Fill(sql.ToString());
      foreach (DataRow row in dbResult.Rows)
      {
        var logicalName = row["column_comment"].ToString();
        if (!string.IsNullOrEmpty(logicalName))
        {
          result.Add(row["column_name"].ToString(), logicalName);
        }
      }

      return result;
    }

    /// <summary>
    /// スネークケースをアッパーキャメルケースに変換
    /// </summary>
    /// <param name="srcSnakeCase">スネークケースの文字列</param>
    /// <returns>アッパーキャメルケース文字列</returns>
    private string snakeCase2CamelCase(string srcSnakeCase)
    {
      if (srcSnakeCase.Length <= 0) return string.Empty;

      string[] words = srcSnakeCase.Split('_');

      string result = string.Empty;
      for (int i = 0; i < words.Length; i++)
      {
        var word = words[i];
        if (i == 0 || i > 0)
        {
          word = word[0].ToString().ToUpper() + word.Substring(1).ToLower();
        }
        else
        {
          word = word.ToLower();
        }
        result += word;
      }

      return result;
    }

    /// <summary>
    /// インスタンス取得
    /// </summary>
    /// <returns>インスタンス</returns>
    public static DatabaseData GetInstance()
    {
      return instance;
    }

    /// <summary>
    /// テーブル情報
    /// </summary>
    public class TableData
    {
      /// <summary>
      /// クラス名用テーブル名
      /// </summary>
      /// <remarks>アッパーキャメルケース</remarks>
      public string TableName { set; get; }

      /// <summary>
      /// DB用テーブル名
      /// </summary>
      /// <remarks>スネークケース</remarks>
      public string TableOriginalName { set; get; }

      /// <summary>
      /// DB用論理テーブル名
      /// </summary>
      public string TableLogicalName { set; get; }

      /// <summary>
      /// カラム情報リスト
      /// </summary>
      public List<TableColumnData> Columns { set; get; } = null;

      /// <summary>
      /// 詳細情報出力
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
        var sb = new StringBuilder();

        // テーブル情報出力
        sb.AppendLine($"{TableName}[{TableOriginalName}] : {TableLogicalName}");

        // カラム情報出力
        if (Columns != null)
        {
          foreach (var column in Columns)
          {
            sb.AppendLine(column.ToString());
          }
        }

        return sb.ToString();
      }
    }

    /// <summary>
    /// テーブルカラム
    /// </summary>
    public class TableColumnData
    {
      /// <summary>
      /// クラス名用カラム名
      /// </summary>
      /// <remarks>アッパーキャメルケース</remarks>
      public string ColumnName { set; get; }

      /// <summary>
      /// DB用カラム名
      /// </summary>
      /// <remarks>スネークケース</remarks>
      public string ColumnOriginalName { set; get; }

      /// <summary>
      /// DB用論理カラム名
      /// </summary>
      public string ColumnLogicalName { set; get; }

      /// <summary>
      /// クラス用Type
      /// </summary>
      public Type ColumnType { set; get; }

      /// <summary>
      /// カラムType名を返す
      /// </summary>
      /// <returns></returns>
      public string GetColumnTypeName()
      {
        switch (ColumnType.Name)
        {
          case nameof(String):
            return "string";
          case nameof(Int32):
            return "int";
          case nameof(Int64):
            return "long";
          case nameof(Double):
            return "double";
          case nameof(Decimal):
            return "decimal";
          case nameof(Single):
            return "float";
          case nameof(Boolean):
            return "bool";
        }
        return ColumnType.Name;
      }

      /// <summary>
      /// 詳細情報出力
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
        return $" > {ColumnName}[{ColumnOriginalName}] : {ColumnType.Name} : {ColumnLogicalName}";
      }
    }

  }
}
