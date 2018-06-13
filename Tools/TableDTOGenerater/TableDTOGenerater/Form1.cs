using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TableDTOGenerater.Common;
using TableDTOGenerater.Common.Interfaces;
using static TableDTOGenerater.Common.DatabaseData;

namespace TableDTOGenerater
{
  public partial class Form1 : Form
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Form1()
    {
      InitializeComponent();

      // dbDataインスタンス取得
      var dbDataInstance = DatabaseData.GetInstance();

      // Database名を取得し、コンボボックスに設定
      var databaseNames = dbDataInstance.DatabaseNames;
      databaseNames.Insert(0, string.Empty);
      DatabaseCombo.DataSource = databaseNames;

      // TableDTOのパスを設定
      if (File.Exists("RootFolder.txt"))
      {
        var rootFolderName = string.Empty;
        using(var sr = new StreamReader("RootFolder.txt"))
        {
          rootFolderName = sr.ReadLine();
        }

        // 設定ファイルに値が存在している場合は実行ファイルのフルパスからTabaleDTOのパスを設定する
        if(rootFolderName.Trim() != string.Empty)
        {
          var rootFolder = Application.StartupPath;
          var index = rootFolder.LastIndexOf(rootFolderName);
          rootFolder = rootFolder.Substring(0, index + rootFolderName.Length);

          RootFolder.Text = rootFolder;
        }
      }
      if (RootFolder.Text.Trim() == string.Empty)
      {
        // 未設定の場合は相対パスを初期値に設定
        RootFolder.Text = Path.GetFullPath(@"../../");
      }

      Text += $"  ver {Application.ProductVersion}";
    }

    /// <summary>
    /// Databaseコンボボックスの選択値変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DatabaseCombo_SelectedValueChanged(object sender, System.EventArgs e)
    {
      // dbDataインスタンス取得
      var dbDataInstance = DatabaseData.GetInstance();

      // 選択されたDatabaseの接続文字列を設定
      ConnectionString.Text = dbDataInstance.GetConnectString(DatabaseCombo.Text);
    }

    /// <summary>
    /// 接続文字列変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ConnectionString_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      // dbDataインスタンス取得
      var dbDataInstance = DatabaseData.GetInstance();

      // 選択中のDatabaseの接続文字列を入力値で反映
      dbDataInstance.SetConnectString(DatabaseCombo.Text, ConnectionString.Text);
    }

    /// <summary>
    /// 生成ボタン クリック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Generate_Click(object sender, System.EventArgs e)
    {
      // Database未選択の場合は終了
      if(DatabaseCombo.Text == string.Empty)
      {
        return;
      }

      // 出力先未設定の場合は終了
      if (RootFolder.Text == string.Empty)
      {
        return;
      }

      // TableDTOのベースパスを取得
      var basePath = RootFolder.Text;

      // 文字コードをUTF8に設定
      var utf8Encoding = new UTF8Encoding(true);

      // dbDataインスタンス取得
      var dbDataInstance = DatabaseData.GetInstance();

      var sb = new StringBuilder();
      foreach (var item in dbDataInstance.GetTables(DatabaseCombo.Text))
      {
        // テーブルDTOを作成
        sb.AppendLine(
          CreateFile(
            $"{basePath}\\DataTransferObjects\\Tables\\{item.TableName}.cs",
            new Templates.TableDTO { Table = item })
        );

        // テストテーブルクラスを作成
        sb.AppendLine(
          CreateFile(
            $"{basePath}\\WebAPITest\\TestTables\\{item.TableName}Test.cs",
            new Templates.TestTable { Table = item })
        );

      }

      // 書き出し結果をテキストボックスに表示
      CreateResult.Text = sb.ToString();
    }

    /// <summary>
    /// ファイル作成
    /// </summary>
    /// <param name="filePath">ファイルパス</param>
    /// <param name="template">T4インスタンス</param>
    /// <returns>ファイル作成メッセージ</returns>
    private string CreateFile(string filePath, ITransformText template)
    {
      // フォルダの存在確認と作成
      var folderPath = Path.GetDirectoryName(filePath);
      if (!Directory.Exists(folderPath))
      {
        Directory.CreateDirectory(folderPath);
      }

      // ファイル書き出し
      using (var tw = new StreamWriter(filePath, false, new UTF8Encoding(true)))
      {
        // csファイル作成
        tw.Write(template.TransformText());
      }

      // 書き出し結果を反映
      return $"{filePath }を書き出しました。";
    }

    /// <summary>
    /// ファイル出力先設定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RefRootFolder_Click(object sender, System.EventArgs e)
    {
      // 出力先が指定されている場合はフォルダ指定ダイアログに設定
      if (RootFolder.Text != string.Empty)
      {
        folderBrowserDialog1.SelectedPath = RootFolder.Text;
      }

      // OKボタンクリックの場合は出力先を設定
      if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        RootFolder.Text = folderBrowserDialog1.SelectedPath;
      }
    }
  }
}
