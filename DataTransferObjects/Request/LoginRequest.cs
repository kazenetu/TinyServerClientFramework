using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Request
{
  /// <summary>
  /// ログインリクエスト
  /// </summary>
  public class LoginRequest
  {
    /// <summary>
    /// ログインID
    /// </summary>
    public string ID { set; get; }

    /// <summary>
    /// パスワード
    /// </summary>
    public string Password { set; get; }
  }
}
