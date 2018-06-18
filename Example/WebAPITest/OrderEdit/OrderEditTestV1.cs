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
    private ILogger<OrderEditController> logger = new LoggerFactory().CreateLogger<OrderEditController>();

    /// <summary>
    /// SQLite(メモリ)にテーブルとデータを作成、取得
    /// </summary>
    /// <returns>SQLiteDBインスタンス</returns>
    private IDatabase GetDB()
    {
      // SQLiteインスタンス作成
      var db = new TestSQLiteDB(@":memory:");

      // テーブル作成
      MtUserTest.CreateTable(db);
      TOrderTest.CreateTable(db);

      // テストデータ登録
      MtUserTest.Insert(db, "test", "テストユーザー", "Z5SMGm/kEGTiZP8tHwuWSwYWFguMP7/qJOnLNL1u4is=", "0", "", DateTime.Parse("2018/01/21 17:32:00"), "", new DateTime(), 1);
      MtUserTest.Insert(db, "test2", "テストユーザー２", "Z5SMGm/kEGTiZP8tHwuWSwYWFguMP7/qJOnLNL1u4is=", "0", "", DateTime.Parse("2018/01/21 17:32:00"), "", new DateTime(), 1);
      MtUserTest.Insert(db, "dummy", "ダミーユーザー", "Z5SMGm/kEGTiZP8tHwuWSwYWFguMP7/qJOnLNL1u4is=", "0", "", DateTime.Parse("2018/01/21 17:32:00"), "", new DateTime(), 1);

      TOrderTest.Insert(db, 1, "test", 1);
      TOrderTest.Insert(db, 2, "test2", 1);
      TOrderTest.Insert(db, 3, "dummy", 1);
      TOrderTest.Insert(db, 4, string.Empty, 1);

      // SQLiteインスタンスを返す
      return db;
    }
  }
}
