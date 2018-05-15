using System.Collections.Generic;

namespace WebAPIFramework.ConfigModel
{
  /// <summary>
  /// 設定ファイル：DB
  /// </summary>
  public class DatabaseConfigModel
  {
    /// <summary>
    /// 生成するDB
    /// </summary>
    /// <returns>DB種類</returns>
    public string Target { set; get; }

    /// <summary>
    /// DBごとの接続文字列
    /// </summary>
    /// <returns>「DB名と接続文字列」情報</returns>
    public Dictionary<string, string> ConnectionStrings { set; get; }
  }
}
