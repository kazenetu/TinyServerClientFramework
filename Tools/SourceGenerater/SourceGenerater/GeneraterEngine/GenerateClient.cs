using SourceGenerater.GeneraterEngine.Interfaces;
using SourceGenerater.GeneraterEngine.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace SourceGenerater.GeneraterEngine
{
  /// <summary>
  /// ソース作成クラス
  /// </summary>
  /// <remarks>シングルトンクラス</remarks>
  public class GenerateClient
  {
    /// <summary>
    /// インスタンス
    /// </summary>
    private static readonly GenerateClient instance = new GenerateClient();

    /// <summary>
    /// 設定ファイル名
    /// </summary>
    private const string SettingFileName = "GenerateClient.json";

    /// <summary>
    /// WebAPIバージョン
    /// </summary>
    /// <remarks>定数クラスから取得する</remarks>
    private string webAPIVersion = "v1";

    /// <summary>
    /// 画面ID単位の機能IDリスト
    /// </summary>
    /// <remarks>保存対象</remarks>
    public ScreenData ScreenDatas { get; } = new ScreenData();

    /// <summary>
    /// 自動生成結果
    /// </summary>
    public List<FileData> FileDatas { get; } = new List<FileData>();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private GenerateClient()
    {
    }

    /// <summary>
    /// インスタンス取得
    /// </summary>
    /// <returns>インスタンス</returns>
    public static GenerateClient GetInstance()
    {
      return instance;
    }

    #region 保存と読み込み

    /// <summary>
    /// 設定ファイルを保存
    /// </summary>
    /// <param name="basePath">実行ファイルのパス</param>
    public void Save(string basePath)
    {
      var sw = new DataContractJsonSerializer(typeof(ScreenData));

      var path = Path.Combine(basePath, SettingFileName);
      using (var tw = new StreamWriter(path, false))
      {
        using (var ms = new MemoryStream()) // 書き込み用のストリームを用意し、
        {
          // シリアライザーのWriteObjectメソッドにストリームと対象オブジェクトを渡す
          sw.WriteObject(ms, ScreenDatas);

          var output = Encoding.UTF8.GetString(ms.ToArray()); // 既定ではUTF-8で出力

          tw.Write(output);
        }
      }
    }

    /// <summary>
    /// 設定ファイルの読み込み
    /// </summary>
    /// <param name="basePath">実行ファイルのパス</param>
    public void Load(string basePath)
    {
      var path = Path.Combine(basePath, SettingFileName);
      if (!File.Exists(path))
      {
        return;
      }
      using (var tr = new StreamReader(path, false))
      {
        // 一旦クリア
        ScreenDatas.ScreenInfo.Clear();

        var sw = new DataContractJsonSerializer(typeof(ScreenData));
        using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(tr.ReadToEnd()), false))
        {
          // シリアライザーのReadObjectメソッドに読み取りストリームを渡す
          var tempScreenDatas = sw.ReadObject(ms) as ScreenData;

          // 設定
          foreach(var key in tempScreenDatas.ScreenInfo.Keys)
          {
            ScreenDatas.ScreenInfo.Add(key, tempScreenDatas.ScreenInfo[key]);
          }
        }
      }
    }

    /// <summary>
    /// WebAPIバージョン取得
    /// </summary>
    /// <param name="clientRootPath">slnファイルのフォルダ</param>
    public void SetWebAPIVersion(string clientRootPath)
    {
      var staticsFilePath = $"{clientRootPath}\\Client\\Client\\Statics.cs";
      if (!File.Exists(staticsFilePath)) return;

      var webAPIversionSentences = getWebAPIVerionSentens();
      if (webAPIversionSentences == string.Empty) return;

      var reg = new Regex("\\\"[a-z0-9]+\\\"");
      var matches = reg.Matches(webAPIversionSentences);

      var result = new StringBuilder();
      foreach (Match match in matches)
      {
        result.Append(match.Value);
      }

      webAPIVersion = result.ToString().Replace("\"", string.Empty);

      string getWebAPIVerionSentens()
      {
        using (var tr = new StreamReader(staticsFilePath, false))
        {
          while (!tr.EndOfStream)
          {
            var line = tr.ReadLine();
            if (line.Contains("WebAPIVersion = "))
            {
              return line;
            }
          }
        }
        return string.Empty;
      }
    }

    #endregion

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

      var targetT4 = new List<IForm>() { new Form(), new FormDesigner(), new Business(), new WebAPIController() { BasePath = clientRootPath } };

      // プロジェクトファイル追加用StringBuilder生成
      var itemGroups = new StringBuilder();

      foreach (var t4 in targetT4)
      {
        // パラメータ設定
        t4.BaseName = baseName.Substring(0, 1).ToUpper() + baseName.Substring(1);

        // ファイル生成
        var filePath = $"{clientRootPath}\\{t4.CreateFileName}";
        var created = CreateFile(filePath, t4.TransformText());

        // ファイル作成情報に追加
        FileDatas.Add(new FileData(created, filePath));

        // 生成した場合はプロジェクトファイル追加
        if (created && t4.ProjectElement != string.Empty)
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

      var targetT4 = new List<IMethod>() { new BusinessMethod(), new Request(), new Response(), new WebAPIControllerMethod() { BasePath = clientRootPath } };

      // プロジェクトファイル追加用StringBuilder生成
      var itemGroups = new StringBuilder();

      foreach (var t4 in targetT4)
      {
        // パラメータ設定
        t4.BaseName = baseName.Substring(0, 1).ToUpper() + baseName.Substring(1);
        t4.MethodName = methodName.Substring(0, 1).ToUpper() + methodName.Substring(1);

        // ファイル生成
        var filePath = $"{clientRootPath}\\{t4.CreateFileName}";
        var created = CreateFile(filePath, t4.TransformText());

        // ファイル作成情報に追加
        FileDatas.Add(new FileData(created, filePath));

        // 生成した場合はプロジェクトファイル追加
        if (created && t4.ProjectElement != string.Empty)
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
        Remarks += Environment.NewLine + filePath;
      }
    }
    #endregion
  }
}
