using Framework.Client.ConnectLib;
using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using static Client.Statics;

namespace Client.Business.OrderEdit
{
  public partial class OrderEditBusiness
  {
    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果</returns>
    public InitializeResponse Initialize(InitializeRequest request)
    {
      var webAPIUrl = "orderedit/initialize";
      request.TargetVersion = WebAPIVersion;

      HttpConnectLib.StubWebAPIDelegate stub = null;
#if STUB
      // WebAPI回避　ダミーデータを返す
      stub = (url, data) =>
      {
        var response = data as InitializeResponse;
        response.ErrorMessage = "";
        response.ResponseData = new InitializeResponse.InitializeResponseParam() {};

        switch(request.OrderNo)
        {
          case 1:
            response.ResponseData.OrderUserID = "test1";
            break;
          case 2:
            response.ResponseData.OrderUserID = "test2";
            break;
          case 3:
            response.ResponseData.OrderUserID = "none";
            break;
          case 4:
            response.ResponseData.OrderUserID = "dummy";
            break;
          default:
            response.Result = nameof(InitializeResponse.Results.NG);
            break;
        }

        return response;
      };
#endif

      return Post<InitializeResponse>(webAPIUrl, request, stub);
    }
  }
}
