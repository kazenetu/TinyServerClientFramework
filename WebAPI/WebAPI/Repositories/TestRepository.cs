using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons.ConfigModel;
using Commons.Interfaces;
using Microsoft.Extensions.Options;
using WebAPIFramework.BaseClasses;

namespace WebAPI.Repositories
{
  public class TestRepository: RepositoryBase
  {
    public TestRepository(IDatabase db) : base(db)
    {
    }

    /// <summary>
    /// ログイン
    /// </summary>
    /// <param name="userID">ユーザー名</param>
    /// <param name="password">パスワード</param>
    /// <returns></returns>
    public string Login(string userID, string password)
    {
      var sql = new StringBuilder();
      sql.AppendLine("select");
      sql.AppendLine("  USER_NAME");
      sql.AppendLine("from");
      sql.AppendLine("  MT_USER");
      sql.AppendLine("where ");
      sql.AppendLine("  USER_ID = @USER_ID");
      //sql.AppendLine("  AND PASSWORD = @PASSWORD");

      // Param設定
      db.ClearParam();
      db.AddParam("@USER_ID", userID);
      //db.AddParam("@PASSWORD", password);

      var result = db.Fill(sql.ToString());
      if (result.Rows.Count > 0)
      {
        return result.Rows[0].ItemArray[0].ToString();
      }

      return null;
    }
  }
}
