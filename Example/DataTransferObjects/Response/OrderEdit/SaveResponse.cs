using Framework.DataTransferObject.BaseClasses;

namespace DataTransferObjects.Response.OrderEdit
{
  public class SaveResponse : ResponseBase<SaveResponse.SaveResponseParam>
  {
    public SaveResponse()
    {
    }

    public SaveResponse(Results result, string errorMessage) : base(result, errorMessage)
    {
    }

    public SaveResponse(Results result, string errorMessage, SaveResponseParam responseData) : base(result, errorMessage, responseData)
    {
    }

    public class SaveResponseParam
    {
      /// <summary>
      /// 注文番号
      /// </summary>
      public int OrderNo { set; get; }
    }
  }
}
