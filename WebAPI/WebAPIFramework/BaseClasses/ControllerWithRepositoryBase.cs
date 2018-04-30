using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPIFramework.Interfaces;

namespace WebAPIFramework.BaseClasses
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
    
    #endregion

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">DIで取得するRepositoryBase用インターフェース</param>
    /// <param name="logger">ロガーインスタンス</param>
    public ControllerWithRepositoryBase(IRepositoryBase repository, ILogger logger)
    {
      this.repository = repository;
      this.logger = logger;
    }
  }
}
