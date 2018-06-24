using Microsoft.Data.Sqlite;
using Framework.WebAPI.DB;
using System;

namespace WebAPITest.TestBase
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
    /// パラメータを追加
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">値</param>
    public override void AddParam(string key, object value)
    {
      switch (value)
      {
        case DateTime dt:
          this.param.Add(key, dt.ToString("yyyy-MM-dd HH:mm:ss"));
          break;
        case object obj:
          this.param.Add(key, obj);
          break;
        default:
          if (value == null)
          {
            value = string.Empty;
          }
          this.param.Add(key, value);
          break;
      }
    }

    /// <summary>
    /// 接続文字列取得
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    protected override SqliteConnection GetConnection(string connectionString)
    {
      return new SqliteConnection($"Data Source={connectionString}");
    }
  }
}
