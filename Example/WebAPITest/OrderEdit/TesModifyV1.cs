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
    /// Modify用入力データ生成、取得
    /// </summary>
    /// <returns>入力データリスト</returns>
    public static IEnumerable<object[]> MakeModifyRequest()
    {
      var result = new List<object[]>();

      // 入力データリストの追加

      // 未入力:必須項目なしエラー
      result.Add(new object[] { new ModifyRequest() });

      // NG:更新Version違い
      result.Add(new object[] { new ModifyRequest() { OrderNo = 1, OrderUserID = "test2", ModVersion = 2 } });

      // NG:更新対象外
      result.Add(new object[] { new ModifyRequest() { OrderNo = 0, OrderUserID = "test", ModVersion = 1 } });

      // 更新:test
      result.Add(new object[] { new ModifyRequest() { OrderNo = 2, OrderUserID = "test", ModVersion = 1 } });

      return result;
    }

    /// <summary>
    /// Modifyのテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeModifyRequest))]
    [Trait("Test", "OrderEditTestV1")]
    public void ModifyTestV1(ModifyRequest request)
    {
      // テスト用DBインスタンスを取得
      var testDB = GetDB();

      // controller作成
      var controller = new OrderEditController(new OrderEditRepository(testDB), logger);

      // controllerメソッド呼び出し
      var result = controller.Modify(request);

      // 戻り値がJSONか確認
      Assert.True(result is JsonResult, "Not JsonResult");

      // ResponseDTOを取得
      var responseObject = ((JsonResult)result).Value as ModifyResponse;

      // ResponseDTOのnull確認
      Assert.NotNull(responseObject);

      // 値確認
      var expectedResult = "OK";
      switch (request.OrderNo)
      {
        case 2:
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
