namespace SourceGenerater.GeneraterEngine.Interfaces
{
  interface IForm: ITransformText
  {
    string BaseName { set; get; }
    string CreateFileName { get; }
    string ProjectElement { get; }
  }
}
