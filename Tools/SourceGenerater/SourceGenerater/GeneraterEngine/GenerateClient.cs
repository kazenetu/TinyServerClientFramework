using SourceGenerater.GeneraterEngine.Interfaces;
using SourceGenerater.GeneraterEngine.Templates;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SourceGenerater.GeneraterEngine
{
  public class GenerateClient
  {
    public ScreenData ScreenDatas { get; } = new ScreenData();

    public List<FileData> FileDatas { get; } = new List<FileData>();

    /// <summary>
    /// FormクラスとBusinessクラスの自動生成
    /// </summary>
    /// <param name="clientRootPath">クライアントプロジェクトのルートパス</param>
    /// <param name="baseName">ベース名</param>
    /// <returns>生成成否</returns>
    public void Generate(string clientRootPath,string baseName)
    {
      // ファイル作成情報をクリア
      FileDatas.Clear();

      var targetT4 = new List<IForm>() { new Form(), new FormDesigner(),new Business() };

      // プロジェクトファイル追加用StringBuilder生成
      var itemGroups = new StringBuilder();

      foreach (var t4 in targetT4)
      {
        // パラメータ設定
        t4.BaseName = baseName;

        // ファイル生成
        var filePath = $"{clientRootPath}\\{t4.CreateFileName}";
        var created = CreateFile(filePath, t4.TransformText());

        // ファイル作成情報に追加
        FileDatas.Add(new FileData(created, filePath));

        // 生成した場合はプロジェクトファイル追加
        if (created)
        {
          itemGroups.Append(t4.ProjectElement);
        }
      }

      // プロジェクトファイルにForm・Form.Designer・Businessを追加
      if(itemGroups.ToString().Trim() != string.Empty)
      {
        AddCSProject(clientRootPath, itemGroups.ToString());
      }
    }

    /// <summary>
    /// ビジネスクラスのメソッドと専用DTOを追加
    /// </summary>
    /// <param name="clientRootPath">クライアントプロジェクトのルートパス</param>
    /// <param name="baseName">ベース名</param>
    /// <param name="methodName">メソッド名</param>
    public void AddBusinessMethod(string clientRootPath, string baseName, string methodName)
    {
      // ファイル作成情報をクリア
      FileDatas.Clear();

      var targetT4 = new List<IMethod>() { new BusinessMethod(), new Request(), new Response() };

      // プロジェクトファイル追加用StringBuilder生成
      var itemGroups = new StringBuilder();

      foreach (var t4 in targetT4)
      {
        // パラメータ設定
        t4.BaseName = baseName;
        t4.MethodName = methodName;

        // ファイル生成
        var filePath = $"{clientRootPath}\\{t4.CreateFileName}";
        var created = CreateFile(filePath, t4.TransformText());

        // ファイル作成情報に追加
        FileDatas.Add(new FileData(created, filePath));

        // 生成した場合はプロジェクトファイル追加
        if (created)
        {
          itemGroups.Append(t4.ProjectElement);
        }
      }

      // プロジェクトファイルにForm・Form.Designer・Businessを追加
      if (itemGroups.ToString().Trim() != string.Empty)
      {
        AddCSProject(clientRootPath, itemGroups.ToString());
      }
    }

    /// <summary>
    /// ファイル作成
    /// </summary>
    /// <param name="filePath">ファイル名を含むフルパス</param>
    /// <param name="transformText">T4で変換したソースコード</param>
    /// <returns>作成成否</returns>
    private bool CreateFile(string filePath, string transformText)
    {
      var fullPath = Path.GetFullPath(filePath);
      if (File.Exists(fullPath))
      {
        return false;
      }

      var folderPath = Path.GetDirectoryName(fullPath);
      if (!Directory.Exists(folderPath))
      {
        Directory.CreateDirectory(folderPath);
      }

      using (var tw = new StreamWriter(fullPath, false, new UTF8Encoding(true)))
      {
        // csファイル作成
        tw.Write(transformText);
      }

      return true;
    }

    /// <summary>
    /// ClientプロジェクトファイルにItemGroupを追加する
    /// </summary>
    /// <param name="clientRootPath">クライアントプロジェクトのルートパス</param>
    /// <param name="targetItemGroup">追加するItemGroup情報</param>
    private void AddCSProject(string clientRootPath, string targetItemGroup)
    {
      var projectFile = $"{clientRootPath}\\Client.csproj";
      if (!File.Exists(projectFile))
      {
        return;
      }

      var projectValue = string.Empty;

      using (var sr = new StreamReader(projectFile))
      {
        projectValue = sr.ReadToEnd();
      }

      var addItenGroup = new StringBuilder();
      addItenGroup.AppendLine("  <ItemGroup>");
      addItenGroup.AppendLine(targetItemGroup);
      addItenGroup.AppendLine("  </ItemGroup>");
      addItenGroup.Append("</Project>");

      var lastTagStartPos = projectValue.LastIndexOf("</Project>");
      projectValue = projectValue.Substring(0, lastTagStartPos)+ addItenGroup.ToString();

      using (var sw = new StreamWriter(projectFile, false, new UTF8Encoding(true)))
      {
        sw.Write(projectValue);
      }
    }

    #region  関連クラス

    /// <summary>
    /// 画面情報
    /// </summary>
    public class ScreenData
    {
      /// <summary>
      /// 画面IDをキーとした機能IDリスト
      /// </summary>
      public Dictionary<string, List<string>> ScreenInfo { get; } = new Dictionary<string, List<string>>();
    }

    public class FileData
    {
      public bool IsCreated { get; }
      public string ClassName { get; }
      public string Remarks { get; }

      /// <summary>
      /// コンストラクタ
      /// </summary>
      /// <param name="isCreated"></param>
      /// <param name="flieName"></param>
      public FileData(bool isCreated,string filePath)
      {
        IsCreated = isCreated;
        ClassName = Path.GetFileName(filePath).Replace(".cs",string.Empty);

        if (isCreated)
        {
          Remarks = "ファイルを作成しました。";
        }
        else
        {
          Remarks = "ファイルが存在するため作成しませんでした。";
        }
        Remarks += filePath;
      }
    }
    #endregion
  }
}
