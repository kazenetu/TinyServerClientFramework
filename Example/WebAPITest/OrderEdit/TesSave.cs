using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using Microsoft.AspNetCore.Mvc;
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
    /// Save用入力データ生成、取得
    /// </summary>
    /// <returns>入力データリスト</returns>
    public static IEnumerable<object[]> MakeSaveRequest()
    {
      var result = new List<object[]>();

      // 入力データリストの追加

      // 未入力:必須項目なしエラー
      result.Add(new object[] { new SaveRequest() });

      // NG:登録対象外
      result.Add(new object[] { new SaveRequest() { TargetVersion = Statics.WebAPIVersion, OrderNo = 1, OrderUserID = "test2", ModVersion = 2 } });

      // NG:バージョンなし:登録:test
      result.Add(new object[] { new SaveRequest() { OrderNo = 0, OrderUserID = "test", ModVersion = 1 } });

      // 登録:test
      result.Add(new object[] { new SaveRequest() { TargetVersion = Statics.WebAPIVersion, OrderNo = 0, OrderUserID = "test", ModVersion = 1 } });

      // NG:登録対象外
      result.Add(new object[] { new SaveRequest() { TargetVersion = Statics.WebAPIVersion, OrderNo = 2, OrderUserID = "test", ModVersion = 1 } });

      return result;
    }

    /// <summary>
    /// Saveのテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeSaveRequest))]
    [Trait("Test", "OrderEditTest")]
    public void SaveTest(SaveRequest request)
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
        case 0:

          // 値入力なしの場合はエラー
          if(string.IsNullOrEmpty(request.OrderUserID))
          {
            expectedResult = "NG";
          }

          // バージョンなしの場合はエラー
          if (string.IsNullOrEmpty(request.TargetVersion))
          {
            expectedResult = "NG";
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
