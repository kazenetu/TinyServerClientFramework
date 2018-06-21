using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Framework.WebAPI.BaseClasses;
using Framework.WebAPI.Interfaces;

namespace WebAPI.Controllers.V1.OrderEdit
{
  [Route("api/v1/orderedit")]
  public partial class OrderEditController : ControllerWithRepositoryBase
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">DIで取得するRepositoryBase用インターフェース</param>
    /// <param name="logger">ロガーインスタンス</param>
    public OrderEditController(IRepositoryBase repository, ILogger<OrderEditController> logger) : base(repository, logger)
    {
    }
  }
}