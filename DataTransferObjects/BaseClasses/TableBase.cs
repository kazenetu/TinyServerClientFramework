using System.Collections.Generic;
using System.Reflection;

namespace DataTransferObjects.BaseClasses
{
  /// <summary>
  /// テーブルDTOのスーパークラス
  /// </summary>
  public abstract class TableBase
  {
    /// <summary>
    /// DBカラム名とプロパティのコレクション取得
    /// </summary>
    /// <returns>DBカラム名とプロパティのコレクション</returns>
    public abstract Dictionary<string, PropertyInfo> GetDBComlunProperyColection();
  }
}
