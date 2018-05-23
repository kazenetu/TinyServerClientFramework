using DataTransferObjects.Request.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Request
{
  /// <summary>
  /// ユーザー追加リクエスト
  /// </summary>
  public class AddUserRequest : RequestBase
  {
    /// <summary>
    /// ユーザーID
    /// </summary>
    [Required]
    public string UserId { set; get; }

    /// <summary>
    /// ユーザー名
    /// </summary>
    [Required]
    public string UserName { set; get; }

    /// <summary>
    /// パスワード
    /// </summary>
    [Required]
    public string Password { set; get; }
  }
}
