using System.IO;
using System.Text;
using System.Windows.Forms;
using TableDTOGenerater.Common;

namespace TableDTOGenerater
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      var dbDataInstance = DatabaseData.GetInstance();

      var databaseNames = dbDataInstance.DatabaseNames;
      databaseNames.Insert(0, string.Empty);
      databaseCombo.DataSource = databaseNames;

      // TableDTOのパスを設定
      tableDTOPath.Text = Path.GetFullPath(@"../../../../..\DataTransferObjects\Tables");
    }

    private void databaseCombo_SelectedValueChanged(object sender, System.EventArgs e)
    {
      var dbDataInstance = DatabaseData.GetInstance();

      connectionString.Text = dbDataInstance.GetConnectString(databaseCombo.Text);
    }

    private void connectionString_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      var dbDataInstance = DatabaseData.GetInstance();

      dbDataInstance.SetConnectString(databaseCombo.Text, connectionString.Text);
    }

    private void generate_Click(object sender, System.EventArgs e)
    {
      if(databaseCombo.Text == string.Empty)
      {
        return;
      }
      if (tableDTOPath.Text == string.Empty)
      {
        return;
      }

      var dbDataInstance = DatabaseData.GetInstance();

      var sb = new StringBuilder();
      var basePath = tableDTOPath.Text;
      foreach (var item in dbDataInstance.GetTables(databaseCombo.Text))
      {
        var template = new Templates.TableDTO();
        template.Table = item;

        var filePath = $"{basePath}\\{item.TableName}.cs";
        using (var tw = new StreamWriter(filePath))
        {
          tw.Write(template.TransformText());
        }

        sb.AppendLine($"{filePath }を書き出しました。");
        textBox1.Text = sb.ToString();
      }

      textBox1.Text = sb.ToString();
    }

    private void refFolder_Click(object sender, System.EventArgs e)
    {
      if (tableDTOPath.Text != string.Empty)
      {
        folderBrowserDialog1.SelectedPath = tableDTOPath.Text;
      }

      if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        tableDTOPath.Text = folderBrowserDialog1.SelectedPath;
      }
    }
  }
}
