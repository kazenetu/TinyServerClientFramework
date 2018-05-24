using SourceGenerater.GeneraterEngine.Interfaces;

namespace SourceGenerater.GeneraterEngine.Templates
{
  partial class BusinessMethod : IMethod
  {
    public string BaseName { set; get; }
    public string CreateFileName
    {
      get
      {
        return $"Business\\{BaseName}\\{MethodName}.cs";
      }
    }
    public string ProjectElement
    {
      get
      {
        return $"    <Compile Include=\"Business\\{BaseName}\\{MethodName}.cs\" />";
      }
    }
    public string MethodName { set; get; }
  }
}
