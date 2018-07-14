using DataTransferObjects.Tables;
using Framework.WebAPI.BaseClasses;
using System.Linq;
using System.Text;

namespace WebAPI.Repositories
{
  public partial class OrderEditRepository : RepositoryBase
  {
    /// <summary>
    /// Save
    /// </summary>
    /// <param name="saveData">DB反映対象のDTO</param>
    /// <returns>処理結果</returns>
    public virtual bool Save(TOrder saveData)
    {
      var sql = new StringBuilder();
      sql.AppendLine("INSERT  ");
      sql.AppendLine("INTO T_ORDER(ORDER_NO, ORDER_USER_ID, MOD_VERSION) ");
      sql.AppendLine("VALUES (@ORDER_NO, @ORDER_USER_ID, 1);");

      // Param設定
      db.ClearParam();
      db.AddParam("@ORDER_NO", saveData.OrderNo);
      db.AddParam("@ORDER_USER_ID", saveData.OrderUserId);

      // SQL発行
      var result = db.ExecuteNonQuery(sql.ToString());

      // 結果確認
      if (result <= 0)
      {
        return false;
      }
      return true;
    }

    /// <summary>
    /// MAX+1の注文番号を取得
    /// </summary>
    /// <returns></returns>
    public int GetNextOrderNo()
    {
      var sql = new StringBuilder();
      sql.AppendLine("SELECT MAX(ORDER_NO)+1 ORDER_NO FROM T_ORDER;");

      // Param設定
      db.ClearParam();

      // 結果をリストで返す
      // ※ テーブルDTOまたはカスタマイズされたテーブルDTOに存在しない別名を使用している場合はfillOhterメソッドにて設定を行うこと
      var nextOrderNo = Fill<TOrder>(db.Fill(sql.ToString()));

      if (nextOrderNo.Any())
      {
        return nextOrderNo.First().OrderNo ?? 0;
      }

      return 0;
    }

  }
}
