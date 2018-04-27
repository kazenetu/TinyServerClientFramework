using Microsoft.AspNetCore.Mvc;
using WebAPIFramework.Interfaces;

namespace WebAPIFramework.BaseClasses
{
  public class ControllerWithRepositoryBase : Controller
  {
    protected IRepositoryBase repository = null;
    public ControllerWithRepositoryBase(IRepositoryBase repository)
    {
      this.repository = repository;
    }
  }
}
