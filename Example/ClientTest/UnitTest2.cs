using Client.Business.User;
using DataTransferObjects.Request.User;
using DataTransferObjects.Response.User;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest2
    {
        [Fact]
        public void Test1()
        {
          var param = new LoginRequest() { ID = "test", Password = "test" };

          // ÉçÉOÉCÉìWebAPIî≠çs
          var business = new SampleBusiness();
          var result = business.PostSample(param);

          Assert.True(result.Result == nameof(LoginResponse.Results.OK));
    }
  }
}
