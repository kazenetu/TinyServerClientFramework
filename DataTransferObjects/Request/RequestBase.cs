using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataTransferObjects.Request
{
  /// <summary>
  /// リクエストのスーパークラス
  /// </summary>  
  public class RequestBase
  {
    // 入力チェックNGとなったプロパティ名
    public string ValidateNGPropertyName { get; protected set; } = string.Empty;

    /// <summary>
    /// 必須入力チェック
    /// </summary>
    /// <returns>結果</returns>
    /// <remarks>任意の確認はサブクラスでオーバーライドする</remarks>
    public virtual bool Validate()
    {
      // 必須プロパティのチェックを行う
      var properties = this.GetType().GetProperties().Where(property => property.GetCustomAttributes(typeof(RequiredAttribute),false) != null);
      foreach (var property in properties)
      {
        // nullの場合はチェックNG
        if(property.GetValue(this) == null)
        {
          ValidateNGPropertyName = property.Name;
          return false;
        }
      }

      return true;
    }
  }
}
