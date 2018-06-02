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
using WebAPITest.TestBase;

namespace WebAPITest
{
  public class UnitTest1
  {
    /// <summary>
    /// ���O�C���X�^���X
    /// </summary>
    private ILogger<ValuesController> logger = new LoggerFactory().AddConsole().CreateLogger<ValuesController>();

    /// <summary>
    /// ���N�G�X�g�쐬���\�b�h
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
    /// Repository�̃��b�N�����ւ��Ẵe�X�g
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeLoginRequest),"test","test")]
    public void Test1(LoginRequest request)
    {

      var mock = new Mock<SampleRepository>(null);
      mock.Setup(c => c.Cast<SampleRepository>()).Returns(mock.Object);
      mock.Setup(c => c.Login(It.IsAny<LoginRequest>())).Returns("�e�X�g���[�U�[");
      var controller = new ValuesController(mock.Object as IRepositoryBase, logger);

      // �R���g���[���[���\�b�h���s
      var result = controller.Get(request);

      // Response�̌��ʂ�JsonResult
      Assert.True(result is JsonResult, "Not JsonResult");

      // ���ʃI�u�W�F�N�g��擾
      var responseObject = ((JsonResult)result).Value as LoginResponse;

      // ���ʊm�F
      Assert.NotNull(responseObject);
    }

    /// <summary>
    /// �C����������SQLite��g�p���Ẵ��O�C���e�X�g
    /// </summary>
    [Theory]
    [MemberData(nameof(MakeLoginRequest), "test", "test")]
    public void Test2(LoginRequest request)
    {
      // �C��������SQLite��Repository�C���X�^���X��쐬
      var controller = new ValuesController(new SampleRepository(getDB()), logger);

      // �R���g���[���[���\�b�h���s
      var result = controller.Get(request);

      // Response�̌��ʂ�JsonResult
      Assert.True(result is JsonResult, "Not JsonResult");

      // ���ʃI�u�W�F�N�g��擾
      var responseObject = ((JsonResult)result).Value as LoginResponse;

      Assert.NotNull(responseObject);

      // ���ʊm�F
      Assert.Equal("�e�X�g���[�U�[", responseObject.ResponseData.Name);
    }

    /// <summary>
    /// �C��������SQLite�C���X�^���X�̎擾
    /// </summary>
    /// <returns></returns>
    private IDatabase getDB()
    {
      // �C��������SQLite�C���X�^���X����
      var db = new TestSQLiteDB(@":memory:");

      // �e�[�u���쐬
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

      // �e�X�g�f�[�^�o�^
      var testData = @"insert into MT_USER(USER_ID,USER_NAME,PASSWORD,DEL_FLAG,ENTRY_USER,ENTRY_DATE,MOD_USER,MOD_DATE,MOD_VERSION) values ('test','�e�X�g���[�U�[','Z5SMGm/kEGTiZP8tHwuWSwYWFguMP7/qJOnLNL1u4is=','0','','2018/01/21 17:32:00',null,null,1);";
      db.ExecuteNonQuery(testData);

      return db;
    }
  }
}
