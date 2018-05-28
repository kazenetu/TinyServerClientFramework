using SourceGenerater.GeneraterEngine.Interfaces;

namespace SourceGenerater.GeneraterEngine.Templates
{
  partial class Request : IMethod
  {
    public string BaseName { set; get; }
    public string CreateFileName
    {
      get
      {
        return $"..\\..\\DataTransferObjects\\Request\\{BaseName}\\{MethodName}Request.cs";
      }
    }
    public string ProjectElement
    {
      get
      {
        return string.Empty;
      }
    }
    public string MethodName { set; get; }
    public string WebAPIVersion { get; set; } = string.Empty;
  }
}
