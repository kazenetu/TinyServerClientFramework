﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataTransferObjects.Tables;
using Framework.WebAPI.Interfaces;

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

      // SQL発行
      db.ExecuteNonQuery(sql);
    }

    /// <summary>
    /// データ登録
    /// </summary>
    /// <param name="db">テスト用SQLiteDBインスタンス</param>
    /// <param name="OrderNo">注文No</param>
    /// <param name="OrderUserId">注文者ID</param>
    /// <param name="ModVersion">更新バージョン</param>
    public static void Insert(IDatabase db
                              , int OrderNo
                              , string OrderUserId
                              , int ModVersion)
    {
      var sql = @"INSERT INTO T_ORDER(ORDER_NO,ORDER_USER_ID,MOD_VERSION) VALUES (@ORDER_NO,@ORDER_USER_ID,@MOD_VERSION);";

      // Param設定
      db.ClearParam();
      db.AddParam("@ORDER_NO",OrderNo);
      db.AddParam("@ORDER_USER_ID",OrderUserId);
      db.AddParam("@MOD_VERSION",ModVersion);

      // SQL発行
      db.ExecuteNonQuery(sql);
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
      sql.AppendLine("SELECT * FROM t_order WHERE 1=1");

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