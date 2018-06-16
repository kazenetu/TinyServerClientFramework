using DataTransferObjects.Tables;

namespace DataTransferObjects.CustomTables
{
  /// <summary>
  /// 注文一覧用カスタムテーブルDTO
  /// </summary>
  public class CustomTOrder : TOrder
  {
    /// <summary>
    /// 注文者名
    /// </summary>
    public string OrderUserName { set; get; }
  }
}
