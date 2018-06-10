using System;
using Framework.WebAPI.ConfigModel;
using Framework.WebAPI.Interfaces;

namespace Framework.WebAPI.DB
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

      var targetDB = config.Target.ToLower();

      if (!config.ConnectionStrings.ContainsKey(targetDB))
      {
        throw new Exception($"{targetDB}の接続文字列が定義されていません。");
      }

      var connectionString = config.ConnectionStrings[targetDB];

      // DB種類で作成するインスタンスを変える
      switch (targetDB)
      {
        case nameof(DatabaseTypes.sqlite):
          result = new SQLiteDB(connectionString);
          break;
        case nameof(DatabaseTypes.postgres):
          result = new PostgreSQLDB(connectionString);
          break;
        case nameof(DatabaseTypes.sqlserver):
          result = new SQLServerDB(connectionString);
          break;
        default:
          result = new SQLiteDB(connectionString);
          break;
      }

      return result;
    }
  }
}