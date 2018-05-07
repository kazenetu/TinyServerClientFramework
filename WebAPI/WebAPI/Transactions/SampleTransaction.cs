using DataTransferObjects.Request;
using DataTransferObjects.Response;
using DataTransferObjects.Tables;
using System;
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

    /// <summary>
    /// 全ユーザー取得
    /// </summary>
    /// <returns></returns>
    public UsersResponse.UsersResponsePram GetAllUsers()
    {
      var result = new UsersResponse.UsersResponsePram();

      // Repositoryのインスタンスを取得
      var testRepository = repository.Cast<SampleRepository>();

      // SQLを発行・戻り値に格納
      result.userList = testRepository.GetAllUsers();

      return result;
    }

    /// <summary>
    /// ユーザー追加
    /// </summary>
    /// <returns>追加したユーザーを含めた全ユーザー</returns>
    /// <remarks>追加ユーザーは暫定の内容</remarks>
    public UsersResponse.UsersResponsePram AddUser()
    {
      var result = new UsersResponse.UsersResponsePram();

      // トランザクション設定
      repository.BeginTransaction();

      // ユーザーRepositoryのインスタンスを取得
      var user = new MtUser();
      user.UserId = $"u{DateTime.Now.ToLongTimeString()}";
      user.UserName = $"test{DateTime.Now.ToLongTimeString()}";
      user.Password = "test";
      user.EntryUser = "test";
      var userRepository = repository.Cast<UserRepository>();
      if (userRepository.AddUser(user))
      {
        // TestRepositoryのインスタンスを取得
        var testRepository = repository.Cast<SampleRepository>();

        // SQLを発行・戻り値に格納
        result.userList = testRepository.GetAllUsers();

        repository.Commit();
      }
      else
      {
        repository.Rollback();
      }

      return result;
    }
  }
}
