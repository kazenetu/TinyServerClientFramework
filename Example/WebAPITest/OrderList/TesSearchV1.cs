using DataTransferObjects.Request.OrderList;
using DataTransferObjects.Response.OrderList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI.Controllers.V1.OrderList;
using WebAPI.Repositories;
using Xunit;

namespace WebAPITest.OrderList
{
  public partial class OrderListTestV1
  {
    /// <summary>
    /// Search用入力データ生成、取得
    /// </summary>
    /// <returns>入力データリスト</returns>
    public static IEnumerable<object[]> MakeSearchRequest()
    {
      var result = new List<object[]>();

      // 入力データリストの追加

      // 検索条件なし 該当件数4件
      result.Add(new object[] { new SearchRequest() { SearchUserID = string.Empty } });

      // 検索条件あり:test 該当件数2件
      result.Add(new object[] { new SearchRequest() { SearchUserID = "test" } });

      // 検索条件あり:none 該当件数0件
      result.Add(new object[] { new SearchRequest() { SearchUserID = "none" } });

      return result;
    }

    /// <summary>
    /// Searchのテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeSearchRequest))]
    [Trait("Test", "OrderListTestV1")]
    public void SearchTestV1(SearchRequest request)
    {
      // テスト用DBインスタンスを取得
      var testDB = GetDB();

      // controller作成
      var controller = new OrderListController(new OrderListRepository(testDB), logger);

      // controllerメソッド呼び出し
      var result = controller.Search(request);

      // 戻り値がJSONか確認
      Assert.True(result is JsonResult, "Not JsonResult");

      // ResponseDTOを取得
      var responseObject = ((JsonResult)result).Value as SearchResponse;

      // ResponseDTOのnull確認
      Assert.NotNull(responseObject);

      // 値確認
      var expectedValue = 0;
      var expectedErrorMessage = false;
      switch (request.SearchUserID)
      {
        case "":
          // 期待する検索件数の設定
          expectedValue = 4;
          break;
        case "test":
          // 期待する検索件数の設定
          expectedValue = 2;
          break;
        case "none":
          // 期待する検索件数の設定
          expectedValue = 0;

          // エラーメッセージの確認
          expectedErrorMessage = true;
          break;
      }
      Assert.True(responseObject.ResponseData.List.Count == expectedValue,
                  $"検索件数が異なります。期待値:{expectedValue},検索結果:{responseObject.ResponseData.List.Count}");

      if (expectedErrorMessage)
      {
        Assert.True(!string.IsNullOrEmpty(responseObject.ErrorMessage), "エラーメッセージが設定されていません。");
      }
      else
      {
        Assert.True(string.IsNullOrEmpty(responseObject.ErrorMessage), $"エラーメッセージ「{responseObject.ErrorMessage}」が設定されています。");
      }
    }
  }
}
