using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataTransferObjects.Tables;
using Framework.WebAPI.Interfaces;
using System.Linq;

namespace WebAPITest.TestTables
{
  public class TOrderTest
  {
    /// <summary>
    /// テーブル作成
    /// </summary>
    /// <param name="db">テスト用SQLiteDBインスタンス</param>
    public static void CreateTable(IDatabase db)
    {
      var sql =  @"CREATE TABLE T_ORDER(ORDER_NO INTEGER,ORDER_USER_ID TEXT,MOD_VERSION INTEGER);";

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
    public static void Insert(IDatabase db, TOrder targetDTO)
    {
      // Param設定
      db.ClearParam();
      var insertColumns = new List<string>();
      insertColumns.Add(SetParam("ORDER_NO", targetDTO.OrderNo));
      insertColumns.Add(SetParam("ORDER_USER_ID", targetDTO.OrderUserId));
      insertColumns.Add(SetParam("MOD_VERSION", targetDTO.ModVersion));

      // SQL作成
      var targetColumns = insertColumns.Where(item => item != string.Empty).ToList();
      var sql = $"INSERT INTO T_ORDER({string.Join(',', targetColumns)}) VALUES ({string.Join(',', targetColumns.Select(item => "@" + item))});";

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
    /// <param name="OrderNo">注文No</param>
    /// <param name="OrderUserId">注文者ID</param>
    /// <param name="ModVersion">更新バージョン</param>
    /// <returns>検索結果リスト</returns>
    public static List<TOrder> Find(IDatabase db
                              , int? OrderNo = null
                              , string OrderUserId = null
                              , int? ModVersion = null)

    {
      var sql = new StringBuilder();
      sql.AppendLine("SELECT * FROM T_ORDER WHERE 1=1");

      var sqlWhere = new StringBuilder();
      if(OrderNo != null)
      {
        sqlWhere.AppendLine($" AND ORDER_NO = {OrderNo}");
      }
      if(OrderUserId != null)
      {
        sqlWhere.AppendLine($" AND ORDER_USER_ID = '{OrderUserId}'");
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
      var result = new List<TOrder>();
      foreach(DataRow row in dt.Rows)
      {
        var rowData = new TOrder();

        rowData.OrderNo =  int.Parse(row["ORDER_NO"].ToString());
        rowData.OrderUserId =  row["ORDER_USER_ID"].ToString();
        rowData.ModVersion =  int.Parse(row["MOD_VERSION"].ToString());
        result.Add(rowData);
      }

      return result;
    }
  }
}