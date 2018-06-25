using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataTransferObjects.Tables;
using Framework.WebAPI.Interfaces;
using System.Linq;

namespace WebAPITest.TestTables
{
  public class MtUserTest
  {
    /// <summary>
    /// テーブル作成
    /// </summary>
    /// <param name="db">テスト用SQLiteDBインスタンス</param>
    public static void CreateTable(IDatabase db)
    {
      var sql =  @"CREATE TABLE MT_USER(USER_ID TEXT,USER_NAME TEXT,PASSWORD TEXT,DEL_FLAG TEXT,ENTRY_USER TEXT,ENTRY_DATE TEXT,MOD_USER TEXT,MOD_DATE TEXT,MOD_VERSION INTEGER);";

      try
      {
        // SQL発行
        db.ExecuteNonQuery(sql);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        db.Rollback();
        db.BeginTransaction();
      }
    }

    /// <summary>
    /// データ登録
    /// </summary>
    /// <param name="db">テスト用SQLiteDBインスタンス</param>
    /// <param name="targetDTO">対象DTOインスタンス</param>
    public static void Insert(IDatabase db, MtUser targetDTO)
    {
      // Param設定
      db.ClearParam();
      var insertColumns = new List<string>();
      insertColumns.Add(SetParam("USER_ID", targetDTO.UserId));
      insertColumns.Add(SetParam("USER_NAME", targetDTO.UserName));
      insertColumns.Add(SetParam("PASSWORD", targetDTO.Password));
      insertColumns.Add(SetParam("DEL_FLAG", targetDTO.DelFlag));
      insertColumns.Add(SetParam("ENTRY_USER", targetDTO.EntryUser));
      insertColumns.Add(SetParam("ENTRY_DATE", targetDTO.EntryDate));
      insertColumns.Add(SetParam("MOD_USER", targetDTO.ModUser));
      insertColumns.Add(SetParam("MOD_DATE", targetDTO.ModDate));
      insertColumns.Add(SetParam("MOD_VERSION", targetDTO.ModVersion));

      // SQL作成
      var targetColumns = insertColumns.Where(item => item != string.Empty).ToList();
      var sql = $"INSERT INTO MT_USER({string.Join(',', targetColumns)}) VALUES ({string.Join(',', targetColumns.Select(item => "@" + item))});";

      // SQL発行
      db.ExecuteNonQuery(sql);

      // パラメータの設定
      string SetParam(string columnName, object columnValue)
      {
        if (columnValue == null)
        {
          return string.Empty;
        }

        if (columnValue is DateTime dt)
        {
          if (dt == DateTime.MinValue)
          {
            return string.Empty;
          }
          db.AddParam("@" + columnName, columnValue);
          return columnName;
        }

        if (columnName == null)
        {
          return string.Empty;
        }

        db.AddParam("@" + columnName, columnValue);
        return columnName;
      }
    }

    /// <summary>
    /// データ取得
    /// </summary>
    /// <param name="db">テスト用SQLiteDBインスタンス</param>
    /// <param name="UserId">ユーザーID</param>
    /// <param name="UserName">ユーザー名</param>
    /// <param name="Password">パスワード</param>
    /// <param name="DelFlag">削除フラグ</param>
    /// <param name="EntryUser">登録ユーザー</param>
    /// <param name="EntryDate">登録日時</param>
    /// <param name="ModUser">更新ユーザー</param>
    /// <param name="ModDate">更新日時</param>
    /// <param name="ModVersion">更新バージョン</param>
    /// <returns>検索結果リスト</returns>
    public static List<MtUser> Find(IDatabase db
                              , string UserId = null
                              , string UserName = null
                              , string Password = null
                              , string DelFlag = null
                              , string EntryUser = null
                              , DateTime? EntryDate = null
                              , string ModUser = null
                              , DateTime? ModDate = null
                              , int? ModVersion = null)

    {
      var sql = new StringBuilder();
      sql.AppendLine("SELECT * FROM MT_USER WHERE 1=1");

      var sqlWhere = new StringBuilder();
      if(UserId != null)
      {
        sqlWhere.AppendLine($" AND USER_ID = '{UserId}'");
      }
      if(UserName != null)
      {
        sqlWhere.AppendLine($" AND USER_NAME = '{UserName}'");
      }
      if(Password != null)
      {
        sqlWhere.AppendLine($" AND PASSWORD = '{Password}'");
      }
      if(DelFlag != null)
      {
        sqlWhere.AppendLine($" AND DEL_FLAG = '{DelFlag}'");
      }
      if(EntryUser != null)
      {
        sqlWhere.AppendLine($" AND ENTRY_USER = '{EntryUser}'");
      }
      if(EntryDate != null)
      {
        sqlWhere.AppendLine($" AND ENTRY_DATE = '{EntryDate}'");
      }
      if(ModUser != null)
      {
        sqlWhere.AppendLine($" AND MOD_USER = '{ModUser}'");
      }
      if(ModDate != null)
      {
        sqlWhere.AppendLine($" AND MOD_DATE = '{ModDate}'");
      }
      if(ModVersion != null)
      {
        sqlWhere.AppendLine($" AND MOD_VERSION = {ModVersion}");
      }
      if(sqlWhere.ToString() != string.Empty)
      {
        sql.AppendLine($" {sqlWhere.ToString()};");
      }

      // Param設定
      db.ClearParam();

      // SQL発行
      var dt = db.Fill(sql.ToString());

      // DataTableからテーブルDTOのリストに変換
      var result = new List<MtUser>();
      foreach(DataRow row in dt.Rows)
      {
        var rowData = new MtUser();

        rowData.UserId =  row["USER_ID"].ToString();
        rowData.UserName =  row["USER_NAME"].ToString();
        rowData.Password =  row["PASSWORD"].ToString();
        rowData.DelFlag =  row["DEL_FLAG"].ToString();
        rowData.EntryUser =  row["ENTRY_USER"].ToString();
        rowData.EntryDate =  DateTime.Parse(row["ENTRY_DATE"].ToString());
        rowData.ModUser =  row["MOD_USER"].ToString();
        rowData.ModDate =  DateTime.Parse(row["MOD_DATE"].ToString());
        rowData.ModVersion =  int.Parse(row["MOD_VERSION"].ToString());
        result.Add(rowData);
      }

      return result;
    }
  }
}