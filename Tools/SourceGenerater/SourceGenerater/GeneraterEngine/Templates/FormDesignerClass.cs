using SourceGenerater.GeneraterEngine.Interfaces;

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
  }
}
