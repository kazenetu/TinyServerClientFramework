using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TableDTOGenerater.Common;
using static WebAPIFramework.DB.DatabaseFactory;

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
      var dbDataInstance = DatabaseData.GetInstance();

      var sb = new StringBuilder();
      foreach(var item in dbDataInstance.GetTables(databaseCombo.Text))
      {
        sb.AppendLine(item);
      }

      textBox1.Text = sb.ToString();
    }
  }
}
