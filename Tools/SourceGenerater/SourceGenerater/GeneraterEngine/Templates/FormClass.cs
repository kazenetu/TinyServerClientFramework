using SourceGenerater.GeneraterEngine.Interfaces;
using System.Text;

namespace SourceGenerater.GeneraterEngine.Templates
{
  partial class Form: IForm
  {
    public string BaseName { set; get; }
    public string CreateFileName
    {
      get
      {
        return $"Forms\\{BaseName}Form.cs";
      }
    }
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
    public string WebAPIVersion { get; set; } = string.Empty;
  }
}
