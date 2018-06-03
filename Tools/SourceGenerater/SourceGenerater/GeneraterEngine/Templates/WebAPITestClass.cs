using SourceGenerater.GeneraterEngine.Interfaces;

namespace SourceGenerater.GeneraterEngine.Templates
{
  /// <summary>
  /// WebAPI用テストテンプレート
  /// </summary>
  partial class WebAPITest : IForm
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
        return $"..\\..\\WebAPITest\\{BaseName}\\{BaseName}Test{WebAPIVersion.ToUpper()}.cs";
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
  }
}
