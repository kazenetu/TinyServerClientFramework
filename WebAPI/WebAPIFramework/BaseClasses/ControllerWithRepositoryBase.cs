using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
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
