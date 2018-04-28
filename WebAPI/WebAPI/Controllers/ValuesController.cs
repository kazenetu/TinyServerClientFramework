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

    // GET api/values
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
