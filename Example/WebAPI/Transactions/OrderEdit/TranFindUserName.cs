using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using DataTransferObjects.Tables;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using WebAPI.Repositories;
using Framework.WebAPI.BaseClasses;
using Framework.WebAPI.Interfaces;
using System.Linq;

namespace WebAPI.Transactions.OrderEdit
{
  public partial class OrderEditTransaction : TransactionBase
  {
    /// <summary>
    /// FindUserName
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>処理結果</returns>
    public FindUserNameResponse.FindUserNameResponseParam FindUserName(FindUserNameRequest request)
    {
      var result = new FindUserNameResponse.FindUserNameResponseParam();
      result.OrderUserName = string.Empty;

      // OrderEditRepositoryのインスタンスを取得
      var ordereditRepository = repository.Cast<OrderEditRepository>();

      try
      {
        // OrderEditRepository経由でSQLを発行
        var ordereditResult = ordereditRepository.FindUserName(request);

        // 発行結果を確認、設定
        if (ordereditResult.Any())
        {
          result.OrderUserName = ordereditResult.First().UserName;
        }

      }
      catch (Exception ex)
      {
        logger.LogError($"{nameof(OrderEditTransaction)}.{nameof(FindUserName)}:\"{ex.Message}\"");
      }

      return result;
    }
  }
}
