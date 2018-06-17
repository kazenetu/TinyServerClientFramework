using Framework.DataTransferObject.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Request.OrderEdit
{
  /// <summary>
  /// FindUserName リクエスト
  /// </summary>
  public class FindUserNameRequest : RequestBase
  {
    /// <summary>
    /// 注文者ID
    /// </summary>
    public string OrderUserID { set; get; }
  }
}
