using SourceGenerater.GeneraterEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGenerater.GeneraterEngine.Templates
{
  partial class Business : IForm
  {
    public string BaseName { set; get; }
    public string CreateFileName
    {
      get
      {
        return $"Business\\{BaseName}\\{BaseName}Business.cs";
      }
    }
    public string ProjectElement
    {
      get
      {
        return $"    <Compile Include=\"Business\\{BaseName}\\{BaseName}Business.cs\" />";
      }
    }
  }
}
