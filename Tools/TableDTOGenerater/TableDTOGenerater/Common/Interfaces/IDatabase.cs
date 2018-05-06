using System;
using System.Data;
using static WebAPIFramework.DB.DatabaseFactory;

namespace WebAPIFramework.Interfaces
{
  /// <summary>
  /// データベース用インターフェース
  /// </summary>
  public interface IDatabase : IDisposable
  {
    /// <summary>
    /// DB種類
    /// </summary>
    DatabaseTypes DBType { get; }

    /// <summary>
    /// SELECTした行の一部だけを取り出すSQLの文字列を返す
    /// </summary>
    /// <param name="limit">取り出す行数</param>
    /// <param name="offset">スキップ行</param>
    /// <returns></returns>
    string GetLimitSQL(long limit, long offset);

    /// <summary>
    /// パラメータを追加
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">値</param>
    void AddParam(string key, object value);

    /// <summary>
    /// パラメータをクリア
    /// </summary>
    void ClearParam();

    /// <summary>
    /// SQL実行（登録・更新・削除）
    /// </summary>
    /// <param name="sql">SQLステートメント</param>
    /// <returns>処理件数</returns>
    int ExecuteNonQuery(string sql);

    /// <summary>
    /// 検索SQL実行
    /// </summary>
    /// <param name="sql">SQLステートメント</param>
    /// <returns>検索結果</returns>
    DataTable Fill(string sql);

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

  }
}