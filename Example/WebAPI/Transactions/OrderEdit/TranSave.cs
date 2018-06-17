using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Tables;
using Framework.WebAPI.BaseClasses;
using Microsoft.Extensions.Logging;
using System;
using WebAPI.Repositories;

namespace WebAPI.Transactions.OrderEdit
{
  public partial class OrderEditTransaction : TransactionBase
  {
    /// <summary>
    /// Save
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>処理結果</returns>
    public bool Save(SaveRequest request)
    {
      var result = false;

      // トランザクション設定
      repository.BeginTransaction();

      try
      {
        // 登録情報を設定する
        var saveData = new TOrder();
        saveData.OrderNo = request.OrderNo;
        saveData.OrderUserId = request.OrderUserID;
        saveData.ModVersion = request.ModVersion;

        // OrderEditRepositoryのインスタンスを取得
        var ordereditRepository = repository.Cast<OrderEditRepository>();

        // OrderEditRepository経由でSQLを発行
        var ordereditResult = ordereditRepository.Save(saveData);

        // 発行結果を確認
        if(ordereditResult)
        {
          // コミット
          repository.Commit();

          // 戻り値を完了に設定
          result = true;
        }
        else{
          // ロールバック
          repository.Rollback();
        }
      }
      catch (Exception ex)
      {
        // 一意制約違反などの場合はログ出力
        logger.LogError($"{nameof(OrderEditTransaction)}.{nameof(Save)}:\"{ex.Message}\"");

        // ロールバック
        repository.Rollback();
      }

      return result;
    }
  }
}
