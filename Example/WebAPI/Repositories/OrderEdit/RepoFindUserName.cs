using DataTransferObjects.Request.OrderEdit;
using DataTransferObjects.Tables;
using System.Collections.Generic;
using System.Text;
using Framework.WebAPI.BaseClasses;

namespace WebAPI.Repositories
{
  public partial class OrderEditRepository : RepositoryBase
  {
    /// <summary>
    /// FindUserName
    /// </summary>
    /// <param name="request">入力リクエスト</param>
    /// <returns>処理結果</returns>
    public virtual List<DummyTable> FindUserName(FindUserNameRequest request)
    {
      // TODO DummyTableからテーブルDTOまたはカスタマイズされたテーブルDTOに置き換えてください。(本コメントは削除してください)

      var sql = new StringBuilder();
      // TODO SQLを追加してください。(本コメントは削除してください)

      // Param設定
      db.ClearParam();
      // TODO db.AddParamメソッドを使って、パラメーターを追加してください。(本コメントは削除してください)

      // 結果をリストで返す
      // ※ テーブルDTOまたはカスタマイズされたテーブルDTOに存在しない別名を使用している場合はfillOhterメソッドにて設定を行うこと
      return Fill<DummyTable>(db.Fill(sql.ToString()));
    }
  }
}
