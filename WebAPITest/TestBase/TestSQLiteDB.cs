using Commons.DB;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

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
