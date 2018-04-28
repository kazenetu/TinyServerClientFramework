using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Request
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
