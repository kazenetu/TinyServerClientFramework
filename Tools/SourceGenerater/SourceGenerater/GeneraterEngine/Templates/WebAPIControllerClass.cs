using SourceGenerater.GeneraterEngine.Interfaces;

namespace SourceGenerater.GeneraterEngine.Templates
{
  partial class WebAPIController : IForm
  {
    public string BaseName { set; get; }
    public string CreateFileName
    {
      get
      {
        return $"..\\..\\WebAPI\\WebAPI\\Controllers\\{WebAPIVersion.ToUpper()}\\{BaseName}\\{BaseName}Controller.cs";
      }
    }
    public string ProjectElement
    {
      get
      {
        return string.Empty;
      }
    }
    public string WebAPIVersion { get; set; } = "v1";
    public string BasePath { set; get; } = string.Empty;
  }
}
