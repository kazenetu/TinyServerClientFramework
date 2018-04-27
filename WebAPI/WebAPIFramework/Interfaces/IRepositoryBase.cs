namespace WebAPIFramework.Interfaces
{
  /// <summary>
  /// Repositoryのスーパークラス
  /// </summary>
  public interface IRepositoryBase
  {
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
