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
    /// FindUserName
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果JSON</returns>
    [HttpPost("findusername")]
    public virtual IActionResult FindUserName([FromBody]FindUserNameRequest request)
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

      // 注文者名が存在しない場合はエラー
      if (string.IsNullOrEmpty(resultParam.OrderUserName))
      {
        status = FindUserNameResponse.Results.NG;
        message = "注文者IDはありません。";
      }

      return Json(new FindUserNameResponse(status, message, resultParam));
    }
  }
}