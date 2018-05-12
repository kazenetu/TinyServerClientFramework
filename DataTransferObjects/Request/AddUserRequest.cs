using DataTransferObjects.Request.BaseClasses;

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
    public string UserId { set; get; }

    /// <summary>
    /// ユーザー名
    /// </summary>
    public string UserName { set; get; }

    /// <summary>
    /// パスワード
    /// </summary>
    public string Password { set; get; }
  }
}
