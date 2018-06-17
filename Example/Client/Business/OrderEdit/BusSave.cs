using Framework.Client.ConnectLib;
using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using static Client.Statics;

namespace Client.Business.OrderEdit
{
  public partial class OrderEditBusiness
  {
    /// <summary>
    /// Save
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果</returns>
    public SaveResponse Save(SaveRequest request)
    {
      var webAPIUrl = $"{WebAPIVersion}/orderedit/save";

      HttpConnectLib.StubWebAPIDelegate stub = null;
#if STUB
      // WebAPI回避　ダミーデータを返す
      stub = (url, data) =>
      {
        var response = data as SaveResponse;
        response.ErrorMessage = "";
        response.ResponseData = new SaveResponse.SaveResponseParam() {};

        // 注文番号設定
        response.ResponseData.OrderNo = request.OrderNo;
        if (response.ResponseData.OrderNo < 0)
        {
          // 登録の場合はダミー番号を返す
          response.ResponseData.OrderNo = 100;
        }

        return response;
      };
#endif

      return Post<SaveResponse>(webAPIUrl, request, stub);
    }
  }
}
