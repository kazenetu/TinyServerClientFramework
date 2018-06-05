using DataTransferObjects.BaseClasses;
using DataTransferObjects.Tables;
using System.Text;
using WebAPIFramework.BaseClasses;
using WebAPIFramework.Interfaces;

namespace WebAPI.Repositories
{
  public class UserRepository : RepositoryBase
  {
    public UserRepository(IDatabase db) : base(db)
    {
    }


    /// <summary>
    /// ユーザー追加
    /// </summary>
    /// <param name="request">入力情報</param>
    /// <returns>追加結果</returns>
    public virtual bool AddUser(MtUser request)
    {
      var sql = new StringBuilder();
      sql.AppendLine("INSERT ");
      sql.AppendLine("INTO MT_USER( ");
      sql.AppendLine("  USER_ID");
      sql.AppendLine("  , USER_NAME");
      sql.AppendLine("  , PASSWORD");
      sql.AppendLine("  , DEL_FLAG");
      sql.AppendLine("  , ENTRY_USER");
      sql.AppendLine("  , ENTRY_DATE");
      sql.AppendLine("  , MOD_VERSION");
      sql.AppendLine(") ");
      sql.AppendLine("VALUES ( ");
      sql.AppendLine("  @USER_ID");
      sql.AppendLine("  , @USER_NAME");
      sql.AppendLine("  , @PASSWORD");
      sql.AppendLine("  , @DEL_FLAG");
      sql.AppendLine("  , @ENTRY_USER");
      sql.AppendLine("  , @ENTRY_DATE");
      sql.AppendLine("  , 1");
      sql.AppendLine(")");

      // Param設定
      db.ClearParam();
      db.AddParam("@USER_ID", request.UserId);
      db.AddParam("@USER_NAME", request.UserName);
      db.AddParam("@PASSWORD", request.Password);
      db.AddParam("@ENTRY_USER", request.EntryUser);
      db.AddParam("@ENTRY_DATE", request.EntryDate);
      db.AddParam("@DEL_FLAG", TableBase.StringFalse);

      // SQL発行
      var result = db.ExecuteNonQuery(sql.ToString());
      if (result == 1)
      {
        return true;
      }
      return false;
    }
  }
}
