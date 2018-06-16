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
    /// FindUserName用入力データ生成、取得
    /// </summary>
    /// <returns>入力データリスト</returns>
    public static IEnumerable<object[]> MakeFindUserNameRequest()
    {
      var result = new List<object[]>();

      // 入力データリストの追加
      // TODO 入力データとしてFindUserNameRequestを作成してください。(本コメントは削除してください)
      //result.Add(new object[] { new FindUserNameRequest() { プロパティ名=値,...} });
      result.Add(new object[] { new FindUserNameRequest() }); //例(削除してください)

      return result;
    }

    /// <summary>
    /// FindUserNameのテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeFindUserNameRequest))]
    [Trait("Test", "OrderEditTestV1")]
    public void FindUserNameTestV1(FindUserNameRequest request)
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
      // TODO 具体的な値の確認をしてください。(本コメントは削除してください)

    }
  }
}
