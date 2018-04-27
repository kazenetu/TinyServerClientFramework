using Commons.Interfaces;
using Moq;
using WebAPI.Controllers;
using WebAPI.Repositories;
using WebAPIFramework.Interfaces;
using Xunit;
using XUnitTestProject1.TestBase;

namespace XUnitTestProject1
{
  public class UnitTest1
  {
    /// <summary>
    /// Repository�̃��b�N�����ւ��Ẵe�X�g
    /// </summary>
    [Fact]
    public void Test1()
    {
      var mock = new Mock<TestRepository>(null);
      mock.Setup(c => c.Cast<TestRepository>()).Returns(mock.Object);
      mock.Setup(c => c.Login(It.IsAny<string>(), It.IsAny<string>())).Returns("");
      var controller = new ValuesController(mock.Object as IRepositoryBase);
      var result = controller.Get();

      Assert.Equal(result, new string[] { "" });
    }

    /// <summary>
    /// �C����������SQLite���g�p���Ẵ��O�C���e�X�g
    /// </summary>
    [Fact]
    public void Test2()
    {
      // �C��������SQLite��Repository�C���X�^���X���쐬
      var controller = new ValuesController(new TestRepository(getDB()));
      var result = controller.Get();

      Assert.Equal(result, new string[] { "�e�X�g���[�U�[" });
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
