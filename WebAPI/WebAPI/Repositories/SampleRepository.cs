using DataTransferObjects.Request;
using DataTransferObjects.Tables;
using System.Collections.Generic;
using System.Text;
using WebAPIFramework.BaseClasses;
using WebAPIFramework.Interfaces;

namespace WebAPI.Repositories
{
  public class SampleRepository : RepositoryBase
  {
    public SampleRepository(IDatabase db) : base(db)
    {
    }

    /// <summary>
    /// ログイン
    /// </summary>
    /// <param name="userID">ユーザー名</param>
    /// <param name="password">パスワード</param>
    /// <returns></returns>
    public virtual string Login(LoginRequest request)
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
      db.AddParam("@USER_ID", request.ID);
      //db.AddParam("@PASSWORD", request.Password);

      var result = db.Fill(sql.ToString());
      if (result.Rows.Count > 0)
      {
        return result.Rows[0].ItemArray[0].ToString();
      }

      return null;
    }

    /// <summary>
    /// 全ユーザーを取得
    /// </summary>
    /// <returns></returns>
    public virtual List<MtUser> GetAllUsers()
    {
      var result = new List<MtUser>();

      var sql = new StringBuilder();
      sql.AppendLine("select");
      sql.AppendLine("  USER_ID id");
      sql.AppendLine("  , USER_NAME");
      sql.AppendLine("  , DEL_FLAG");
      sql.AppendLine("  , ENTRY_USER");
      sql.AppendLine("  , ENTRY_DATE");
      sql.AppendLine("  , MOD_USER");
      sql.AppendLine("  , MOD_DATE");
      sql.AppendLine("  , MOD_VERSION");
      sql.AppendLine("from");
      sql.AppendLine("  MT_USER");
      sql.AppendLine(db.GetLimitSQL(25, 0));

      // Param設定
      db.ClearParam();

      // 結果をリストで返す
      return fill<MtUser>(db.Fill(sql.ToString()));
    }

    /// <summary>
    /// インスタンスのプロパティに該当しないデータの処理
    /// </summary>
    /// <typeparam name="T">テーブルDTO</typeparam>
    /// <param name="callerMethodName">fillメソッドを呼び出したメソッド名</param>
    /// <param name="columnName">カラム名</param>
    /// <param name="columnValue">カラムの値</param>
    /// <param name="instance">クラスインスタンス</param>
    protected override void fillOhter<T>(string methodName, string columnName, object columnValue, T instance)
    {
      switch (methodName)
      {
        case nameof(GetAllUsers):
          var mtUserDto = instance as MtUser;
          if (mtUserDto == null)
          {
            return;
          }

          switch (columnName)
          {
            case "id":
              mtUserDto.UserId = columnValue.ToString();
              break;
          }
          break;
      }
    }


  }
}
