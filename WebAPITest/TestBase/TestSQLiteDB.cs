using Commons.DB;
using Microsoft.Data.Sqlite;

namespace XUnitTestProject1.TestBase
{
  /// <summary>
  /// テスト用SQLiteクラス
  /// </summary>
  /// <remarks>インメモリで実行用</remarks>
  public class TestSQLiteDB : SQLiteDB
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="connectionString">接続文字列のコア部分</param>
    public TestSQLiteDB(string connectionString) : base(connectionString)
    {
    }

    /// <summary>
    /// 接続文字列取得
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    protected override SqliteConnection getConnection(string connectionString)
    {
      return new SqliteConnection($"Data Source={connectionString}");
    }
  }
}
