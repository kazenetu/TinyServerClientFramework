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
    /// Initialize用入力データ生成、取得
    /// </summary>
    /// <returns>入力データリスト</returns>
    public static IEnumerable<object[]> MakeInitializeRequest()
    {
      var result = new List<object[]>();

      // 入力データリストの追加
      // TODO 入力データとしてInitializeRequestを作成してください。(本コメントは削除してください)
      //result.Add(new object[] { new InitializeRequest() { プロパティ名=値,...} });
      result.Add(new object[] { new InitializeRequest() }); //例(削除してください)

      return result;
    }

    /// <summary>
    /// Initializeのテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeInitializeRequest))]
    [Trait("Test", "OrderListTestV1")]
    public void InitializeTestV1(InitializeRequest request)
    {
      // テスト用DBインスタンスを取得
      var testDB = GetDB();

      // controller作成
      var controller = new OrderListController(new OrderListRepository(testDB), logger);

      // controllerメソッド呼び出し
      var result = controller.Initialize(request);

      // 戻り値がJSONか確認
      Assert.True(result is JsonResult, "Not JsonResult");

      // ResponseDTOを取得
      var responseObject = ((JsonResult)result).Value as InitializeResponse;

      // ResponseDTOのnull確認
      Assert.NotNull(responseObject);

      // 値確認
      // TODO 具体的な値の確認をしてください。(本コメントは削除してください)

    }
  }
}
