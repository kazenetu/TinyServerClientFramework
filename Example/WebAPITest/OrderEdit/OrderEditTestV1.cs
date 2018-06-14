using Microsoft.Extensions.Logging;
using System;
using WebAPI.Controllers.V1.OrderEdit;
using Framework.WebAPI.Interfaces;
using WebAPITest.TestBase;
using WebAPITest.TestTables;

namespace WebAPITest.OrderEdit
{
  /// <summary>
  /// WebAPI.Controllers.V1.OrderEdit.OrderEditController用テストクラス
  /// </summary>
  public partial class OrderEditTestV1
  {
    /// <summary>
    /// ログ インスタンス
    /// </summary>
    private ILogger<OrderEditController> logger = new LoggerFactory().AddConsole().CreateLogger<OrderEditController>();

    /// <summary>
    /// SQLite(メモリ)にテーブルとデータを作成、取得
    /// </summary>
    /// <returns>SQLiteDBインスタンス</returns>
    private IDatabase GetDB()
    {
      // SQLiteインスタンス作成
      var db = new TestSQLiteDB(@":memory:");

      // テーブル作成
      // TODO TestTablesのクラスを使ってテーブルを作成してください。(本コメントは削除してください)
      //(本コメントは削除してください) 例) MtUserTest.CreateTable(db);

      // テストデータ登録
      // TODO TestTablesのクラスを使ってテストデータを登録してください。(本コメントは削除してください)
      //(本コメントは削除してください) 例) MtUserTest.Insert(db, "test", "テストユーザー", "Z5SMGm/kEGTiZP8tHwuWSwYWFguMP7/qJOnLNL1u4is=", "0", "", DateTime.Parse("2018/01/21 17:32:00"), "", new DateTime(), 1);

      // SQLiteインスタンスを返す
      return db;
    }
  }
}
