namespace Commons.ConfigModel
{
  /// <summary>
  /// 設定ファイル：DB
  /// </summary>
  public class DatabaseConfigModel
  {
    /// <summary>
    /// DBタイプ
    /// </summary>
    /// <returns>DB種類</returns>
    public string Type { set; get; }

    /// <summary>
    /// 接続文字列
    /// </summary>
    /// <returns>接続文字列</returns>
    public string connectionString { set; get; }
  }
}
