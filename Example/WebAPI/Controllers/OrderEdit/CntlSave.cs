using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Transactions.OrderEdit;

namespace WebAPI.Controllers.OrderEdit
{
  public partial class OrderEditController
  {
    /// <summary>
    /// Save
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果JSON</returns>
    [HttpPost("save")]
    public virtual IActionResult Save([FromBody]SaveRequest request)
    {
      // システムエラーチェック
      if (systenErrorResult is IActionResult) return systenErrorResult;

      // 入力チェック
      if (!request.Validate())
      {
        logger.LogError("Pram[{0}]が未設定", request.ValidateNGPropertyName);
        return Json(new SaveResponse(SaveResponse.Results.NG, "未入力項目があります。", null));
      }

      var status = SaveResponse.Results.OK;
      var message = string.Empty;
      SaveResponse.SaveResponseParam resultParam = new SaveResponse.SaveResponseParam();

      var transaction = new OrderEditTransaction(repository, logger);
      resultParam.OrderNo = transaction.Save(request);
      if (resultParam.OrderNo <= 0)
      {
        status = SaveResponse.Results.NG;
        message = "保存失敗しました。";
      }

      return Json(new SaveResponse(status, message, resultParam));
    }
  }
}