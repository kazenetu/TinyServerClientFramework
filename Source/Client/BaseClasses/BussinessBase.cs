using Framework.Client.ConnectLib;
using System.IO;
using System.Reflection;
using static Framework.Client.ConnectLib.HttpConnectLib;

namespace Framework.Client.BaseClasses
{
  /// <summary>
  /// ビジネスクラスのスーパークラス
  /// </summary>
  public abstract class BussinessBase
  {
    /// <summary>
    /// WebAPI通信不可メッセージ
    /// </summary>
    private const string DisconnectionWebAPI = @"{
                                              ""result"":""NG"",
                                              ""errorMessage"":""通信できませんでした""
                                              }";

    /// <summary>
    /// トークン取得済みか否か
    /// </summary>
    private static bool existToken = false;

    /// <summary>
    /// WebAPIのベースURL
    /// </summary>
    public static string WebAPIBaseUrl { set; get; } = string.Empty;

    /// <summary>
    /// Getメソッド
    /// </summary>
    /// <typeparam name="Response">ResponseBaseを継承したDTOクラス</typeparam>
    /// <param name="webAPIUrl">クエリ付きWebAPIURL</param>
    /// <param name="stubDelegate">スタブ処理用デリゲート</param>
    /// <returns>レスポンス</returns>
    protected Response Get<Response>(string webAPIUrl, StubWebAPIDelegate stubDelegate = null) where Response : new()
    {
      try
      {
        return HttpConnectLib.Get<Response>($"{getWebApiRootAddress(true)}{webAPIUrl}", stubDelegate);
      }
      catch
      {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(DisconnectionWebAPI);
      }
    }

    /// <summary>
    /// Postメソッド
    /// </summary>
    /// <typeparam name="Response">ResponseBaseを継承したDTOクラス</typeparam>
    /// <param name="webAPIUrl">クエリ付きWebAPIURL</param>
    /// <param name="param">パラメータ</param>
    /// <param name="stubDelegate">スタブ処理用デリゲート</param>
    /// <returns>レスポンス</returns>
    protected Response Post<Response>(string webAPIUrl, object param, StubWebAPIDelegate stubDelegate = null) where Response : new()
    {
      try
      {
        // 一回もトークンを取得していない場合は取得する
        if (!existToken && stubDelegate == null)
        {
          HttpConnectLib.GetToken(getWebApiRootAddress(false));
          existToken = true;
        }

        return HttpConnectLib.Post<Response>($"{getWebApiRootAddress(true)}{webAPIUrl}", param, stubDelegate);
      }
      catch
      {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(DisconnectionWebAPI);
      }
    }

    /// <summary>
    /// WebAPIのルートURLを取得
    /// </summary>
    /// <param name="setApiPath">APIパスの追加</param>
    /// <returns>APIのルートURL</returns>
    private string getWebApiRootAddress(bool setApiPath)
    {
      var url = WebAPIBaseUrl;

      if (url == string.Empty)
      {
        var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().Name);
        var fullPath = Path.Combine(appDir, "WebApiURL.txt");

        // ファイルが存在する場合はファイル参照する
        if (File.Exists(fullPath))
        {
          using (var reader = new StreamReader(fullPath))
          {
            url = reader.ReadLine();
          }
        }
      }

      if (url == string.Empty)
      {
        // コマンドライン取得
        var args = System.Environment.GetCommandLineArgs();
        if (args.Length > 1)
        {
          // コマンドラインからURL取得
          url = args[1];
        }
        else
        {
          // デバッグ実行時のURL
          url = "http://localhost:5000/";
        }
      }

      // APIパス追加
      if (setApiPath)
      {
        url += "api/";
      }

      // WebAPIのベースURLが未設定の場合は設定する
      if(WebAPIBaseUrl == string.Empty)
      {
        WebAPIBaseUrl = url;
      }

      return url;
    }

  }
}
