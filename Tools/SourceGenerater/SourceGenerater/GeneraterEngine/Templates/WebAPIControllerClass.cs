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
        return $"..\\WebAPI\\Controllers\\{BaseName}\\{BaseName}Controller.cs";
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

  }
}
