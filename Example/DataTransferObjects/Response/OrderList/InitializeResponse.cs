using Framework.DataTransferObject.BaseClasses;

namespace DataTransferObjects.Response.OrderList
{
  public class InitializeResponse : ResponseBase<InitializeResponse.InitializeResponseParam>
  {
    public InitializeResponse()
    {
    }

    public InitializeResponse(Results result, string errorMessage) : base(result, errorMessage)
    {
    }

    public InitializeResponse(Results result, string errorMessage, InitializeResponseParam responseData) : base(result, errorMessage, responseData)
    {
    }

    public class InitializeResponseParam
    {
      // TODO:プロパティを追加してください。(本コメントは削除してください)
    }
  }
}
