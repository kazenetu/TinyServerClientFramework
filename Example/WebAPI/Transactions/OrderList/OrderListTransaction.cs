using Microsoft.Extensions.Logging;
using System;
using System.Text;
using WebAPI.Repositories;
using Framework.WebAPI.BaseClasses;
using Framework.WebAPI.Interfaces;

namespace WebAPI.Transactions.OrderList
{
  public partial class OrderListTransaction : TransactionBase
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">RepositoryBase用インターフェース</param>
    /// <param name="logger">ロガーインスタンス</param>
    /// <remarks>パラメータはControllerから引き渡される</remarks>
    public OrderListTransaction(IRepositoryBase repository, ILogger logger) : base(repository, logger)
    {
    }
  }
}
