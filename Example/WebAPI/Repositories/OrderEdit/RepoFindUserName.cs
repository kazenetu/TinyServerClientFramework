using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Tables;
using System.Collections.Generic;
using System.Text;
using Framework.WebAPI.BaseClasses;

namespace WebAPI.Repositories
{
  public partial class OrderEditRepository : RepositoryBase
  {
    /// <summary>
    /// FindUserName
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>処理結果</returns>
    public virtual List<MtUser> FindUserName(FindUserNameRequest request)
    {
      var sql = new StringBuilder();
      sql.AppendLine("SELECT");
      sql.AppendLine("  USER_NAME");
      sql.AppendLine("FROM");
      sql.AppendLine("  MT_USER ");
      sql.AppendLine("WHERE");
      sql.AppendLine("  USER_ID = @USER_ID ");

      // Param設定
      db.ClearParam();
      db.AddParam("@USER_ID",request.OrderUserID);

      // 結果をリストで返す
      // ※ テーブルDTOまたはカスタマイズされたテーブルDTOに存在しない別名を使用している場合はfillOhterメソッドにて設定を行うこと
      return Fill<MtUser>(db.Fill(sql.ToString()));
    }
  }
}
