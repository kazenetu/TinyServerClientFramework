using Framework.DataTransferObject.BaseClasses;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using Framework.WebAPI.ConfigModel;
using Framework.WebAPI.DB;
using Framework.WebAPI.Interfaces;

namespace Framework.WebAPI.BaseClasses
{
  /// <summary>
  /// Repositoryのスーパークラス
  /// </summary>
  public class RepositoryBase : IRepositoryBase, IDisposable
  {
    /// <summary>
    /// データを永続化するDB用インターフェース
    /// </summary>
    protected IDatabase db;

    /// <summary>
    /// DB情報
    /// </summary>
    private IOptions<DatabaseConfigModel> config;

    /// <summary>
    /// DB設定取得用コンストラクタ
    /// </summary>
    /// <param name="config">DB設定取得</param>
    public RepositoryBase(IOptions<DatabaseConfigModel> config)
    {
      this.config = config;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
      // 設定されていない場合は終了
      if(config == null)
      {
        return;
      }

      // DatabaseFactoryから永続化対象のDBインスタンスを取得
      db = DatabaseFactory.Create(config.Value);
    }

    /// <summary>
    /// DBインスタンス設定用コンストラクタ
    /// </summary>
    /// <param name="db">外部から設定されたDBインスタンス</param>
    public RepositoryBase(IDatabase db)
    {
      this.db = db;
    }

    /// <summary>
    /// 破棄
    /// </summary>
    public void Dispose()
    {
      db?.Dispose();
    }

    /// <summary>
    /// トランザクション設定
    /// </summary>
    public void BeginTransaction()
    {
      db.BeginTransaction();
    }

    /// <summary>
    /// コミット
    /// </summary>
    public void Commit()
    {
      db.Commit();
    }

    /// <summary>
    /// ロールバック
    /// </summary>
    public void Rollback()
    {
      db.Rollback();
    }

    /// <summary>
    /// サブクラスへのキャスト
    /// </summary>
    /// <typeparam name="T">サブクラス</typeparam>
    /// <returns>サブクラスのインスタンス</returns>
    public virtual T Cast<T>() where T : IRepositoryBase
    {
      // インスタンスを作成して返す
      return (T)Activator.CreateInstance(typeof(T), db);
    }

    /// <summary>
    /// DB結果をテーブルDTOのリストで返す
    /// </summary>
    /// <typeparam name="T">テーブルDTO</typeparam>
    /// <param name="dbResult">DB結果</param>
    /// <param name="callerMethodName">呼び出したメソッド名</param>
    /// <returns>テーブルDTOのリスト</returns>
    protected List<T> Fill<T>(DataTable dbResult, [CallerMemberName] string callerMethodName = "") where T : TableBase, new()
    {
      var result = new List<T>();

      var instance = new T();
      var dbColmunnProperties = instance.GetDBComlunProperyColection();
      var dbColumns = dbResult.Columns;

      foreach (DataRow row in dbResult.Rows)
      {
        instance = new T();
        foreach (DataColumn col in dbColumns)
        {
          if (dbColmunnProperties.ContainsKey(col.ColumnName))
          {
            var dbColmunnProperty = dbColmunnProperties[col.ColumnName];
            dbColmunnProperty.SetValue(instance, getColumnData(dbColmunnProperty, row[col.ColumnName]));
          }
          else
          {
            // クラスインスタンスに該当するプロパティがない場合は専用メソッドを呼び出す
            FillOhter(callerMethodName, col.ColumnName, row[col.ColumnName], instance);
          }
        }
        result.Add(instance);
      }

      // DBセルデータからプロパティデータとして取得
      object getColumnData(PropertyInfo pi, object dbValue)
      {
        // DBNullの場合はnullを返す
        if (dbValue == DBNull.Value)
        {
          return null;
        }

        // 対象プロパティのTypeを取得
        var targetType = pi.PropertyType;
        if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
          targetType = targetType.GenericTypeArguments[0];
        }

        // プロパティTypeがstringの場合はそのまま返す
        if (targetType == typeof(string))
        {
          return dbValue;
        }

        // プロパティTypeがboolの場合
        if (targetType == typeof(bool))
        {
          // パースを試みる("True"や"False")
          if (bool.TryParse(dbValue.ToString(), out bool boolResult))
          {
            return boolResult;
          }

          // 0・1の場合は値を判定する
          if (dbValue.ToString() == "1")
          {
            return true;
          }
          return false;
        }

        // それ以外(日付や日時など)の場合はキャストを試みる
        return Convert.ChangeType(dbValue, targetType);
      }

      return result;
    }

    /// <summary>
    /// インスタンスのプロパティに該当しないデータの処理
    /// </summary>
    /// <typeparam name="T">テーブルDTO</typeparam>
    /// <param name="callerMethodName">fillメソッドを呼び出したメソッド名</param>
    /// <param name="columnName">カラム名</param>
    /// <param name="columnValue">カラムの値</param>
    /// <param name="instance">クラスインスタンス</param>
    protected virtual void FillOhter<T>(string methodName, string columnName, object columnValue, T instance)
    {
    }
  }
}
