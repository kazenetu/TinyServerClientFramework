using Framework.DataTransferObject.BaseClasses;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataTransferObjects.Tables
{
  /// <summary>
  /// ユーザーマスタ―
  /// </summary>
  public class MtUser : TableBase
  {
    /// <summary>
    /// DBカラム名とプロパティのコレクション取得
    /// </summary>
    /// <returns>DBカラム名とプロパティのコレクション</returns>
    public override Dictionary<string, PropertyInfo> GetDBComlunProperyColection()
    {
      var result = new Dictionary<string, PropertyInfo>();

      var classType = this.GetType();
      result.Add("USER_ID", classType.GetProperty("UserId"));
      result.Add("USER_NAME", classType.GetProperty("UserName"));
      result.Add("PASSWORD", classType.GetProperty("Password"));
      result.Add("DEL_FLAG", classType.GetProperty("DelFlag"));
      result.Add("ENTRY_USER", classType.GetProperty("EntryUser"));
      result.Add("ENTRY_DATE", classType.GetProperty("EntryDate"));
      result.Add("MOD_USER", classType.GetProperty("ModUser"));
      result.Add("MOD_DATE", classType.GetProperty("ModDate"));
      result.Add("MOD_VERSION", classType.GetProperty("ModVersion"));
      return result;
    }

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

    /// <summary>
    /// 削除フラグ
    /// </summary>
    public string DelFlag { set; get; }

    /// <summary>
    /// 登録ユーザー
    /// </summary>
    public string EntryUser { set; get; }

    /// <summary>
    /// 登録日時
    /// </summary>
    public DateTime? EntryDate { set; get; }

    /// <summary>
    /// 更新ユーザー
    /// </summary>
    public string ModUser { set; get; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime? ModDate { set; get; }

    /// <summary>
    /// 更新バージョン
    /// </summary>
    public int? ModVersion { set; get; }

  }
}
