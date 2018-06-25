using Microsoft.Extensions.Logging;
using System;
using WebAPI.Controllers.OrderEdit;
using Framework.WebAPI.Interfaces;
using WebAPITest.TestBase;
using WebAPITest.TestTables;
using DataTransferObjects.Tables;

namespace WebAPITest.OrderEdit
{
  /// <summary>
  /// WebAPI.Controllers.OrderEdit.OrderEditController用テストクラス
  /// </summary>
  public partial class OrderEditTest : IDisposable
  {
    /// <summary>
    /// ログ インスタンス
    /// </summary>
    private ILogger<OrderEditController> logger = new LoggerFactory().CreateLogger<OrderEditController>();

    private IDatabase db = null;

    public OrderEditTest()
    {
      // テストDBインスタンス作成
      db = TestDB.GetDB();
      db.BeginTransaction();

      // テーブル作成
      MtUserTest.CreateTable(db);
      TOrderTest.CreateTable(db);

      // テーブル作成(すでにテーブルが存在する場合はdbを使ってtruncateしてください)
      MtUserTest.Insert(db, new MtUser() { UserId = "test", UserName = "テストユーザー" });
      MtUserTest.Insert(db, new MtUser() { UserId = "test2", UserName = "テストユーザー２" });
      MtUserTest.Insert(db, new MtUser() { UserId = "dummy", UserName = "ダミーユーザー" });

      TOrderTest.Insert(db, new TOrder() { OrderNo = 1, OrderUserId = "test", ModVersion = 1 });
      TOrderTest.Insert(db, new TOrder() { OrderNo = 2, OrderUserId = "test2", ModVersion = 1 });
      TOrderTest.Insert(db, new TOrder() { OrderNo = 3, OrderUserId = "dummy", ModVersion = 1 });
      TOrderTest.Insert(db, new TOrder() { OrderNo = 4, OrderUserId = string.Empty, ModVersion = 1 });
    }

    public void Dispose()
    {
      db.Rollback();
      db.Dispose();
    }

    /// <summary>
    /// テスト用テーブルとデータを作成、取得
    /// </summary>
    /// <returns>テストDBインスタンス</returns>
    private IDatabase GetDB()
    {
      // テストDBインスタンスを返す
      return db;
    }
  }
}
