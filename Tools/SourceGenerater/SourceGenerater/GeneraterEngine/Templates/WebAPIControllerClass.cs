using SourceGenerater.GeneraterEngine.Interfaces;

namespace SourceGenerater.GeneraterEngine.Templates
{
  /// <summary>
  /// WebAPIControllerテンプレート
  /// </summary>
  partial class WebAPIController : IForm
  {
    /// <summary>
    /// 画面ID
    /// </summary>
    public string BaseName { set; get; }

    /// <summary>
    /// 出力ファイルパス
    /// </summary>
    public string CreateFileName
    {
      get
      {
        return $"..\\..\\WebAPI\\WebAPI\\Controllers\\{WebAPIVersion.ToUpper()}\\{BaseName}\\{BaseName}Controller.cs";
      }
    }

    /// <summary>
    /// .NET Frameworkプロジェクト追加用要素
    /// </summary>
    /// <remarks>追加しない場合はstring.Empty</remarks>
    public string ProjectElement
    {
      get
      {
        return string.Empty;
      }
    }
    /// <summary>
    /// WebAPIバージョン
    /// </summary>
    /// <remarks>使用しない場合はstring.Empty</remarks>
    public string WebAPIVersion { get; set; } = "v1";

    /// <summary>
    /// ルートパス(slnファイルのフォルダ)
    /// </summary>
    /// <remarks>旧バージョンのメソッドがあるかファイル確認をおこなうため</remarks>
    public string BasePath { set; get; } = string.Empty;

    /// <summary>
    /// スーパークラス取得
    /// </summary>
    /// <returns>旧バージョンがある場合はそのクラス、ない場合は大本のスーパークラス</returns>
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
