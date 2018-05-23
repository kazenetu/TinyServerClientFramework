using DataTransferObjects.Request.User;
using DataTransferObjects.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using WebAPI.Controllers.V1;
using WebAPI.Repositories;
using WebAPIFramework.Interfaces;
using Xunit;
using XUnitTestProject1.TestBase;

namespace XUnitTestProject1
{
  public class UnitTest1
  {
    /// <summary>
    /// ログインスタンス
    /// </summary>
    private ILogger<ValuesController> logger = new LoggerFactory().AddConsole().CreateLogger<ValuesController>();

    /// <summary>
    /// リクエスト作成メソッド
    /// </summary>
    /// <param name="id"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static IEnumerable<object[]> MakeLoginRequest(string id,string password)
    {
      var result = new List<object[]>();

      result.Add(new object[] { new LoginRequest() { ID = id, Password = password } });

      return result;
    }

    /// <summary>
    /// Repositoryのモック差し替えてのテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeLoginRequest),"test","test")]
    public void Test1(LoginRequest request)
    {

      var mock = new Mock<SampleRepository>(null);
      mock.Setup(c => c.Cast<SampleRepository>()).Returns(mock.Object);
      mock.Setup(c => c.Login(It.IsAny<LoginRequest>())).Returns("テストユーザー");
      var controller = new ValuesController(mock.Object as IRepositoryBase, logger);

      // コントローラーメソッド実行
      var result = controller.Get(request);

      // Responseの結果がJsonResult
      Assert.True(result is JsonResult, "Not JsonResult");

      // 結果オブジェクトを取得
      var responseObject = ((JsonResult)result).Value as LoginResponse;

      // 結果確認
      Assert.NotNull(responseObject);
    }

    /// <summary>
    /// インメモリのSQLiteを使用してのログインテスト
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeLoginRequest), "test", "test")]
    public void Test2(LoginRequest request)
    {
      // インメモリSQLiteでRepositoryインスタンスを作成
      var controller = new ValuesController(new SampleRepository(getDB()), logger);

      // コントローラーメソッド実行
      var result = controller.Get(request);

      // Responseの結果がJsonResult
      Assert.True(result is JsonResult, "Not JsonResult");

      // 結果オブジェクトを取得
      var responseObject = ((JsonResult)result).Value as LoginResponse;

      Assert.NotNull(responseObject);

      // 結果確認
      Assert.Equal("テストユーザー", responseObject.ResponseData.Name);
    }

    /// <summary>
    /// インメモリSQLiteインスタンスの取得
    /// </summary>
    /// <returns></returns>
    private IDatabase getDB()
    {
      // インメモリSQLiteインスタンス生成
      var db = new TestSQLiteDB(@":memory:");

      // テーブル作成
      var createTable =
          @"create table MT_USER (
                  USER_ID NVARCHAR
                  , USER_NAME NVARCHAR
                  , PASSWORD NVARCHAR
                  , DEL_FLAG NCHAR
                  , ENTRY_USER NVARCHAR
                  , ENTRY_DATE DATETIME
                  , MOD_USER NVARCHAR
                  , MOD_DATE DATETIME
                  , MOD_VERSION INT
                  , primary key (USER_ID)
                );";

      db.ExecuteNonQuery(createTable);

      // テストデータ登録
      var testData = @"insert into MT_USER(USER_ID,USER_NAME,PASSWORD,DEL_FLAG,ENTRY_USER,ENTRY_DATE,MOD_USER,MOD_DATE,MOD_VERSION) values ('test','テストユーザー','Z5SMGm/kEGTiZP8tHwuWSwYWFguMP7/qJOnLNL1u4is=','0','','2018/01/21 17:32:00',null,null,1);";
      db.ExecuteNonQuery(testData);

      return db;
    }


    //public IRepositoryBase GetDummyRepository()
    //{
    //    return new DummyTestRepo(null);
    //}

    //private class DummyTestRepo : TestRepository
    //{
    //    public DummyTestRepo(IDatabase db) : base(db)
    //    {
    //    }
    //    public override T Cast<T>() 
    //    {
    //        return (T)(IRepositoryBase)new DummyTestRepo(db);
    //    }

    //    public override string Login(string userID, string password)
    //    {
    //        return "";
    //    }
    //}
  }
}
