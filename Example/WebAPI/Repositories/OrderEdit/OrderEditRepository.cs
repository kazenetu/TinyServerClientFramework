using DataTransferObjects.Request.OrderEdit;
using System.Text;
using Framework.WebAPI.BaseClasses;
using Framework.WebAPI.Interfaces;

namespace WebAPI.Repositories
{
  public partial class OrderEditRepository : RepositoryBase
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="db">dbインターフェース</param>
    public OrderEditRepository(IDatabase db) : base(db)
    {
    }

    /// <summary>
    /// 検索時に別名をつけたカラムのマッピング
    /// </summary>
    /// <typeparam name="T">テーブルDTO</typeparam>
    /// <param name="methodName">fillメソッドを呼び出したメソッド名</param>
    /// <param name="columnName">カラム名</param>
    /// <param name="columnValue">カラムの値</param>
    /// <param name="instance">クラスインスタンス</param>
    /// <remarks>インスタンスのプロパティに該当しないデータの処理</remarks>
    protected override void FillOhter<T>(string methodName, string columnName, object columnValue, T instance)
    {
      // HACK 必要になったら実装してください。(本コメントは削除してください)
      // メソッド名で検索
      //switch (methodName)
      //{
      //}
    }
  }
}
