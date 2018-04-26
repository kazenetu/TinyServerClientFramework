namespace DataTransferObjects.Response
{
  public class LoginResponse : BaseResponse<LoginResponse.LoginResponseParam>
  {
    public LoginResponse()
    {
    }

    public LoginResponse(Results result, string errorMessage) : base(result, errorMessage)
    {
    }

    public LoginResponse(Results result, string errorMessage, LoginResponseParam responseData) : base(result, errorMessage, responseData)
    {
    }

    public class LoginResponseParam
    {
      public string Name { set; get; }
    }
  }
}
