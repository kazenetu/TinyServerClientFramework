using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebAPI.Transactions.OrderEdit;
using Framework.WebAPI.BaseClasses;
using Framework.WebAPI.Interfaces;

namespace WebAPI.Controllers.OrderEdit
{
  public partial class OrderEditController
  {
    /// <summary>
    /// Modify
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果JSON</returns>
    [HttpPost("modify")]
    public virtual IActionResult Modify([FromBody]ModifyRequest request)
    {
      // システムエラーチェック
      if (systenErrorResult is IActionResult) return systenErrorResult;

      // バージョンチェック
      if (request.TargetVersion != Statics.WebAPIVersion)
      {
        return Json(new ModifyResponse(ModifyResponse.Results.NG, Statics.ErrorMessageUpdate, null));
      }

      // 入力チェック
      if (!request.Validate())
      {
        logger.LogError("Pram[{0}]が未設定", request.ValidateNGPropertyName);
        return Json(new ModifyResponse(ModifyResponse.Results.NG, Statics.ErrorMessageRequired, null));
      }

      var status = ModifyResponse.Results.OK;
      var message = string.Empty;
      ModifyResponse.ModifyResponseParam resultParam = new ModifyResponse.ModifyResponseParam();

      var transaction = new OrderEditTransaction(repository, logger);
      if (transaction.Modify(request))
      {
        resultParam.OrderNo = request.OrderNo;
      }
      else
      {
        status = ModifyResponse.Results.NG;
        message = "保存失敗しました。";
      }

      return Json(new ModifyResponse(status, message, resultParam));
    }
  }
}