using DataTransferObjects.Tables;
using Framework.DataTransferObject.BaseClasses;
using Framework.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
      var sql = $"INSERT INTO t_order({string.Join(',', targetColumns)}) VALUES ({string.Join(',', targetColumns.Select(item => "@" + item))});";

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

        SetData(rowData, "OrderNo", row, "ORDER_NO");
        SetData(rowData, "OrderUserId", row, "ORDER_USER_ID");
        SetData(rowData, "ModVersion", row, "MOD_VERSION");
        result.Add(rowData);
      }

      return result;

      // レコードデータ変換・取得
      void SetData(TableBase tableClass, string propertyName, DataRow row, string columnName)
      {
        var propertyInfo = tableClass.GetType().GetProperty(propertyName);

        // レコ―ドデータがDBNull
        if (row[columnName] is DBNull)
        {
          return;
        }

        // レコードデータが入っている場合は型変換して返す
        propertyInfo.SetValue(tableClass, row[columnName]);
      }
    }
  }
}