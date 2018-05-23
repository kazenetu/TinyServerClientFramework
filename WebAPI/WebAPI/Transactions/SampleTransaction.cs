using DataTransferObjects.Request.User;
using DataTransferObjects.Response.User;
using DataTransferObjects.Tables;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using WebAPI.Repositories;
using WebAPIFramework.BaseClasses;
using WebAPIFramework.Interfaces;

namespace WebAPI.Transactions
{
  public class SampleTransaction : TransactionBase
  {
    public SampleTransaction(IRepositoryBase repository, ILogger logger) : base(repository, logger)
    {
    }

    /// <summary>
    /// サンプルメソッド
    /// </summary>
    /// <returns></returns>
    public LoginResponse.LoginResponseParam Test(LoginRequest request)
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
    /// <param name="request">追加ユーザー情報</param>
    /// <param name="entryDate">追加日時</param>
    /// <returns>追加したユーザーを含めた全ユーザー</returns>
    public UsersResponse.UsersResponsePram AddUser(AddUserRequest request,DateTime entryDate)
    {
      var result = new UsersResponse.UsersResponsePram();

      // トランザクション設定
      repository.BeginTransaction();

      try
      {
        // ユーザーRepositoryのインスタンスを取得
        var user = new MtUser();
        user.UserId = request.UserId;
        user.UserName = request.UserName;
        user.Password = createPasswordHash(request.UserId, request.Password, entryDate);
        user.EntryUser = request.LoginUserID;
        user.EntryDate = entryDate;
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
      }
      catch (Exception ex)
      {
        logger.LogError($"{nameof(SampleTransaction)}.{nameof(AddUser)}:\"{ex.Message}\"");
        repository.Rollback();
      }

      return result;
    }

    protected string createPasswordHash(string userId, string password, DateTime createDate)
    {
      byte[] salt = new byte[128 / 8];
      var saltSrc = createDate.ToString("yyyyMMddhhmmsss") + userId;
      salt = Encoding.UTF8.GetBytes(saltSrc);

      // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
      string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: password,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA1,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));

      return hashed;
    }

  }
}
