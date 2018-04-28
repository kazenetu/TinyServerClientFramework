using DataTransferObjects.Request;
using DataTransferObjects.Response;
using WebAPI.Repositories;
using WebAPIFramework.BaseClasses;
using WebAPIFramework.Interfaces;

namespace WebAPI.Transactions
{
  public class SampleTransaction : TransactionBase
  {
    private LoginRequest request = null;

    public SampleTransaction(IRepositoryBase repository, LoginRequest request) : base(repository)
    {
      this.request = request;
    }

    /// <summary>
    /// サンプルメソッド
    /// </summary>
    /// <returns></returns>
    public LoginResponse.LoginResponseParam Test()
    {
      var result = new LoginResponse.LoginResponseParam();

      // Repositoryのインスタンスを取得
      var testRepository = repository.Cast<SampleRepository>();

      // ログイン用SQLを発行
      var loginResult = testRepository.Login(request);
      if (!string.IsNullOrEmpty(loginResult))
      {
        // ユーザー名を戻り値に設定
        result.Name = loginResult;
      }

      return result;
    }
  }
}
