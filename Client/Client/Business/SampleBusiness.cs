using ClientFramework.BaseClasses;
using ClientFramework.ConnectLib;
using DataTransferObjects.Request;
using DataTransferObjects.Response;
using static Client.Statics;

namespace Client.Business
{
  /// <summary>
  /// サンプルロジック
  /// </summary>
  public class SampleBusiness : BussinessBase
  {

    /// <summary>
    /// GETメソッドのサンプル
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>ログイン結果</returns>
    public LoginResponse GetSample(LoginRequest request)
    {
      var webAPIUrl = $"{WebAPIVersion}/values?id={request.ID}&password={request.Password}";

      HttpConnectLib.StubWebAPIDelegate stub = null;
#if STUB
      // WebAPI回避　ダミーデータを返す
      stub = (url, data) =>
      {
        var response = data as LoginResponse;
        response.ErrorMessage = "テストメッセージ";
        response.ResponseData = new LoginResponse.LoginResponseParam() { Name = "GETダミー" };
        return response;
      };
#endif

      return get<LoginResponse>(webAPIUrl, stub);
    }

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

      return post<LoginResponse>(webAPIUrl, request, stub);
    }
  }
}
