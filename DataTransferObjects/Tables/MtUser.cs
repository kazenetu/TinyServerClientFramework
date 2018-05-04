using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataTransferObjects.Tables
{
  /// <summary>
  /// ユーザーマスタ―
  /// </summary>
  public class MtUser
  {
    /// <summary>
    /// ユーザーID
    /// </summary>
    [Column("USER_ID")]
    public string UserId { set; get; }

    /// <summary>
    /// ユーザー名
    /// </summary>
    [Column("USER_NAME")]
    public string UserName { set; get; }

    /// <summary>
    /// パスワード
    /// </summary>
    [Column("PASSWORD")]
    public string Password { set; get; }

    /// <summary>
    /// 削除フラグ
    /// </summary>
    [Column("DEL_FLAG")]
    public bool DelFlag { set; get; }

    /// <summary>
    /// 登録ユーザー
    /// </summary>
    [Column("ENTRY_USER")]
    public string EntryUser{ set; get; }

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column("ENTRY_DATE")]
    public DateTime EntryDate { set; get; }

    /// <summary>
    /// 更新ユーザー
    /// </summary>
    [Column("MOD_USER")]
    public string ModUser { set; get; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column("MOD_DATE")]
    public DateTime ModDate { set; get; }

    /// <summary>
    /// 更新バージョン
    /// </summary>
    [Column("MOD_VERSION")]
    public int ModVersion { set; get; }
  }
}
