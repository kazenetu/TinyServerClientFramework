﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Framework.WebAPI.BaseClasses;
using Framework.WebAPI.Interfaces;

namespace WebAPI.Controllers.OrderList
{
  [Route("api/orderlist")]
  public partial class OrderListController : ControllerWithRepositoryBase
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">DIで取得するRepositoryBase用インターフェース</param>
    /// <param name="logger">ロガーインスタンス</param>
    public OrderListController(IRepositoryBase repository, ILogger<OrderListController> logger) : base(repository, logger)
    {
    }
  }
}