using SourceGenerater.GeneraterEngine.Interfaces;

/// <summary>
/// Businessテンプレート
/// </summary>
namespace SourceGenerater.GeneraterEngine.Templates
{
  public partial class Business : IForm
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
        return $"Business\\{BaseName}\\{BaseName}Business.cs";
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
        return $"    <Compile Include=\"Business\\{BaseName}\\{BaseName}Business.cs\" />";
      }
    }

    /// <summary>
    /// WebAPIバージョン
    /// </summary>
    /// <remarks>使用しない場合はstring.Empty</remarks>
    public string WebAPIVersion { get; set; } = string.Empty;
  }
}
