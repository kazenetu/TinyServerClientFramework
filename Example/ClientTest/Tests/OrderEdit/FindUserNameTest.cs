using Client.Business.OrderEdit;
using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Response.OrderEdit;
using Xunit;

namespace ClientTest.Tests.OrderEdit
{
  public class FindUserNameTest
  {
    [Fact]
    public void Test1()
    {
      var param = new FindUserNameRequest() { OrderUserID = "test" };

      // WebAPI問合せ
      var business = new OrderEditBusiness();
      var result = business.FindUserName(param);

      Assert.True(result.Result == nameof(FindUserNameResponse.Results.OK));

      Assert.True(result.ResponseData.OrderUserName == "テストユーザー");
    }
  }
}
