using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Commons.Interfaces;
using Microsoft.Data.Sqlite;

namespace Commons.DB
{
  /// <summary>
  /// SQLiteラッパークラス
  /// </summary>
  public class SQLiteDB : IDatabase
  {
    #region プライベートフィールド

    /// <summary>
    /// コネクションインスタンス
    /// </summary>
    private SqliteConnection conn = null;

    /// <summary>
    /// トランザクションインスタンス
    /// </summary>
    private SqliteTransaction tran = null;

    /// <summary>
    /// パラメータ
    /// </summary>
    private Dictionary<string, object> param;

    /// <summary>
    /// トランザクションが開いているか否か
    /// </summary>
    private bool isTran = false;

    #endregion

    #region パブリックメソッド

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="connectionString">接続文字列</param>
    public SQLiteDB(string connectionString)
    {
      this.conn = this.getConnection(connectionString);
      this.conn.Open();

      this.param = new Dictionary<string, object>();
    }

    /// <summary>
    /// パラメータを追加
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">値</param>
    public void AddParam(string key, object value)
    {
      this.param.Add(key, value);
    }

    /// <summary>
    /// パラメータをクリア
    /// </summary>
    public void ClearParam()
    {
      this.param.Clear();
    }

    /// <summary>
    /// SQL実行（登録・更新・削除）
    /// </summary>
    /// <param name="sql">SQLステートメント</param>
    /// <returns>処理件数</returns>
    public int ExecuteNonQuery(string sql)
    {
      using (SqliteCommand command = conn.CreateCommand())
      {
        command.CommandText = sql;

        foreach (var key in this.param.Keys)
        {
          command.Parameters.AddWithValue(key, this.param[key]);
        }

        return command.ExecuteNonQuery();
      }
    }

    /// <summary>
    /// 検索SQL実行
    /// </summary>
    /// <param name="sql">SQLステートメント</param>
    /// <returns>検索結果</returns>
    public DataTable Fill(string sql)
    {
      using (SqliteCommand command = conn.CreateCommand())
      {
        command.CommandText = sql;

        foreach (var key in this.param.Keys)
        {
          command.Parameters.AddWithValue(key, this.param[key]);
        }

        using (SqliteDataReader reader = command.ExecuteReader())
        {
          //スキーマ取得
          var result = this.GetShcema(reader);

          while (reader.Read())
          {
            var addRow = result.NewRow();

            foreach (DataColumn col in result.Columns)
            {
              addRow[col.ColumnName] = reader[col.ColumnName];
            }

            result.Rows.Add(addRow);
          }

          return result;
        }
      }
    }

    /// <summary>
    /// トランザクション設定
    /// </summary>
    public void BeginTransaction()
    {
      this.tran = this.conn.BeginTransaction();
      this.isTran = true;
    }

    /// <summary>
    /// コミット
    /// </summary>
    public void Commit()
    {
      if (!this.isTran)
      {
        return;
      }

      this.tran.Commit();
      this.isTran = false;
    }

    /// <summary>
    /// ロールバック
    /// </summary>
    public void Rollback()
    {
      if (!this.isTran)
      {
        return;
      }

      this.tran.Rollback();
      this.isTran = false;
    }

    /// <summary>
    /// 解放処理
    /// </summary>
    public void Dispose()
    {
      if (this.isTran)
      {
        this.tran.Rollback();
      }
      if(this.tran != null){
        this.tran.Dispose();
      }
      this.conn.Close();
      this.conn.Dispose();
    }

    #endregion

    #region プライベートメソッド

    /// <summary>
    /// コネクション生成
    /// </summary>
    /// <param name="connectionString">接続文字列</param>
    /// <returns>コネクションインスタンス</returns>
    private SqliteConnection getConnection(string connectionString)
    {
      var resourcePath = AppContext.BaseDirectory;
      resourcePath = String.Format("Data Source={0}", Path.Combine(resourcePath, connectionString));

      return new SqliteConnection(resourcePath);
    }

    /// <summary>
    /// スキーマ取得
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    private DataTable GetShcema(SqliteDataReader reader)
    {
      var result = new DataTable();

      for (var i = 0; i < reader.FieldCount; i++)
      {
        result.Columns.Add(new DataColumn(reader.GetName(i)));
      }

      return result;
    }
    #endregion
  }
}