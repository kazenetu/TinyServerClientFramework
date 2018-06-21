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
    /// テスト用文字列:test2
    /// </summary>
    private const string Test2 = "test2";

    /// <summary>
    /// テスト用文字列:none
    /// </summary>
    private const string None = "none";

    /// <summary>
    /// FindUserName用入力データ生成、取得
    /// </summary>
    /// <returns>入力データリスト</returns>
    public static IEnumerable<object[]> MakeFindUserNameRequest()
    {
      var result = new List<object[]>();

      // 入力データリストの追加

      // 未入力:データなしエラー
      result.Add(new object[] { new FindUserNameRequest() });

      // test2:NG:バージョンなし:テストユーザー２
      result.Add(new object[] { new FindUserNameRequest() { OrderUserID = Test2 } });

      // test2:テストユーザー２
      result.Add(new object[] { new FindUserNameRequest() { TargetVersion = Statics.WebAPIVersion, OrderUserID = Test2 } });

      // none:データなしエラー
      result.Add(new object[] { new FindUserNameRequest() { TargetVersion = Statics.WebAPIVersion, OrderUserID = None } });

      return result;
    }

    /// <summary>
    /// FindUserNameのテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeFindUserNameRequest))]
    [Trait("Test", "OrderEditTest")]
    public void FindUserNameTest(FindUserNameRequest request)
    {
      // テスト用DBインスタンスを取得
      var testDB = GetDB();

      // controller作成
      var controller = new OrderEditController(new OrderEditRepository(testDB), logger);

      // controllerメソッド呼び出し
      var result = controller.FindUserName(request);

      // 戻り値がJSONか確認
      Assert.True(result is JsonResult, "Not JsonResult");

      // ResponseDTOを取得
      var responseObject = ((JsonResult)result).Value as FindUserNameResponse;

      // ResponseDTOのnull確認
      Assert.NotNull(responseObject);

      // 値確認
      var expectedValue = string.Empty;
      var expectedResult = "OK";
      switch (request.OrderUserID)
      {
        case Test2:
          if (string.IsNullOrEmpty(request.TargetVersion))
          {
            expectedResult = "NG";
          }
          else
          {
            expectedValue = "テストユーザー２";
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
        Assert.True(responseObject.ResponseData.OrderUserName == expectedValue,
                  $"ユーザー名が異なります[{responseObject.ResponseData.OrderUserName}]");

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
