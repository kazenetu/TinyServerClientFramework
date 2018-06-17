using DataTransferObjects.Tables;
using System.Collections.Generic;
using System.Reflection;

namespace DataTransferObjects.CustomTables
{
  /// <summary>
  /// 注文一覧用カスタムテーブルDTO
  /// </summary>
  public class CustomTOrder : TOrder
  {
    /// <summary>
    /// DBカラム名とプロパティのコレクション取得
    /// </summary>
    /// <returns>DBカラム名とプロパティのコレクション</returns>
    public override Dictionary<string, PropertyInfo> GetDBComlunProperyColection()
    {
      var result = base.GetDBComlunProperyColection();

      var classType = this.GetType();
      result.Add("ORDER_USER_NAME", classType.GetProperty("OrderUserName"));
      return result;
    }

    /// <summary>
    /// 注文者名
    /// </summary>
    public string OrderUserName { set; get; }
  }
}
