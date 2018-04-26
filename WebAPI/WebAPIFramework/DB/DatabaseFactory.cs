using System;
using System.IO;
using Commons.ConfigModel;
using Commons.Interfaces;

namespace Commons.DB
{
  /// <summary>
  /// DBインスタンス生成クラス
  /// </summary>
  public class DatabaseFactory
  {
    /// <summary>
    /// DBインスタンス取得
    /// </summary>
    /// <returns>IDatabasインスタンス</returns>
    public static IDatabase Create(DatabaseConfigModel config)
    {
      IDatabase result = null;

      // DB種類で作成するインスタンスを変える
      switch (config.Type.ToLower())
      {
        case "sqlite":
          result = new SQLiteDB(config.connectionString);
          break;
        case "postgres":
          result = new PostgreSQLDB(config.connectionString);
          break;
        default:
          result = new SQLiteDB(config.connectionString);
          break;
      }

      return result;
    }
  }
}