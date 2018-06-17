using Framework.DataTransferObject.BaseClasses;

namespace DataTransferObjects.Response.OrderEdit
{
  public class ModifyResponse : ResponseBase<ModifyResponse.ModifyResponseParam>
  {
    public ModifyResponse()
    {
    }

    public ModifyResponse(Results result, string errorMessage) : base(result, errorMessage)
    {
    }

    public ModifyResponse(Results result, string errorMessage, ModifyResponseParam responseData) : base(result, errorMessage, responseData)
    {
    }

    public class ModifyResponseParam
    {
      /// <summary>
      /// 注文番号
      /// </summary>
      public int OrderNo { set; get; }
    }
  }
}
