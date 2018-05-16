using DataTransferObjects.Request;
using DataTransferObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
      // システムエラーチェック
      if (systenErrorResult is IActionResult) return systenErrorResult;

      // 入力チェック
      if (!request.Validate())
      {
        logger.LogError("Pram[{0}]が未設定", request.ValidateNGPropertyName);
        return BadRequest();
      }

      var status = LoginResponse.Results.OK;
      var message = string.Empty;

      var transaction = new SampleTransaction(repository, logger);
      var resultParam = transaction.Test(request);
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
      // システムエラーチェック
      if (systenErrorResult is IActionResult) return systenErrorResult;

      // 入力チェック
      if (!request.Validate())
      {
        logger.LogError("Pram[{0}]が未設定", request.ValidateNGPropertyName);
        return BadRequest();
      }

      var status = LoginResponse.Results.OK;
      var message = string.Empty;

      var transaction = new SampleTransaction(repository, logger);
      var resultParam = transaction.Test(request);
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
      // システムエラーチェック
      if (systenErrorResult is IActionResult) return systenErrorResult;

      var status = UsersResponse.Results.OK;
      var message = string.Empty;

      var transaction = new SampleTransaction(repository, logger);
      var resultParam = transaction.GetAllUsers();

      return Json(new UsersResponse(status, message, resultParam));
    }

    /// <summary>
    /// ユーザー追加
    /// </summary>
    /// <param name="request">追加ユーザー情報</param>
    /// <returns>追加したユーザーも含めた全ユーザー情報</returns>
    [HttpGet("adduser")]
    public IActionResult AddUser(AddUserRequest request)
    {
      // システムエラーチェック
      if (systenErrorResult is IActionResult) return systenErrorResult;

      // 入力チェック
      if (!request.Validate())
      {
        logger.LogError("Pram[{0}]が未設定", request.ValidateNGPropertyName);
        return BadRequest();
      }

      var status = UsersResponse.Results.OK;
      var message = string.Empty;

      var transaction = new SampleTransaction(repository, logger);
      var resultParam = transaction.AddUser(request,DateTime.Now);
      if (resultParam.userList == null)
      {
        status = UsersResponse.Results.NG;
        message = "追加失敗";
        resultParam = null;
      }

      return Json(new UsersResponse(status, message, resultParam));
    }
  }
}
