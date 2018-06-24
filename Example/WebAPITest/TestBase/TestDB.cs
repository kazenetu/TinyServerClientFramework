using Framework.WebAPI.ConfigModel;
using Framework.WebAPI.DB;
using Framework.WebAPI.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace WebAPITest.TestBase
{
  /// <summary>
  /// テスト用データベース
  /// </summary>
  public class TestDB
  {
    /// <summary>
    /// 設定クラス
    /// </summary>
    private static DatabaseConfigModel Config = null;

    /// <summary>
    /// 設定クラスの取得
    /// </summary>
    /// <returns></returns>
    private static DatabaseConfigModel GetDatabaseConfig()
    {
      if(Config == null)
      {
        // jsonファイルをデシアライズ
        using (StreamReader stream = File.OpenText("appsettings.json"))
        {
          var json = stream.ReadToEnd();
          Config = JsonConvert.DeserializeObject<DatabaseConfigModel>(json);
        }
      }
      return Config;
    }

    /// <summary>
    /// テスト用DBの取得
    /// </summary>
    /// <returns>テスト用DB</returns>
    /// <remarks>sqliteの場合はインメモリで動作</remarks>
    public static IDatabase GetDB()
    {
      var config = GetDatabaseConfig();
      switch (config.Target)
      {
        case nameof(DatabaseFactory.DatabaseTypes.sqlite):
          return  new TestSQLiteDB(@":memory:");
        default:
          return DatabaseFactory.Create(config);
      }
    }
  }
}
