using Framework.DataTransferObject.BaseClasses;

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
      // TODO:プロパティを追加してください。(本コメントは削除してください)
    }
  }
}
