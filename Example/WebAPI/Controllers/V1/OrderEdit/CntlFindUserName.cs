using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebAPI.Transactions.OrderEdit;
using Framework.WebAPI.BaseClasses;
using Framework.WebAPI.Interfaces;

namespace WebAPI.Controllers.V1.OrderEdit
{
  public partial class OrderEditController
  {
    /// <summary>
    /// FindUserName
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果JSON</returns>
    [HttpPost("findusername")]
    public virtual IActionResult FindUserName(FindUserNameRequest request)
    {
      // システムエラーチェック
      if (systenErrorResult is IActionResult) return systenErrorResult;

      // 入力チェック
      if (!request.Validate())
      {
        logger.LogError("Pram[{0}]が未設定", request.ValidateNGPropertyName);
        return Json(new FindUserNameResponse(FindUserNameResponse.Results.NG, "未入力項目があります。", null));
      }

      var status = FindUserNameResponse.Results.OK;
      var message = string.Empty;
      FindUserNameResponse.FindUserNameResponseParam resultParam = null;

      var transaction = new OrderEditTransaction(repository, logger);
      resultParam = transaction.FindUserName(request);

      // TODO resultParamのエラー条件を実装してください。(本コメントは削除してください)
      if (false)
      {
        status = FindUserNameResponse.Results.NG;

       // TODO エラーメッセージを設定してください。(本コメントは削除してください)
        message = "メッセージ";
      }

      return Json(new FindUserNameResponse(status, message, resultParam));
    }
  }
}