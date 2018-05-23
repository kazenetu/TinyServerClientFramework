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
      databaseCombo.DataSource = databaseNames;

      // TableDTOのパスを設定
      tableDTOPath.Text = Path.GetFullPath(@"../../../../..\DataTransferObjects\Tables");
    }

    /// <summary>
    /// Databaseコンボボックスの選択値変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void databaseCombo_SelectedValueChanged(object sender, System.EventArgs e)
    {
      // dbDataインスタンス取得
      var dbDataInstance = DatabaseData.GetInstance();

      // 選択されたDatabaseの接続文字列を設定
      connectionString.Text = dbDataInstance.GetConnectString(databaseCombo.Text);
    }

    /// <summary>
    /// 接続文字列変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void connectionString_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      // dbDataインスタンス取得
      var dbDataInstance = DatabaseData.GetInstance();

      // 選択中のDatabaseの接続文字列を入力値で反映
      dbDataInstance.SetConnectString(databaseCombo.Text, connectionString.Text);
    }

    /// <summary>
    /// 生成ボタン クリック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void generate_Click(object sender, System.EventArgs e)
    {
      // Database未選択の場合は終了
      if(databaseCombo.Text == string.Empty)
      {
        return;
      }

      // 出力先未設定の場合は終了
      if (tableDTOPath.Text == string.Empty)
      {
        return;
      }

      // TableDTOのベースパスを取得
      var basePath = tableDTOPath.Text;

      // 文字コードをUTF8に設定
      var utf8Encoding = new UTF8Encoding(true);

      // dbDataインスタンス取得
      var dbDataInstance = DatabaseData.GetInstance();

      var sb = new StringBuilder();
      foreach (var item in dbDataInstance.GetTables(databaseCombo.Text))
      {
        // テーブル単位でcsファイルを作成
        var template = new Templates.TableDTO();
        template.Table = item;

        var filePath = $"{basePath}\\{item.TableName}.cs";
        using (var tw = new StreamWriter(filePath,false, utf8Encoding))
        {
          // csファイル作成
          tw.Write(template.TransformText());
        }

        // 書き出し結果を反映
        sb.AppendLine($"{filePath }を書き出しました。");
      }

      // 書き出し結果をテキストボックスに表示
      textBox1.Text = sb.ToString();
    }

    /// <summary>
    /// ファイル出力先設定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void refFolder_Click(object sender, System.EventArgs e)
    {
      // 出力先が指定されている場合はフォルダ指定ダイアログに設定
      if (tableDTOPath.Text != string.Empty)
      {
        folderBrowserDialog1.SelectedPath = tableDTOPath.Text;
      }

      // OKボタンクリックの場合は出力先を設定
      if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        tableDTOPath.Text = folderBrowserDialog1.SelectedPath;
      }
    }
  }
}
