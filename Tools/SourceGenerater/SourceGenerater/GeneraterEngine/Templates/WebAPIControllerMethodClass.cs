using SourceGenerater.GeneraterEngine.Interfaces;

namespace SourceGenerater.GeneraterEngine.Templates
{
  /// <summary>
  /// WebAPIControllerのメソッドテンプレート
  /// </summary>
  partial class WebAPIControllerMethod : IMethod
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
        return $"..\\..\\WebAPI\\WebAPI\\Controllers\\{WebAPIVersion.ToUpper()}\\{BaseName}\\{MethodName}.cs";
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
    /// 機能ID(メソッド名)
    /// </summary>
    public string MethodName { set; get; }

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
    /// メソッドキーワード
    /// </summary>
    /// <returns>旧バージョンがある場合はoverride、ない場合はvirtual</returns>
    public string GetMethodKeyword()
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
          return "override";
        }

        beforeVersion -= 1;
      }

      return "virtual";
    }

  }
}
