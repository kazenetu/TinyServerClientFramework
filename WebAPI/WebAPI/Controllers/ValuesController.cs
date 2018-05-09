using DataTransferObjects.Request;
using DataTransferObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Transactions;
using WebAPIFramework.BaseClasses;
using WebAPIFramework.Interfaces;

namespace WebAPI.Controllers
{
  [Route("api/[controller]")]
  public class ValuesController : ControllerWithRepositoryBase
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">DIで取得するRepositoryBase用インターフェース</param>
    /// <param name="logger">ロガーインスタンス</param>
    public ValuesController(IRepositoryBase repository, ILogger<ValuesController> logger) : base(repository, logger)
    {
    }

    /// <summary>
    /// ログインチェック(GET版)
    /// </summary>
    /// <param name="request">ログイン情報</param>
    /// <returns>ログイン結果</returns>
    [HttpGet]
    public IActionResult Get(LoginRequest request)
    {
      // 入力チェック
      if (!request.Validate())
      {
        logger.LogError("Pram[{0}]が未設定", request.ValidateNGPropertyName);
        return BadRequest();
      }

      var status = LoginResponse.Results.OK;
      var message = string.Empty;

      var tansaction = new SampleTransaction(repository, request);
      var resultParam = tansaction.Test();
      if (string.IsNullOrEmpty(resultParam.Name))
      {
        status = LoginResponse.Results.NG;
        message = "ログイン不可";
      }

      return Json(new LoginResponse(status, message, resultParam));
    }

    /// <summary>
    /// ログインチェック(POST版)
    /// </summary>
    /// <param name="request">ログイン情報</param>
    /// <returns>ログイン結果</returns>
    [HttpPost("login")]
    [AutoValidateAntiforgeryToken]
    public IActionResult Login([FromBody]LoginRequest request)
    {
      // 入力チェック
      if (!request.Validate())
      {
        logger.LogError("Pram[{0}]が未設定", request.ValidateNGPropertyName);
        return BadRequest();
      }

      var status = LoginResponse.Results.OK;
      var message = string.Empty;

      var tansaction = new SampleTransaction(repository, request);
      var resultParam = tansaction.Test();
      if (string.IsNullOrEmpty(resultParam.Name))
      {
        status = LoginResponse.Results.NG;
        message = "ログイン不可";
      }

      return Json(new LoginResponse(status, message, resultParam));
    }

    /// <summary>
    /// 全ユーザー情報取得
    /// </summary>
    /// <returns>全ユーザー情報</returns>
    /// <remarks>暫定版のため入力情報なし</remarks>
    [HttpGet("alluser")]
    public IActionResult GetAllUser()
    {
      var status = UsersResponse.Results.OK;
      var message = string.Empty;

      var tansaction = new SampleTransaction(repository, null);
      var resultParam = tansaction.GetAllUsers();

      return Json(new UsersResponse(status, message, resultParam));
    }

    /// <summary>
    /// ユーザー追加
    /// </summary>
    /// <returns>追加したユーザーも含めた全ユーザー情報</returns>
    /// <remarks>暫定版のため入力情報なし</remarks>
    [HttpGet("adduser")]
    public IActionResult AddUser()
    {
      var status = UsersResponse.Results.OK;
      var message = string.Empty;

      var transaction = new SampleTransaction(repository, null);
      var resultParam = transaction.AddUser();
      if (resultParam.userList == null)
      {
        status = UsersResponse.Results.NG;
        message = "追加失敗";
        resultParam = null;
      }

      return Json(new UsersResponse(status, message, resultParam));
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
