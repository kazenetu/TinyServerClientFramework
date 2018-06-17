using Framework.DataTransferObject.BaseClasses;

namespace DataTransferObjects.Response.OrderEdit
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
      /// <summary>
      /// 注文者ID
      /// </summary>
      public string OrderUserID { set; get; } = string.Empty;

      /// <summary>
      /// 更新バージョン
      /// </summary>
      public int ModVersion { set; get; }
    }
  }
}
