using Framework.Client.ConnectLib;
using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using static Client.Statics;

namespace Client.Business.OrderEdit
{
  public partial class OrderEditBusiness
  {
    /// <summary>
    /// FindUserName
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果</returns>
    public FindUserNameResponse FindUserName(FindUserNameRequest request)
    {
      var webAPIUrl = "orderedit/findusername";
      request.TargetVersion = WebAPIVersion;

      HttpConnectLib.StubWebAPIDelegate stub = null;
#if STUB
      // WebAPI回避　ダミーデータを返す
      stub = (url, data) =>
      {
        var response = data as FindUserNameResponse;
        response.ErrorMessage = "";
        response.ResponseData = new FindUserNameResponse.FindUserNameResponseParam() {};

        if (string.IsNullOrEmpty(request.OrderUserID.Trim()))
        {
          response.Result = nameof(InitializeResponse.Results.NG);
          response.ErrorMessage = "ユーザーIDを設定してください。";
          return response;
        }

        switch (request.OrderUserID)
        {
          case "test":
            response.ResponseData.OrderUserName = "テストユーザー";
            break;
          case "test2":
            response.ResponseData.OrderUserName = "テストユーザー２";
            break;
          case "none":
            response.ResponseData.OrderUserName = "";
            break;
          case "dummy":
            response.ResponseData.OrderUserName = "ダミー";
            break;
          default:
            response.Result = nameof(InitializeResponse.Results.NG);
            response.ErrorMessage = "ユーザーがありません";
            break;
        }

        return response;
      };
#endif

      return Post<FindUserNameResponse>(webAPIUrl, request, stub);
    }
  }
}
