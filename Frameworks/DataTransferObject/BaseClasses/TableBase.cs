using System.Collections.Generic;
using System.Reflection;

namespace Framework.DataTransferObject.BaseClasses
{
  /// <summary>
  /// テーブルDTOのスーパークラス
  /// </summary>
  public abstract class TableBase
  {
    #region 定数フィールド

    /// <summary>
    /// DB文字列 True
    /// </summary>
    public const string StringTrue = "1";

    /// <summary>
    /// DB文字列 False
    /// </summary>
    public const string StringFalse = "0";

    #endregion

    /// <summary>
    /// DBカラム名とプロパティのコレクション取得
    /// </summary>
    /// <returns>DBカラム名とプロパティのコレクション</returns>
    public abstract Dictionary<string, PropertyInfo> GetDBComlunProperyColection();
  }
}
