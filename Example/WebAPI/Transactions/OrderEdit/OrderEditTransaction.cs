using Microsoft.Extensions.Logging;
using System;
using System.Text;
using WebAPI.Repositories;
using Framework.WebAPI.BaseClasses;
using Framework.WebAPI.Interfaces;

namespace WebAPI.Transactions.OrderEdit
{
  public partial class OrderEditTransaction : TransactionBase
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">RepositoryBase用インターフェース</param>
    /// <param name="logger">ロガーインスタンス</param>
    /// <remarks>パラメータはControllerから引き渡される</remarks>
    public OrderEditTransaction(IRepositoryBase repository, ILogger logger) : base(repository, logger)
    {
    }
  }
}
