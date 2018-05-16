namespace WebAPIFramework.Interfaces
{
  /// <summary>
  /// Repositoryのスーパークラス
  /// </summary>
  public interface IRepositoryBase
  {
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Initialize();

    /// <summary>
    /// トランザクション設定
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// コミット
    /// </summary>
    void Commit();

    /// <summary>
    /// ロールバック
    /// </summary>
    void Rollback();

    T Cast<T>() where T : IRepositoryBase;
  }
}
