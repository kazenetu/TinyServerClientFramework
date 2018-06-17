using DataTransferObjects.CustomTables;
using DataTransferObjects.Request.OrderList;
using Framework.WebAPI.BaseClasses;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Repositories
{
  public partial class OrderListRepository : RepositoryBase
  {
    /// <summary>
    /// Search
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>処理結果</returns>
    public virtual List<CustomTOrder> Search(SearchRequest request)
    {
      var sql = new StringBuilder();
      sql.AppendLine("SELECT");
      sql.AppendLine("  ORDER_NO");
      sql.AppendLine("  , MT_USER.USER_NAME ORDER_USER_NAME");
      sql.AppendLine("FROM");
      sql.AppendLine("  T_ORDER");
      sql.AppendLine("LEFT JOIN MT_USER");
      sql.AppendLine("  ON T_ORDER.ORDER_USER_ID = MT_USER.USER_ID  ");

      if (!string.IsNullOrEmpty(request.SearchUserID?.Trim()))
      {
        sql.AppendLine("WHERE ");
        sql.AppendLine("T_ORDER.ORDER_USER_ID LIKE @USER_ID");
      }

      sql.AppendLine("ORDER BY");
      sql.AppendLine("  ORDER_NO");

      // Param設定
      db.ClearParam();
      if (!string.IsNullOrEmpty(request.SearchUserID?.Trim()))
      {
        db.AddParam("@USER_ID", $"%{request.SearchUserID.Trim()}%");
      }

      // 結果をリストで返す
      // ※ テーブルDTOまたはカスタマイズされたテーブルDTOに存在しない別名を使用している場合はfillOhterメソッドにて設定を行うこと
      return Fill<CustomTOrder>(db.Fill(sql.ToString()));
    }
  }
}
