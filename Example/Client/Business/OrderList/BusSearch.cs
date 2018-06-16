using Framework.Client.ConnectLib;
using DataTransferObjects.Request.OrderList;
using DataTransferObjects.Response.OrderList;
using static Client.Statics;
using System.Collections.Generic;
using DataTransferObjects.CustomTables;
using System.Linq;

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

        // 検索結果ダミーデータ
        var list = new List<CustomTOrder>();
        list.Add(new CustomTOrder() { OrderNo = 1, OrderUserId = "test", OrderUserName = "テストユーザー" });
        list.Add(new CustomTOrder() { OrderNo = 2, OrderUserId = "test2", OrderUserName = "テストユーザー２" });
        list.Add(new CustomTOrder() { OrderNo = 3, OrderUserId = "none", OrderUserName = "" });
        list.Add(new CustomTOrder() { OrderNo = 4, OrderUserId = "dummy", OrderUserName = "ダミー" });

        // 検索ダミーデータのフィルタリング
        List<CustomTOrder> result = null;
        if (string.IsNullOrEmpty(request.SearchUserID))
        {
          result = list;
        }
        else
        {
          result = list.Where(item => item.OrderUserId.Contains(request.SearchUserID)).ToList();
        }

        // ダミーデータを設定
        response.ResponseData = new SearchResponse.SearchResponseParam();
        response.ResponseData.List.AddRange(result);
        return response;
      };
#endif

      return Post<SearchResponse>(webAPIUrl, request, stub);
    }
  }
}
