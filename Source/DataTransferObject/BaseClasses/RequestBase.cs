using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Framework.DataTransferObject.BaseClasses
{
  /// <summary>
  /// リクエストのスーパークラス
  /// </summary>  
  public class RequestBase
  {
    #region インスタンスプロパティ

    /// <summary>
    /// 入力チェックNGとなったプロパティ名
    /// </summary>
    public string ValidateNGPropertyName { get; protected set; } = string.Empty;

    /// <summary>
    /// 共通パラメータ：ログインユーザーID
    /// </summary>
    public string LoginUserID { set; get; }

    /// <summary>
    /// 共通パラメータ：対象バージョン
    /// </summary>
    public string TargetVersion { set; get; }

    #endregion

    #region メソッド

    /// <summary>
    /// 必須入力チェック
    /// </summary>
    /// <returns>結果</returns>
    /// <remarks>任意の確認はサブクラスでオーバーライドする</remarks>
    public virtual bool Validate()
    {
      // 必須プロパティのチェックを行う
      var properties = this.GetType().GetProperties().Where(property => property.CustomAttributes.Any(attr=> attr.AttributeType == typeof(RequiredAttribute)));
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
    #endregion
  }
}
