using SourceGenerater.GeneraterEngine;
using System;
using System.IO;
using System.Windows.Forms;

namespace SourceGenerater
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var basePath = Path.GetFullPath(@"../../../../..\Client\Client");
      var baseName = "Test";

      var generater = new GenerateClient();
      if (generater.Generate(basePath, baseName))
      {
        MessageBox.Show("OK");
      }
      else
      {
        MessageBox.Show("NG");
      }

    }

    private void button2_Click(object sender, EventArgs e)
    {
      var basePath = Path.GetFullPath(@"../../../../..\Client\Client");
      var baseName = "Test";
      var methodName = "Display";

      var generater = new GenerateClient();
      if (generater.AddBusinessMethod(basePath, baseName, methodName))
      {
        MessageBox.Show("OK");
      }
      else
      {
        MessageBox.Show("NG");
      }
    }
  }
}
