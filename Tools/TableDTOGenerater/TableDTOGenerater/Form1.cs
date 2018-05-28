using System.IO;
using System.Text;
using System.Windows.Forms;
using TableDTOGenerater.Common;

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

          TableDTOPath.Text = Path.Combine(rootFolder, @"DataTransferObjects\Tables");
        }
      }
      if(TableDTOPath.Text.Trim() == string.Empty)
      {
        // 未設定の場合は相対パスを初期値に設定
        TableDTOPath.Text = Path.GetFullPath(@"../../../../..\DataTransferObjects\Tables");
      }
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
      if (TableDTOPath.Text == string.Empty)
      {
        return;
      }

      // TableDTOのベースパスを取得
      var basePath = TableDTOPath.Text;

      // 文字コードをUTF8に設定
      var utf8Encoding = new UTF8Encoding(true);

      // dbDataインスタンス取得
      var dbDataInstance = DatabaseData.GetInstance();

      var sb = new StringBuilder();
      foreach (var item in dbDataInstance.GetTables(DatabaseCombo.Text))
      {
        // テーブル単位でcsファイルを作成
        var template = new Templates.TableDTO
        {
          Table = item
        };

        // 作成ファイルパス設定
        var filePath = $"{basePath}\\{item.TableName}.cs";

        // フォルダの存在確認と作成
        var folderPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(folderPath))
        {
          Directory.CreateDirectory(folderPath);
        }

        // ファイル書き出し
        using (var tw = new StreamWriter(filePath,false, utf8Encoding))
        {
          // csファイル作成
          tw.Write(template.TransformText());
        }

        // 書き出し結果を反映
        sb.AppendLine($"{filePath }を書き出しました。");
      }

      // 書き出し結果をテキストボックスに表示
      CreateResult.Text = sb.ToString();
    }

    /// <summary>
    /// ファイル出力先設定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RefFolder_Click(object sender, System.EventArgs e)
    {
      // 出力先が指定されている場合はフォルダ指定ダイアログに設定
      if (TableDTOPath.Text != string.Empty)
      {
        folderBrowserDialog1.SelectedPath = TableDTOPath.Text;
      }

      // OKボタンクリックの場合は出力先を設定
      if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        TableDTOPath.Text = folderBrowserDialog1.SelectedPath;
      }
    }
  }
}
