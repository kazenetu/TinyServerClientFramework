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

      // DB名が未設定の場合は終了
      if (!dbInfos.ContainsKey(dbName))
      {
        return result;
      }

      var config = dbInfos[dbName];
      IDatabase db = null;
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
        result.Add(row["tname"].ToString());
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
  }
}
