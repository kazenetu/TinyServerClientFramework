using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI;
using WebAPI.Controllers.OrderEdit;
using WebAPI.Repositories;
using Xunit;

namespace WebAPITest.OrderEdit
{
  public partial class OrderEditTest
  {
    /// <summary>
    /// Initialize用入力データ生成、取得
    /// </summary>
    /// <returns>入力データリスト</returns>
    public static IEnumerable<object[]> MakeInitializeRequest()
    {
      var result = new List<object[]>();

      // 入力データリストの追加

      // 未入力:データなしエラー
      result.Add(new object[] { new InitializeRequest() });

      // 注文NO 1:NG:バージョンなし:データあり:test
      result.Add(new object[] { new InitializeRequest() { OrderNo = 1 } });

      // 注文NO 1:データあり:test
      result.Add(new object[] { new InitializeRequest() { TargetVersion = Statics.WebAPIVersion, OrderNo = 1 } });
      
      // 注文NO 10:データなし
      result.Add(new object[] { new InitializeRequest() { OrderNo = 10 } });

      return result;
    }

    /// <summary>
    /// Initializeのテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeInitializeRequest))]
    [Trait("Test", "OrderEditTest")]
    public void InitializeTest(InitializeRequest request)
    {
      // テスト用DBインスタンスを取得
      var testDB = GetDB();

      // controller作成
      var controller = new OrderEditController(new OrderEditRepository(testDB), logger);

      // controllerメソッド呼び出し
      var result = controller.Initialize(request);

      // 戻り値がJSONか確認
      Assert.True(result is JsonResult, "Not JsonResult");

      // ResponseDTOを取得
      var responseObject = ((JsonResult)result).Value as InitializeResponse;

      // ResponseDTOのnull確認
      Assert.NotNull(responseObject);

      // 値確認
      var expectedValue = string.Empty;
      var expectedResult = "OK";
      switch (request.OrderNo)
      {
        case 1:
          if (string.IsNullOrEmpty(request.TargetVersion))
          {
            expectedResult = "NG";
          }
          else
          {
            expectedValue = "test";
          }
          break;
        default:
          expectedResult = "NG";
          break;
      }
      Assert.True(responseObject.Result == expectedResult,
                $"結果が異なります。期待値:{expectedResult},検索結果:{responseObject.Result}");

      if (expectedResult == "OK")
      {
        Assert.True(responseObject.ResponseData.OrderUserID == expectedValue,
                  $"ユーザーIDが異なります[{responseObject.ResponseData.OrderUserID}]");

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
