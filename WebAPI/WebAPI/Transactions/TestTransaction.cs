using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Repositories;
using WebAPIFramework.BaseClasses;
using WebAPIFramework.Interfaces;

namespace WebAPI.Transactions
{
  public class TestTransaction : TransactionBase
  {
    public TestTransaction(IRepositoryBase repository) : base(repository)
    {
    }

    public string Test()
    {
      var result = string.Empty;

      var testRepository = repository.Cast<TestRepository>();
      var loginResult = testRepository.Login("test", "test");
      if (!string.IsNullOrEmpty(loginResult))
      {
        result = loginResult;
      }

      return result;
    }
  }
}
