using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ClientFramework.ConnectLib
{
  public class HttpConnectLib
  {
    private const string HeaderSetCookie = "Set-Cookie";

    private static CookieContainer cookies = new CookieContainer();

    /// <summary>
    /// Getメソッド
    /// </summary>
    /// <param name="url">クエリ付きURL</param>
    /// <returns>レスポンス</returns>
    public static T Get<T>(string url)
    {
      string result = string.Empty;
      HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
      HttpWebResponse res = null;
      Stream resStream = null;
      StreamReader sr = null;

      try
      {
        setCookie(url, req);

        // レスポンスの取得と読み込み
        res = (HttpWebResponse)req.GetResponse();

        updateCookie(url, res.Headers);

        resStream = res.GetResponseStream();
        sr = new StreamReader(resStream, Encoding.UTF8);
        result = sr.ReadToEnd();
      }
      catch(Exception e)
      {
        throw e;
      }
      finally
      {
        if (sr != null) sr.Close();
        if (resStream != null) resStream.Close();
      }

      return JsonConvert.DeserializeObject<T>(result);
    }

    /// <summary>
    /// GetTokenメソッド
    /// </summary>
    /// <param name="url">クエリ付きURL</param>
    /// <returns>レスポンス</returns>
    public static void GetToken(string url)
    {
      var req = (HttpWebRequest)WebRequest.Create(url);

      // レスポンスの取得と読み込み
      var res = (HttpWebResponse)req.GetResponse();

      updateCookie(url, res.Headers);
    }

    /// <summary>
    /// POSTメソッド
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="param">パラメータ</param>
    /// <returns>レスポンス</returns>
    public static T Post<T>(string url, object param)
    {
      string result = string.Empty;
      HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
      Stream paramStream = null;
      Stream resStream = null;
      StreamReader sr = null;

      try
      {
        req.Method = "POST";
        req.ContentType = "application/json";
        req.AllowWriteStreamBuffering = true;

        setCookie(url, req);

        var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(param));
        paramStream = req.GetRequestStream();
        paramStream.Write(data, 0, data.Length);
        paramStream.Close();
        paramStream = null;

        // レスポンスの取得と読み込み
        var res = (HttpWebResponse)req.GetResponse();

        updateCookie(url, res.Headers);

        resStream = res.GetResponseStream();
        sr = new StreamReader(resStream, Encoding.UTF8);
        result = sr.ReadToEnd();
      }
      catch(Exception e)
      {
        throw e;
      }
      finally
      {
        if (paramStream != null) paramStream.Close();
        if (sr != null) sr.Close();
        if (resStream != null) resStream.Close();
      }

      return JsonConvert.DeserializeObject<T>(result);
    }

    /// <summary>
    /// cookie情報の更新
    /// </summary>
    /// <param name="url">対象URL</param>
    /// <param name="headers">ヘッダーコレクション</param>
    private static void updateCookie(string url, WebHeaderCollection headers)
    {
      var baseUri = new Uri(url);

      // HACK マルチテナント対応
      if (baseUri.AbsolutePath.Length > 1)
      {
        baseUri = new Uri(baseUri.AbsoluteUri.Replace(baseUri.AbsolutePath, string.Empty));
      }

      // Cookieが設定されていない場合は終了
      if (string.IsNullOrEmpty(headers.Get(HeaderSetCookie)))
      {
        return;
      }

      var tempCookies = new CookieContainer();
      tempCookies.SetCookies(baseUri, headers[HeaderSetCookie]);

      foreach (Cookie cookie in tempCookies.GetCookies(baseUri))
      {
        cookies.Add(baseUri, cookie);
        System.Diagnostics.Debug.WriteLine($"{cookie.Name} : {cookie.Value}");
      }
    }

    /// <summary>
    /// cookie情報をRequestに設定
    /// </summary>
    /// <param name="url">対象URL</param>
    /// <param name="request">Requestインスタンス</param>
    private static void setCookie(string url, HttpWebRequest request)
    {
      var baseUri = new Uri(url);

      // HACK マルチテナント対応
      if (baseUri.AbsoluteUri.Length > 1)
      {
        baseUri = new Uri(baseUri.AbsoluteUri.Replace(baseUri.AbsolutePath, string.Empty));
      }

      var targetCookies = cookies.GetCookies(baseUri);
      request.CookieContainer = new CookieContainer();
      request.CookieContainer.Add(targetCookies);

      // XSRF-TOKENを設定
      if (targetCookies["XSRF-TOKEN"] != null)
      {
        request.Headers.Add("X-XSRF-TOKEN", targetCookies["XSRF-TOKEN"].Value);
      }
    }

  }
}
