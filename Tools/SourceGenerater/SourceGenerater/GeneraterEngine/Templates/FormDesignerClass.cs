using SourceGenerater.GeneraterEngine.Interfaces;
using System.Text;

namespace SourceGenerater.GeneraterEngine.Templates
{
  partial class FormDesigner: IForm
  {
    public string BaseName { set; get; }
    public string CreateFileName
    {
      get
      {
        return $"Forms\\{BaseName}Form.Designer.cs";
      }
    }
    public string ProjectElement
    {
      get
      {
        var sb = new StringBuilder();
        sb.AppendLine($"    <Compile Include=\"Forms\\{BaseName}Form.Designer.cs\">");
        sb.AppendLine($"      <DependentUpon>{BaseName}Form.cs</DependentUpon>");
        sb.AppendLine($"    </Compile>");

        return sb.ToString();
      }
    }
  }
}
