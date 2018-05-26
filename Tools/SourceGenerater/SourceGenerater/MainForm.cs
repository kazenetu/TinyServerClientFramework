using SourceGenerater.GeneraterEngine;
using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace SourceGenerater
{
  public partial class MainForm : Form
  {
    private GenerateClient generater = new GenerateClient();

    public MainForm()
    {
      InitializeComponent();

      // ルートパスを設定
      if (File.Exists("RootFolder.txt"))
      {
        var rootFolderName = string.Empty;
        using (var sr = new StreamReader("RootFolder.txt"))
        {
          rootFolderName = sr.ReadLine();
        }

        // 設定ファイルに値が存在している場合は実行ファイルのフルパスからTabaleDTOのパスを設定する
        if (rootFolderName.Trim() != string.Empty)
        {
          var rootFolder = Application.StartupPath;
          var index = rootFolder.LastIndexOf(rootFolderName);
          RootFolder.Text = rootFolder.Substring(0, index + rootFolderName.Length);
        }
      }
      if (RootFolder.Text.Trim() == string.Empty)
      {
        // 未設定の場合は相対パスを初期値に設定
        RootFolder.Text = Path.GetFullPath(@"../../../../../");
      }
    }

    private void RefTargetFolder_Click(object sender, EventArgs e)
    {
      // 出力先が指定されている場合はフォルダ指定ダイアログに設定
      if (RootFolder.Text != string.Empty)
      {
        folderBrowserDialog1.SelectedPath = RootFolder.Text;
      }

      // OKボタンクリックの場合は出力先を設定
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        RootFolder.Text = folderBrowserDialog1.SelectedPath;
      }
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

    private void ScreenID_Leave(object sender, EventArgs e)
    {
      if (!generater.ScreenDatas.ScreenInfo.ContainsKey(ScreenID.Text))
      {
        var screenID = ScreenID.Text;
        generater.ScreenDatas.ScreenInfo.Add(screenID, new System.Collections.Generic.List<string>());

        ScreenID.DataSource = generater.ScreenDatas.ScreenInfo.Keys.ToList();
        ScreenID.Text = string.Empty;

        ScreenID.SelectedText = screenID;
      }
    }
  }
}
