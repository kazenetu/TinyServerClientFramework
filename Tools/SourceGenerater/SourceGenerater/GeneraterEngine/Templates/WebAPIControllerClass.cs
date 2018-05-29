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

    public string GetSuperClassName()
    {
      var reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
      var nowVersion = reg.Replace(WebAPIVersion, string.Empty);
      var beforeVersion = int.Parse(nowVersion) - 1;
      while (beforeVersion > 0)
      {
        var targetFile = System.IO.Path.GetFullPath(System.IO.Path.Combine(BasePath, CreateFileName));
        targetFile = targetFile.Replace(WebAPIVersion.ToUpper(), $"V{beforeVersion}");
        if (System.IO.File.Exists(targetFile))
        {
          return $"WebAPI.Controllers.V{beforeVersion}.{BaseName}Controller";
        }

        beforeVersion -= 1;
      }

      return "ControllerWithRepositoryBase";
    }
  }
}
