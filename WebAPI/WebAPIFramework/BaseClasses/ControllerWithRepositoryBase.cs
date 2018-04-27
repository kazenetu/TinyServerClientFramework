using Microsoft.AspNetCore.Mvc;
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
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">DIで取得するRepositoryBase用インターフェース</param>
    public ControllerWithRepositoryBase(IRepositoryBase repository)
    {
      this.repository = repository;
    }
  }
}
