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
      var webAPIUrl = $"{WebAPIVersion}/orderedit/initialize";

      HttpConnectLib.StubWebAPIDelegate stub = null;
#if STUB
      // WebAPI回避　ダミーデータを返す
      stub = (url, data) =>
      {
        var response = data as InitializeResponse;
        response.ErrorMessage = "";
        response.ResponseData = new InitializeResponse.InitializeResponseParam() {};
        return response;
      };
#endif

      return Post<InitializeResponse>(webAPIUrl, request, stub);
    }
  }
}
