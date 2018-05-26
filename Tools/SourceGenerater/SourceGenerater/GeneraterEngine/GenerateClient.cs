using SourceGenerater.GeneraterEngine.Interfaces;
using SourceGenerater.GeneraterEngine.Templates;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SourceGenerater.GeneraterEngine
{
  public class GenerateClient
  {
    /// <summary>
    /// FormクラスとBusinessクラスの自動生成
    /// </summary>
    /// <param name="clientRootPath">クライアントプロジェクトのルートパス</param>
    /// <param name="baseName">ベース名</param>
    /// <returns>生成成否</returns>
    public bool Generate(string clientRootPath,string baseName)
    {
      var targetT4 = new List<IForm>() { new Form(), new FormDesigner(),new Business() };

      var itemGroups = new StringBuilder();
      foreach (var t4 in targetT4)
      {
        t4.BaseName = baseName;
        if (!CreateFile($"{clientRootPath}\\{t4.CreateFileName}",t4.TransformText()))
        {
          return false;
        }
        itemGroups.Append(t4.ProjectElement);
      }

      AddCSProject(clientRootPath, itemGroups.ToString());

      return true;
    }

    /// <summary>
    /// ビジネスクラスのメソッドと専用DTOを追加
    /// </summary>
    /// <param name="clientRootPath">クライアントプロジェクトのルートパス</param>
    /// <param name="baseName">ベース名</param>
    /// <param name="methodName">メソッド名</param>
    /// <returns>生成成否</returns>
    public bool AddBusinessMethod(string clientRootPath, string baseName, string methodName)
    {
      var targetT4 = new List<IMethod>() { new BusinessMethod(), new Request(), new Response() };

      var itemGroups = new StringBuilder();
      foreach (var t4 in targetT4)
      {
        t4.BaseName = baseName;
        t4.MethodName = methodName;
        if (!CreateFile($"{clientRootPath}\\{t4.CreateFileName}", t4.TransformText()))
        {
          return false;
        }
        itemGroups.Append(t4.ProjectElement);
      }

      AddCSProject(clientRootPath, itemGroups.ToString());

      return true;
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
        //return false;
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
  }
}
