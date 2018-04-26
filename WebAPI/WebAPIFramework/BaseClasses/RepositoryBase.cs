using Commons.ConfigModel;
using Commons.DB;
using Commons.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPIFramework.Interfaces;

namespace WebAPIFramework.BaseClasses
{
  /// <summary>
  /// Repositoryのスーパークラス
  /// </summary>
  public class RepositoryBase : IRepositoryBase, IDisposable
  {
    protected IDatabase db;

    public RepositoryBase(IOptions<DatabaseConfigModel> config)
    {
      db = DatabaseFactory.Create(config.Value);
    }

    public RepositoryBase(IDatabase db)
    {
      this.db = db;
    }

    public void Dispose()
    {
      db.Dispose();
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

    public T Cast<T>() where T : IRepositoryBase
    {
      return (T)Activator.CreateInstance(typeof(T), db);
    }
  }
}
