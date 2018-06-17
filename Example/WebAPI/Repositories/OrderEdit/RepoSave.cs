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
      var sql = string.Empty;
      if(saveData.OrderNo > 0)
      {
        sql = GetUpdateQuery(saveData);
      }
      else
      {
        sql = GetInsertQuery(saveData);
      }

      // SQL発行
      var result = db.ExecuteNonQuery(sql);

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
        return nextOrderNo.First().OrderNo;
      }

      return 0;
    }

    /// <summary>
    /// 更新クエリ取得
    /// </summary>
    /// <param name="saveData">DB反映対象のDTO</param>
    /// <returns>更新SQL</returns>
    /// <remarks>パラメータ設定も実施する</remarks>
    private string GetUpdateQuery(TOrder saveData)
    {
      var sql = new StringBuilder();
      sql.AppendLine("UPDATE T_ORDER ");
      sql.AppendLine("SET");
      sql.AppendLine("  ORDER_USER_ID = @ORDER_USER_ID");
      sql.AppendLine("WHERE");
      sql.AppendLine("  ORDER_NO = @ORDER_NO");
      sql.AppendLine("  AND MOD_VERSION = @MOD_VERSION ");

      // Param設定
      db.ClearParam();
      db.AddParam("@ORDER_USER_ID", saveData.OrderUserId);
      db.AddParam("@ORDER_NO", saveData.OrderNo);
      db.AddParam("@MOD_VERSION", saveData.ModVersion);

      return sql.ToString();
    }

    /// <summary>
    /// 登録クエリ取得
    /// </summary>
    /// <param name="saveData">DB反映対象のDTO</param>
    /// <returns>登録SQL</returns>
    /// <remarks>パラメータ設定も実施する</remarks>
    private string GetInsertQuery(TOrder saveData)
    {
      var sql = new StringBuilder();
      sql.AppendLine("INSERT  ");
      sql.AppendLine("INTO T_ORDER(ORDER_NO, ORDER_USER_ID, MOD_VERSION) ");
      sql.AppendLine("SELECT MAX(ORDER_NO)+1, @ORDER_USER_ID, 1 FROM T_ORDER;");

      // Param設定
      db.ClearParam();
      db.AddParam("@ORDER_USER_ID", saveData.OrderUserId);

      return sql.ToString();
    }

  }
}
