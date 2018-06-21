using DataTransferObjects.Request.OrderList;
using DataTransferObjects.Response.OrderList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebAPI.Transactions.OrderList;

namespace WebAPI.Controllers.OrderList
{
  public partial class OrderListController
  {
    /// <summary>
    /// Search
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果JSON</returns>
    [HttpPost("search")]
    public virtual IActionResult Search([FromBody]SearchRequest request)
    {
      // システムエラーチェック
      if (systenErrorResult is IActionResult) return systenErrorResult;

      // 入力チェック
      if (!request.Validate())
      {
        logger.LogError("Pram[{0}]が未設定", request.ValidateNGPropertyName);
        return Json(new SearchResponse(SearchResponse.Results.NG, "未入力項目があります。", null));
      }

      var status = SearchResponse.Results.OK;
      var message = string.Empty;
      SearchResponse.SearchResponseParam resultParam = null;

      var transaction = new OrderListTransaction(repository, logger);
      resultParam = transaction.Search(request);

      if (!resultParam.List.Any())
      {
        message = "検索件数がゼロ件です。";
      }

      return Json(new SearchResponse(status, message, resultParam));
    }
  }
}