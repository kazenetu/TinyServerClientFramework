using DataTransferObjects.Tables;
using Framework.WebAPI.BaseClasses;
using System.Text;

namespace WebAPI.Repositories
{
  public partial class OrderEditRepository : RepositoryBase
  {
    /// <summary>
    /// Modify
    /// </summary>
    /// <param name="saveData">DB反映対象のDTO</param>
    /// <returns>処理結果</returns>
    public virtual bool Modify(TOrder saveData)
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

      // SQL発行
      var result = db.ExecuteNonQuery(sql.ToString());

      // 結果確認
      if (result <= 0)
      {
        return false;
      }
      return true;
    }
  }
}
