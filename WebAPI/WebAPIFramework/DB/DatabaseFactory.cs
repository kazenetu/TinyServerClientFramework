using Commons.ConfigModel;
using Commons.Interfaces;
using WebAPIFramework.DB;

namespace Commons.DB
{
  /// <summary>
  /// DBインスタンス生成クラス
  /// </summary>
  public class DatabaseFactory
  {
    /// <summary>
    /// 接続DB種別
    /// </summary>
    public enum DatabaseTypes
    {
      sqlite,
      postgres,
      sqlserver
    }

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
        case nameof(DatabaseTypes.sqlite):
          result = new SQLiteDB(config.connectionString);
          break;
        case nameof(DatabaseTypes.postgres):
          result = new PostgreSQLDB(config.connectionString);
          break;
        case nameof(DatabaseTypes.sqlserver):
          result = new SQLServerDB(config.connectionString);
          break;
        default:
          result = new SQLiteDB(config.connectionString);
          break;
      }

      return result;
    }
  }
}