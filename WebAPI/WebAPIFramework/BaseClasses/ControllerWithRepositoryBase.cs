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
    /// <summary>
    /// DIで取得するRepositoryBase用インターフェース
    /// </summary>
    protected IRepositoryBase repository = null;

    /// <summary>
    /// ロガーインスタンス
    /// </summary>
    protected readonly ILogger logger;

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
