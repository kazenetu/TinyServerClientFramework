using Framework.DataTransferObject.BaseClasses;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataTransferObjects.Tables
{
  /// <summary>
  /// ダミーテーブルDTO
  /// </summary>
  /// <remarks>
  /// 実際に利用するテーブルDTOまたはカスタマイズDTOに置き換えてください。
  /// </remarks>
  [Obsolete("テーブルDTOまたはカスタマイズDTOに置き換えてください。")]
  public class DummyTable : TableBase
  {
    /// <summary>
    /// DBカラム名とプロパティのコレクション取得
    /// </summary>
    /// <returns>DBカラム名とプロパティのコレクション</returns>
    public override Dictionary<string, PropertyInfo> GetDBComlunProperyColection()
    {
      var result = new Dictionary<string, PropertyInfo>();

      return result;
    }
  }
}
