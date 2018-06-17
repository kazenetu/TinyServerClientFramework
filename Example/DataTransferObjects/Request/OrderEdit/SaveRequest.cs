using Framework.DataTransferObject.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Request.OrderEdit
{
  /// <summary>
  /// Save リクエスト
  /// </summary>
  public class SaveRequest : RequestBase
  {
    /// <summary>
    /// 注文番号
    /// </summary>
    [Required]
    public int OrderNo { set; get; }

    /// <summary>
    /// 注文者ID
    /// </summary>
    [Required]
    public string OrderUserID { set; get; }

    /// <summary>
    /// 更新バージョン
    /// </summary>
    [Required]
    public int ModVersion { set; get; }
  }
}
