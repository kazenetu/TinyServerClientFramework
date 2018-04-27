using Commons.DB;
using Microsoft.Data.Sqlite;

namespace XUnitTestProject1.TestBase
{
  public class TestSQLiteDB : SQLiteDB
  {
    public TestSQLiteDB(string connectionString) : base(connectionString)
    {
    }

    protected override SqliteConnection getConnection(string connectionString)
    {
      return new SqliteConnection($"Data Source={connectionString}");
    }
  }
}
