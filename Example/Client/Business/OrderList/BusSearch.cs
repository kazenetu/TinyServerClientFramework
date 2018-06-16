using Framework.Client.ConnectLib;
using DataTransferObjects.Request.OrderList;
using DataTransferObjects.Response.OrderList;
using static Client.Statics;

namespace Client.Business.OrderList
{
  public partial class OrderListBusiness
  {
    /// <summary>
    /// Search
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>結果</returns>
    public SearchResponse Search(SearchRequest request)
    {
      var webAPIUrl = $"{WebAPIVersion}/orderlist/search";

      HttpConnectLib.StubWebAPIDelegate stub = null;
#if STUB
      // WebAPI回避　ダミーデータを返す
      stub = (url, data) =>
      {
        var response = data as SearchResponse;
        response.ErrorMessage = "";
        response.ResponseData = new SearchResponse.SearchResponseParam() {};
        return response;
      };
#endif

      return Post<SearchResponse>(webAPIUrl, request, stub);
    }
  }
}
