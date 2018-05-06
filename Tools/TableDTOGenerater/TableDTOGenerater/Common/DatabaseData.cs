using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIFramework.DB;
using WebAPIFramework.Interfaces;
using static WebAPIFramework.DB.DatabaseFactory;

namespace TableDTOGenerater.Common
{
  /// <summary>
  /// データベース情報 シングルトンクラス
  /// </summary>
  public class DatabaseData
  {
    private static DatabaseData instance = new DatabaseData();

    private Dictionary<string, string> dbInfos =new Dictionary<string, string>();

    IDatabase db = null;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private DatabaseData()
    {
      dbInfos.Add(nameof(DatabaseTypes.sqlserver).ToUpper(), @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;");
      dbInfos.Add(nameof(DatabaseTypes.postgres).ToUpper(), @"Server=localhost;Port=5432;User Id=test;Password=test;Database=test;");
      dbInfos.Add(nameof(DatabaseTypes.sqlite).ToUpper(), @"Resource/Test.db");
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
    /// 接続文字列設定
    /// </summary>
    /// <param name="dbName"></param>
    /// <param name="connectionString"></param>
    public void SetConnectString(string dbName,string connectionString)
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

    public List<string> GetTables(string dbName)
    {
      var result = new List<string>();

      if(db != null)
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
      if(db == null)
      {
        return result;
      }

      var dbResult = db.Fill(sql);
      foreach(DataRow row in dbResult.Rows)
      {
        var originalTableName = row["tname"].ToString();
        var tableName = snakeCase2CamelCase(originalTableName);

        var tableData = new TableData() {TableName=tableName,TableOriginalName=originalTableName };
        tableData.Columns = GetColumn(originalTableName, db);

        result.Add(tableData.ToString());
      }

      return result;
    }

    /// <summary>
    /// テーブル検索してカラム情報リストを取得する
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="db"></param>
    /// <returns></returns>
    public List<TableColumnData> GetColumn(string tableName,IDatabase db)
    {
      var result = new List<TableColumnData>();

      var sql = $"select * from {tableName};";
      var dbResult = db.Fill(sql);
      foreach(DataColumn column in dbResult.Columns)
      {
        var columnName = snakeCase2CamelCase(column.ColumnName);
        var dataType = column.DataType;
        result.Add(new TableColumnData() { ColumnName = columnName, ColumnOriginalName = column.ColumnName, ColumnType = dataType });
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
        sb.AppendLine($"{TableName}[{TableOriginalName}]");

        // カラム情報出力
        if(Columns != null)
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
      /// クラス用Type
      /// </summary>
      public Type ColumnType { set; get; }

      /// <summary>
      /// 詳細情報出力
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
        return $" > {ColumnName}[{ColumnOriginalName}] : {ColumnType.Name}";
      }
    }
  }
}
