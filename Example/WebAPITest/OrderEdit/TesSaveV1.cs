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
      //result.Add(new object[] { new SaveRequest() { プロパティ名=値,...} });
      result.Add(new object[] { new SaveRequest() }); //例(削除してください)

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
      // TODO 具体的な値の確認をしてください。(本コメントは削除してください)

    }
  }
}
