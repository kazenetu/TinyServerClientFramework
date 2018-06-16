using Framework.DataTransferObject.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Request.OrderList
{
  /// <summary>
  /// Search リクエスト
  /// </summary>
  public class SearchRequest : RequestBase
  {
    /// <summary>
    /// 検索条件:ユーザーID　
    /// </summary>
    public string SearchUserID { set; get; }
  }
}
