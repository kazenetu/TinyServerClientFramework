using WebAPIFramework.Interfaces;

namespace WebAPIFramework.BaseClasses
{
  public class TransactionBase
  {
    protected IRepositoryBase repository = null;

    public TransactionBase(IRepositoryBase repository)
    {
      this.repository = repository;
    }
  }
}
