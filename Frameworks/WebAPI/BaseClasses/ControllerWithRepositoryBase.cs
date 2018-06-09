using Framework.DataTransferObject.BaseClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Framework.WebAPI.Interfaces;

namespace Framework.WebAPI.BaseClasses
{
  /// <summary>
  /// ControllerのIRepositoryBase付スーパークラス
  /// </summary>
  public class ControllerWithRepositoryBase : Controller
  {
    #region クラス定数
    /// <summary>
    /// セッション用クッキー名
    /// </summary>
    public static readonly string SessionCookieName = "sid";
    #endregion

    #region インスタンス定数
    /// <summary>
    /// セッションキー：ユーザーID
    /// </summary>
    protected readonly string SessionKeyUserID = "userID";
    #endregion

    #region インスタンスフィールド

    /// <summary>
    /// DIで取得するRepositoryBase用インターフェース
    /// </summary>
    protected IRepositoryBase repository = null;

    /// <summary>
    /// ロガーインスタンス
    /// </summary>
    protected readonly ILogger logger;

    /// <summary>
    /// システムエラー用ActionResult
    /// </summary>
    protected IActionResult systenErrorResult = null;

    #endregion

    #region プロパティ

    /// <summary>
    /// セッション
    /// </summary>
    protected ISession session
    {
      get
      {
        return HttpContext.Session;
      }
    }

    #endregion

    #region メソッド

    #region コンストラクタ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">DIで取得するRepositoryBase用インターフェース</param>
    /// <param name="logger">ロガーインスタンス</param>
    public ControllerWithRepositoryBase(IRepositoryBase repository, ILogger logger)
    {
      this.repository = repository;
      this.logger = logger;

      try
      {
        // DatabaseFactoryから永続化対象のDBインスタンスを取得
        this.repository.Initialize();
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        systenErrorResult = Json(new ResponseBase<object>(ResponseBase<object>.Results.NG, "システムエラーが発生しました。"));
      }
    }
    #endregion

    /// <summary>
    /// セッションキーの文字列を取得
    /// </summary>
    /// <param name="sessionKey">セッションキー</param>
    /// <returns>キーがある場合は対応する文字列、ない場合はnull</returns>
    protected string getSessionString(string sessionKey)
    {
      string result = session.GetString(sessionKey);

      return result;
    }

    /// <summary>
    /// ログインチェック
    /// </summary>
    /// <param name="requestUserID">入力されたユーザーID</param>
    /// <returns>ログイン結果</returns>
    protected bool isLogin(string requestUserID)
    {
      var sessionUserID = getSessionString(SessionKeyUserID);

      if (sessionUserID == requestUserID)
      {
        return true;
      }

#if DEBUG
      return true;
#else
      return false;
#endif
    }

    /// <summary>
    /// セッションのリフレッシュ
    /// </summary>
    protected void refreshSession()
    {
      // セッション破棄
      session.Clear();
    }


    #endregion
  }
}
