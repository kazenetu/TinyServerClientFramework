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
    /// Initialize
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>処理結果</returns>
    public InitializeResponse.InitializeResponseParam Initialize(InitializeRequest request)
    {
      var result = new InitializeResponse.InitializeResponseParam();

      // OrderEditRepositoryのインスタンスを取得
      var ordereditRepository = repository.Cast<OrderEditRepository>();

      try
      {
        // OrderEditRepository経由でSQLを発行
        var ordereditResult = ordereditRepository.Initialize(request);

        // 発行結果を確認、設定
        if (ordereditResult.Any())
        {
          var targetData = ordereditResult.First();
          result.OrderUserID = targetData.OrderUserId;
          result.ModVersion = targetData.ModVersion ?? Statics.ModVersionInitValue;
        }

      }
      catch (Exception ex)
      {
        logger.LogError($"{nameof(OrderEditTransaction)}.{nameof(Initialize)}:\"{ex.Message}\"");
      }

      return result;
    }
  }
}
