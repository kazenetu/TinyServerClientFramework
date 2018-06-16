using DataTransferObjects.CustomTables;
using Framework.DataTransferObject.BaseClasses;
using System.Collections.Generic;

namespace DataTransferObjects.Response.OrderList
{
  public class SearchResponse : ResponseBase<SearchResponse.SearchResponseParam>
  {
    public SearchResponse()
    {
    }

    public SearchResponse(Results result, string errorMessage) : base(result, errorMessage)
    {
    }

    public SearchResponse(Results result, string errorMessage, SearchResponseParam responseData) : base(result, errorMessage, responseData)
    {
    }

    public class SearchResponseParam
    {
      /// <summary>
      /// 検索結果
      /// </summary>
      public List<CustomTOrder> List { get; } = new List<CustomTOrder>();
    }
  }
}
