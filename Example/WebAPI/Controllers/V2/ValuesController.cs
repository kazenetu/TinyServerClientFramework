using DataTransferObjects.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Transactions;
using WebAPIFramework.Interfaces;

namespace WebAPI.Controllers.V2
{
  [ApiExplorerSettings(GroupName = "v2")]
  [Route("api/v2/[controller]")]
  public class ValuesController : WebAPI.Controllers.V1.ValuesController
  {
    public ValuesController(IRepositoryBase repository, ILogger<V1.ValuesController> logger) : base(repository, logger)
    {
    }

    /// <summary>
    /// 全ユーザー情報取得 Ver2
    /// </summary>
    /// <returns>全ユーザー情報</returns>
    /// <remarks>暫定版のため入力情報なし</remarks>
    [HttpGet("alluser")]
    public override IActionResult GetAllUser()
    {
      // システムエラーチェック
      if (systenErrorResult is IActionResult) return systenErrorResult;

      var status = UsersResponse.Results.OK;
      var message = string.Empty;

      var transaction = new SampleTransaction(repository, logger);
      var resultParam = transaction.GetAllUsers();

      // Ver2の例として戻り値にダミーデータを追加する
      resultParam.userList.Add(new DataTransferObjects.Tables.MtUser() { UserId = "testV2", UserName = "Ver2追加ダミーデータ" });

      return Json(new UsersResponse(status, message, resultParam));
    }
  }
}
