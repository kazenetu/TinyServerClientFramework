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
      var webAPIUrl = $"{WebAPIVersion}/orderedit/findusername";

      HttpConnectLib.StubWebAPIDelegate stub = null;
#if STUB
      // WebAPI回避　ダミーデータを返す
      stub = (url, data) =>
      {
        var response = data as FindUserNameResponse;
        response.ErrorMessage = "";
        response.ResponseData = new FindUserNameResponse.FindUserNameResponseParam() {};
        return response;
      };
#endif

      return Post<FindUserNameResponse>(webAPIUrl, request, stub);
    }
  }
}
