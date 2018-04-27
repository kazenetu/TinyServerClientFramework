using WebAPI.Repositories;
using WebAPIFramework.BaseClasses;
using WebAPIFramework.Interfaces;

namespace WebAPI.Transactions
{
  public class SampleTransaction : TransactionBase
  {
    public SampleTransaction(IRepositoryBase repository) : base(repository)
    {
    }

    /// <summary>
    /// サンプルメソッド
    /// </summary>
    /// <returns></returns>
    public string Test()
    {
      var result = string.Empty;

      // Repositoryのインスタンスを取得
      var testRepository = repository.Cast<SampleRepository>();

      // ログイン用SQLを発行
      var loginResult = testRepository.Login("test", "test");
      if (!string.IsNullOrEmpty(loginResult))
      {
        // ユーザー名を戻り値に設定
        result = loginResult;
      }

      return result;
    }
  }
}
