using ClientFramework.ConnectLib;
using DataTransferObjects.Request.User;
using DataTransferObjects.Response.User;
using static Client.Statics;

namespace Client.Business.User
{
  public partial class SampleBusiness
  {
    /// <summary>
    /// POSTメソッドのサンプル
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>ログイン結果</returns>
    public LoginResponse PostSample(LoginRequest request)
    {
      var webAPIUrl = $"{WebAPIVersion}/values/login";

      HttpConnectLib.StubWebAPIDelegate stub = null;
#if STUB
      // WebAPI回避　ダミーデータを返す
      stub = (url, data) =>
      {
        var response = data as LoginResponse;
        response.ErrorMessage = "テストメッセージ";
        response.ResponseData = new LoginResponse.LoginResponseParam() { Name = "POSTダミー" };
        return response;
      };
#endif

      return Post<LoginResponse>(webAPIUrl, request, stub);
    }
  }
}
