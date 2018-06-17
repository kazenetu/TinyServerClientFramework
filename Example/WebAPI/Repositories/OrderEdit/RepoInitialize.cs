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
    /// Initialize
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>処理結果</returns>
    public virtual List<TOrder> Initialize(InitializeRequest request)
    {
      var sql = new StringBuilder();
      sql.AppendLine("SELECT");
      sql.AppendLine("  ORDER_USER_ID, MOD_VERSION");
      sql.AppendLine("FROM");
      sql.AppendLine("  T_ORDER");
      sql.AppendLine("WHERE ");
      sql.AppendLine("  ORDER_NO = @ORDER_NO");

      // Param設定
      db.ClearParam();
      db.AddParam("@ORDER_NO", request.OrderNo);

      // 結果をリストで返す
      // ※ テーブルDTOまたはカスタマイズされたテーブルDTOに存在しない別名を使用している場合はfillOhterメソッドにて設定を行うこと
      return Fill<TOrder>(db.Fill(sql.ToString()));
    }
  }
}
