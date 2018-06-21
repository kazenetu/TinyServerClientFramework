using SourceGenerater.GeneraterEngine.Interfaces;

namespace SourceGenerater.GeneraterEngine.Templates
{
  /// <summary>
  /// WebAPI用テストのメソッドテンプレート
  /// </summary>
  partial class WebAPITestMethod : IMethod
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
        return $"..\\WebAPITest\\{BaseName}\\Tes{MethodName}.cs";
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
  }
}

