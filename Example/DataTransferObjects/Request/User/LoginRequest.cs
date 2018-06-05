using DataTransferObjects.Request.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Request.User
{
  /// <summary>
  /// ログインリクエスト
  /// </summary>
  public class LoginRequest : RequestBase
  {
    /// <summary>
    /// ログインID
    /// </summary>
    [Required]
    public string ID { set; get; }

    /// <summary>
    /// パスワード
    /// </summary>
    [Required]
    public string Password { set; get; }
  }
}
