﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;

namespace Framework.Client.ConnectLib
{
  /// <summary>
  /// WebAPI問い合わせ用クラス
  /// </summary>
  public class HttpConnectLib
  {
    /// <summary>
    /// Cookieヘッダー
    /// </summary>
    private const string HeaderSetCookie = "Set-Cookie";

    /// <summary>
    /// Cookie情報
    /// </summary>
    private static CookieContainer Cookies = new CookieContainer();

    /// <summary>
    /// Basic認証情報
    /// </summary>
    private static NetworkCredential BasicCertification = null;

    /// <summary>
    /// スタブ用デリゲート
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="request">入力データ</param>
    /// <returns>スタブデータ</returns>
    public delegate object StubWebAPIDelegate(string url,object request);

    /// <summary>
    /// 自己証明書のWebAPIアクセスを許可する
    /// </summary>
    public static void UseSelfSignedCertificate()
    {
      ServicePointManager.ServerCertificateValidationCallback =
        new RemoteCertificateValidationCallback(
          (sender, certificate, chain, sslPolicyErrors) => {
            return true; 
          });
    }

    /// <summary>
    /// Basic認証の設定
    /// </summary>
    /// <param name="userName">ユーザー名</param>
    /// <param name="password">パスワード</param>
    public static void UseBasicCertification(string userName, string password)
    {
      BasicCertification = new NetworkCredential(userName, password);
    }

    /// <summary>
    /// Getメソッド
    /// </summary>
    /// <param name="url">クエリ付きURL</param>
    /// <param name="stubDelegate">スタブ処理用デリゲート</param>
    /// <returns>レスポンス</returns>
    public static T Get<T>(string url, StubWebAPIDelegate stubDelegate = null) where T:new()
    {
      if (stubDelegate != null)
      {
        var responseData = (new T()) as object;
        return (T)stubDelegate(url, responseData);
      }

      string result = string.Empty;
      HttpWebRequest req = GetHttpWebRequest(url);
      HttpWebResponse res = null;
      Stream resStream = null;
      StreamReader sr = null;

      try
      {
        SetCookie(url, req);

        // レスポンスの取得と読み込み
        res = (HttpWebResponse)req.GetResponse();

        UpdateCookie(url, res.Headers);

        resStream = res.GetResponseStream();
        sr = new StreamReader(resStream, Encoding.UTF8);
        result = sr.ReadToEnd();
      }
      catch
      {
        throw;
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
#if !STUB
      var req = GetHttpWebRequest(url);

      // レスポンスの取得と読み込み
      var res = (HttpWebResponse)req.GetResponse();

      UpdateCookie(url, res.Headers);
#endif
    }

    /// <summary>
    /// POSTメソッド
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="param">パラメータ</param>
    /// <param name="stubDelegate">スタブ処理用デリゲート</param>
    /// <returns>レスポンス</returns>
    public static T Post<T>(string url, object param, StubWebAPIDelegate stubDelegate = null) where T : new()
    {
      if (stubDelegate != null)
      {
        var responseData = (new T()) as object;
        return (T)stubDelegate(url, responseData);
      }

      string result = string.Empty;
      HttpWebRequest req = GetHttpWebRequest(url);
      Stream paramStream = null;
      Stream resStream = null;
      StreamReader sr = null;

      try
      {
        req.Method = "POST";
        req.ContentType = "application/json";
        req.AllowWriteStreamBuffering = true;

        SetCookie(url, req);

        var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(param));
        paramStream = req.GetRequestStream();
        paramStream.Write(data, 0, data.Length);
        paramStream.Close();
        paramStream = null;

        // レスポンスの取得と読み込み
        var res = (HttpWebResponse)req.GetResponse();

        UpdateCookie(url, res.Headers);

        resStream = res.GetResponseStream();
        sr = new StreamReader(resStream, Encoding.UTF8);
        result = sr.ReadToEnd();
      }
      catch
      {
        throw;
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
    /// HttpWebRequestインスタンス取得
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>HttpWebRequestインスタンス</returns>
    /// <remarks>必要に応じてBasic認証を設定</remarks>
    private static HttpWebRequest GetHttpWebRequest(string url)
    {
      var request = (HttpWebRequest)WebRequest.Create(url);

      // Basic認証
      if(BasicCertification != null)
      {
        request.Credentials = BasicCertification;
      }

      return request;
    }

    /// <summary>
    /// cookie情報の更新
    /// </summary>
    /// <param name="url">対象URL</param>
    /// <param name="headers">ヘッダーコレクション</param>
    private static void UpdateCookie(string url, WebHeaderCollection headers)
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
        Cookies.Add(baseUri, cookie);
      }
    }

    /// <summary>
    /// cookie情報をRequestに設定
    /// </summary>
    /// <param name="url">対象URL</param>
    /// <param name="request">Requestインスタンス</param>
    private static void SetCookie(string url, HttpWebRequest request)
    {
      var baseUri = new Uri(url);

      // HACK マルチテナント対応
      if (baseUri.PathAndQuery.Length > 1)
      {
        baseUri = new Uri(baseUri.OriginalString.Replace(baseUri.PathAndQuery, string.Empty));
      }

      var targetCookies = Cookies.GetCookies(baseUri);
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
