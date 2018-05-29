using SourceGenerater.GeneraterEngine.Interfaces;
using System.Text;

namespace SourceGenerater.GeneraterEngine.Templates
{
  /// <summary>
  /// Formテンプレート
  /// </summary>
  partial class Form: IForm
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
        return $"Forms\\{BaseName}Form.cs";
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
        var sb = new StringBuilder();
        sb.AppendLine($"    <Compile Include=\"Forms\\{BaseName}Form.cs\">");
        sb.AppendLine($"      <SubType>Form</SubType>");
        sb.AppendLine($"    </Compile>");

        return sb.ToString();
      }
    }

    /// <summary>
    /// WebAPIバージョン
    /// </summary>
    /// <remarks>使用しない場合はstring.Empty</remarks>
    public string WebAPIVersion { get; set; } = string.Empty;
  }
}
