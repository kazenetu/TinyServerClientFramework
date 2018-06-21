using Framework.Client.ConnectLib;
using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using static Client.Statics;

namespace Client.Business.OrderEdit
{
  public partial class OrderEditBusiness
  {
    /// <summary>
    /// Modify
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果</returns>
    public ModifyResponse Modify(ModifyRequest request)
    {
      var webAPIUrl = "orderedit/modify";
      request.TargetVersion = WebAPIVersion;

      HttpConnectLib.StubWebAPIDelegate stub = null;
#if STUB
      // WebAPI回避　ダミーデータを返す
      stub = (url, data) =>
      {
        var response = data as ModifyResponse;
        response.ErrorMessage = "";
        response.ResponseData = new ModifyResponse.ModifyResponseParam() {};

        // 注文番号設定
        response.ResponseData.OrderNo = request.OrderNo;

        return response;
      };
#endif

      return Post<ModifyResponse>(webAPIUrl, request, stub);
    }
  }
}
