using DataTransferObjects.Request.OrderList;
using DataTransferObjects.Response.OrderList;
using DataTransferObjects.Tables;
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
    /// Search
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>処理結果</returns>
    public SearchResponse.SearchResponseParam Search(SearchRequest request)
    {
      var result = new SearchResponse.SearchResponseParam();

      // OrderListRepositoryのインスタンスを取得
      var orderlistRepository = repository.Cast<OrderListRepository>();

      try
      {
        // OrderListRepository経由でSQLを発行
        var orderlistResult = orderlistRepository.Search(request);

        // 発行結果を確認、設定
        // TODO orderlistResultの内容確認とresultへの設定を行ってください。(本コメントは削除してください)

      }
      catch (Exception ex)
      {
        logger.LogError($"{nameof(OrderListTransaction)}.{nameof(Search)}:\"{ex.Message}\"");
      }

      return result;
    }
  }
}
