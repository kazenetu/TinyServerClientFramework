using Framework.DataTransferObject.BaseClasses;

namespace DataTransferObjects.Response.OrderEdit
{
  public class FindUserNameResponse : ResponseBase<FindUserNameResponse.FindUserNameResponseParam>
  {
    public FindUserNameResponse()
    {
    }

    public FindUserNameResponse(Results result, string errorMessage) : base(result, errorMessage)
    {
    }

    public FindUserNameResponse(Results result, string errorMessage, FindUserNameResponseParam responseData) : base(result, errorMessage, responseData)
    {
    }

    public class FindUserNameResponseParam
    {
      /// <summary>
      /// 注文者名
      /// </summary>
      public string OrderUserName { set; get; }
    }
  }
}
