using Framework.DataTransferObject.BaseClasses;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataTransferObjects.Tables
{
  /// <summary>
  /// 注文
  /// </summary>
  public class TOrder : TableBase
  {
    /// <summary>
    /// DBカラム名とプロパティのコレクション取得
    /// </summary>
    /// <returns>DBカラム名とプロパティのコレクション</returns>
    public override Dictionary<string, PropertyInfo> GetDBComlunProperyColection()
    {
      var result = new Dictionary<string, PropertyInfo>();

      var classType = this.GetType();
      result.Add("ORDER_NO", classType.GetProperty("OrderNo"));
      result.Add("ORDER_USER_ID", classType.GetProperty("OrderUserId"));
      result.Add("MOD_VERSION", classType.GetProperty("ModVersion"));
      return result;
    }

    /// <summary>
    /// 注文No
    /// </summary>
    public int? OrderNo { set; get; }

    /// <summary>
    /// 注文者ID
    /// </summary>
    public string OrderUserId { set; get; }

    /// <summary>
    /// 更新バージョン
    /// </summary>
    public int? ModVersion { set; get; }

  }
}
