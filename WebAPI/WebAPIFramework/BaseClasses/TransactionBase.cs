﻿using Microsoft.Extensions.Logging;
using WebAPIFramework.Interfaces;

namespace WebAPIFramework.BaseClasses
{
  /// <summary>
  /// Transactioのスーパークラス
  /// </summary>
  public class TransactionBase
  {
    /// <summary>
    /// Repositoryインターフェース
    /// </summary>
    /// <remarks>
    /// Controllerから取得する
    /// </remarks>
    protected IRepositoryBase repository = null;

    /// <summary>
    /// ロガーインスタンス
    /// </summary>
    protected readonly ILogger logger;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">Repositoryインターフェース</param>
    public TransactionBase(IRepositoryBase repository, ILogger logger)
    {
      this.repository = repository;
      this.logger = logger;
    }
  }
}
