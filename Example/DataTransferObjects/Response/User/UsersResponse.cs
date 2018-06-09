using Framework.DataTransferObject.BaseClasses;
using DataTransferObjects.Tables;
using System.Collections.Generic;

namespace DataTransferObjects.Response.User
{
  public class UsersResponse: ResponseBase<UsersResponse.UsersResponsePram>
  {
    public UsersResponse()
    {
    }

    public UsersResponse(Results result, string errorMessage) : base(result, errorMessage)
    {
    }

    public UsersResponse(Results result, string errorMessage, UsersResponsePram responseData) : base(result, errorMessage, responseData)
    {
    }

    public class UsersResponsePram
    {
      public List<MtUser> userList { set; get; } = null;
    }
  }
}
