using Framework.DataTransferObject.BaseClasses;

namespace DataTransferObjects.Request.OrderEdit
{
  /// <summary>
  /// Initialize リクエスト
  /// </summary>
  public class InitializeRequest : RequestBase
  {
    /// <summary>
    /// 注文番号
    /// </summary>
    public int OrderNo { set; get; }
  }
}
