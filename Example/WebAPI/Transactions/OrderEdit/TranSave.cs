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
    public int Save(SaveRequest request)
    {
      var result = 0;

      // トランザクション設定
      repository.BeginTransaction();

      try
      {
        // すでに注文番号が設定している場合は0を返す
        if(request.OrderNo > 0)
        {
          return 0;
        }

        // 登録情報を設定する
        var saveData = new TOrder();
        saveData.OrderUserId = request.OrderUserID;
        saveData.ModVersion = request.ModVersion;

        // OrderEditRepositoryのインスタンスを取得
        var ordereditRepository = repository.Cast<OrderEditRepository>();

        // OrderNoをMAX+1に設定
        saveData.OrderNo = ordereditRepository.GetNextOrderNo();
        if(saveData.OrderNo <= 0)
        {
          // 取得に失敗した場合は0を返す
          return 0;
        }

        // OrderEditRepository経由でSQLを発行
        var ordereditResult = ordereditRepository.Save(saveData);

        // 発行結果を確認
        if(ordereditResult)
        {
          // コミット
          repository.Commit();

          // 戻り値を完了として採番した注文番号を返す
          result = saveData.OrderNo ?? 0;
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
