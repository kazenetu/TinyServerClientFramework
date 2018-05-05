using ClientFramework.ConnectLib;
using System;
using System.Deployment.Application;
using static ClientFramework.ConnectLib.HttpConnectLib;

namespace ClientFramework.BaseClasses
{
  /// <summary>
  /// ビジネスクラスのスーパークラス
  /// </summary>
  public abstract class BussinessBase
  {
    /// <summary>
    /// トークン取得済みか否か
    /// </summary>
    private static bool existToken = false;

    /// <summary>
    /// Getメソッド
    /// </summary>
    /// <typeparam name="Response">ResponseBaseを継承したDTOクラス</typeparam>
    /// <param name="webAPIUrl">クエリ付きWebAPIURL</param>
    /// <param name="stubDelegate">スタブ処理用デリゲート</param>
    /// <returns>レスポンス</returns>
    public Response Get<Response>(string webAPIUrl, StubWebAPIDelegate stubDelegate = null) where Response : new()
    {
      return HttpConnectLib.Get<Response>($"{getWebApiRootAddress(true)}{webAPIUrl}", stubDelegate);
    }

    /// <summary>
    /// Postメソッド
    /// </summary>
    /// <typeparam name="Response">ResponseBaseを継承したDTOクラス</typeparam>
    /// <param name="webAPIUrl">クエリ付きWebAPIURL</param>
    /// <param name="param">パラメータ</param>
    /// <param name="stubDelegate">スタブ処理用デリゲート</param>
    /// <returns>レスポンス</returns>
    public Response Post<Response>(string webAPIUrl, object param, StubWebAPIDelegate stubDelegate = null) where Response : new()
    {
      // 一回もトークンを取得していない場合は取得する
      if (!existToken)
      {
        HttpConnectLib.GetToken(getWebApiRootAddress(false));
        existToken = true;
      }
      
      return HttpConnectLib.Post<Response>($"{getWebApiRootAddress(true)}{webAPIUrl}", param, stubDelegate);
    }

    /// <summary>
    /// WebAPIのルートURLを取得
    /// </summary>
    /// <param name="setApiPath">APIパスの追加</param>
    /// <returns>APIのルートURL</returns>
    private string getWebApiRootAddress(bool setApiPath)
    {
      var url = string.Empty;

      if (ApplicationDeployment.IsNetworkDeployed)
      {
        // ClickOnce実行時のURL
        var updateLocation = ApplicationDeployment.CurrentDeployment.UpdateLocation;
        url = string.Format("{0}://{1}:{2}/", updateLocation.Scheme, updateLocation.Host, updateLocation.Port);
      }
      else
      {
        // デバッグ実行時のURL
        var args = Environment.GetCommandLineArgs();
        if (args.Length >= 2)
        {
          url = args[1];
          if (url[url.Length - 1] != '/')
          {
            url += "/";
          }
        }
        else
        {
          url = "http://localhost:5000/";
        }
      }

      // APIパス追加
      if (setApiPath)
      {
        url += "api/";
      }

      return url;
    }

  }
}
