using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI.Controllers.V1.OrderEdit;
using WebAPI.Repositories;
using Xunit;

namespace WebAPITest.OrderEdit
{
  public partial class OrderEditTestV1
  {
    /// <summary>
    /// Save用入力データ生成、取得
    /// </summary>
    /// <returns>入力データリスト</returns>
    public static IEnumerable<object[]> MakeSaveRequest()
    {
      var result = new List<object[]>();

      // 入力データリストの追加
      // TODO 入力データとしてSaveRequestを作成してください。(本コメントは削除してください)

      // 未入力:必須項目なしエラー
      result.Add(new object[] { new SaveRequest() });

      // 更新NG:Version違い
      result.Add(new object[] { new SaveRequest() { OrderNo = 1, LoginUserID = "test2", ModVersion = 2 } });

      // 登録:test
      result.Add(new object[] { new SaveRequest() { OrderNo = 10, LoginUserID = "test", ModVersion = 1 } });

      // 更新:test
      result.Add(new object[] { new SaveRequest() { OrderNo = 2, LoginUserID = "test", ModVersion = 1 } });

      return result;
    }

    /// <summary>
    /// Saveのテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeSaveRequest))]
    [Trait("Test", "OrderEditTestV1")]
    public void SaveTestV1(SaveRequest request)
    {
      // テスト用DBインスタンスを取得
      var testDB = GetDB();

      // controller作成
      var controller = new OrderEditController(new OrderEditRepository(testDB), logger);

      // controllerメソッド呼び出し
      var result = controller.Save(request);

      // 戻り値がJSONか確認
      Assert.True(result is JsonResult, "Not JsonResult");

      // ResponseDTOを取得
      var responseObject = ((JsonResult)result).Value as SaveResponse;

      // ResponseDTOのnull確認
      Assert.NotNull(responseObject);

      // 値確認
      var expectedResult = "OK";
      switch (request.OrderNo)
      {
        case 2:
        case 10:
          break;
        default:
          expectedResult = "NG";
          break;
      }

      Assert.True(responseObject.Result == expectedResult,
                $"結果が異なります。期待値:{expectedResult},検索結果:{responseObject.Result}");

      if (expectedResult == "OK")
      {
        Assert.True(string.IsNullOrEmpty(responseObject.ErrorMessage),
                $"エラーメッセージ「{responseObject.ErrorMessage}」が設定されています。");
      }
      else
      {
        Assert.True(!string.IsNullOrEmpty(responseObject.ErrorMessage),
                "エラーメッセージが設定されていません。");
      }

    }
  }
}
