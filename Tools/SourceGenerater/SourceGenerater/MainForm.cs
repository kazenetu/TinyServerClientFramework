using SourceGenerater.GeneraterEngine;
using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SourceGenerater
{
  public partial class MainForm : Form
  {
    /// <summary>
    /// 生成対象列挙型
    /// </summary>
    private enum GaneraterMode
    {
      None,
      ClientWebAPI,
      Client,
      WebAPI
    };

    /// <summary>
    /// ジェネレータークラスインスタンス
    /// </summary>
    private GenerateClient Generater = GenerateClient.GetInstance();

    /// <summary>
    /// 生成対象メニューアイテム
    /// </summary>
    private List<ToolStripMenuItem> ModeMenuItems = null;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public MainForm()
    {
      InitializeComponent();

      // 生成対象メニューアイテム追加
      ModeMenuItems = new List<ToolStripMenuItem>() {
                        ModeClientWebAPIIToolStripMenuItem,
                        ModeClientToolStripMenuItem,
                        ModeWebAPIToolStripMenuItem };
      ModeClientWebAPIIToolStripMenuItem.Tag = GaneraterMode.ClientWebAPI;
      ModeClientToolStripMenuItem.Tag = GaneraterMode.Client;
      ModeWebAPIToolStripMenuItem.Tag = GaneraterMode.WebAPI;

      // 生成対象の初期設定
      ModeMenu_Click(ModeClientWebAPIIToolStripMenuItem, new EventArgs());

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
        RootFolder.Text = Path.GetFullPath(@"../../");
      }

      // 画面IDの設定
      ScreenID.DataSource = Generater.ScreenDatas.ScreenInfo.Keys.ToList();
      ScreenID.Text = Generater.ScreenDatas.ScreenInfo.Keys.FirstOrDefault();

      // 機能IDの設定
      if (Generater.ScreenDatas.ScreenInfo.ContainsKey(ScreenID.Text)) {
        FunctionID.Items.Clear();
        FunctionID.Items.AddRange(Generater.ScreenDatas.ScreenInfo[ScreenID.Text].ToArray());
        FunctionID.Text = Generater.ScreenDatas.ScreenInfo[ScreenID.Text].FirstOrDefault();
      }

      // WebAPIバージョンを定数クラスから取得するように修正
      Generater.SetWebAPIVersion(RootFolder.Text);

      // Select専用チェックボックスの設定
      SelectOnly.Checked = GetSelectOnly();
    }

    #region Select専用情報

    /// <summary>
    /// 画面ID・機能IDに紐づくSelect専用情報の取得
    /// </summary>
    /// <returns>未設定の場合はTrueを返す。設定済みの場合は設定値</returns>
    private bool GetSelectOnly()
    {
      if (ScreenID.Text == string.Empty || FunctionID.Text == string.Empty)
      {
        return false;
      }

      var selectOnlyKey = $"{ScreenID.Text}{FunctionID.Text}";
      if (!Generater.ScreenDatas.SelectOnlyMethod.ContainsKey(selectOnlyKey))
      {
        Generater.ScreenDatas.SelectOnlyMethod[selectOnlyKey] = true;
        return true;
      }
      return Generater.ScreenDatas.SelectOnlyMethod[selectOnlyKey];
    }

    /// <summary>
    /// 画面ID・機能IDに紐づくSelect専用情報の設定
    /// </summary>
    private void SetSelectOnly()
    {
      if (ScreenID.Text == string.Empty || FunctionID.Text == string.Empty)
      {
        return;
      }

      var selectOnlyKey = $"{ScreenID.Text}{FunctionID.Text}";
      if (!Generater.ScreenDatas.SelectOnlyMethod.ContainsKey(selectOnlyKey))
      {
        Generater.ScreenDatas.SelectOnlyMethod[selectOnlyKey] = true;
        return;
      }
      Generater.ScreenDatas.SelectOnlyMethod[selectOnlyKey] = SelectOnly.Checked;
    }

    #endregion

    #region ルートフォルダ

    /// <summary>
    /// ルートフォルダ設定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    #endregion


    #region 画面ID

    /// <summary>
    /// 画面IDテキストボックス ロストフォーカス
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ScreenID_Leave(object sender, EventArgs e)
    {
      if(ScreenID.Text.Trim() == string.Empty)
      {
        return;
      }

      if (!Generater.ScreenDatas.ScreenInfo.ContainsKey(ScreenID.Text))
      {
        var screenID = ScreenID.Text;
        Generater.ScreenDatas.ScreenInfo.Add(screenID, new System.Collections.Generic.List<string>());

        ScreenID.DataSource = Generater.ScreenDatas.ScreenInfo.Keys.ToList();
        ScreenID.Text = string.Empty;

        ScreenID.SelectedText = screenID;
      }

      // 機能IDの設定
      FunctionID.Items.Clear();
      FunctionID.Items.AddRange(Generater.ScreenDatas.ScreenInfo[ScreenID.Text].ToArray());
      FunctionID.Text = Generater.ScreenDatas.ScreenInfo[ScreenID.Text].FirstOrDefault();

      // Select専用チェックボックスの設定
      SelectOnly.Checked = GetSelectOnly();
    }

    /// <summary>
    /// Form・Business生成ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CreateFormBus_Click(object sender, EventArgs e)
    {
      // パラメータ設定
      var screenID = ScreenID.Text;

      // 入力チェック
      if(screenID.Trim() == string.Empty)
      {
        MessageBox.Show($"{ScreenIDName.Text}を入力してください。");
        ScreenID.Focus();
        return;
      }

      // 対象パス指定
      var basePath = Path.Combine(RootFolder.Text, "Client");

      // 対象ファイル作成
      Generater.Generate(basePath, screenID);

      // 生成結果をグリッドに表示
      ResultView.DataSource = null;
      ResultView.DataSource = Generater.FileDatas;
      ResultView.Refresh();
    }
    #endregion

    #region 機能ID

    /// <summary>
    /// 機能IDテキストボックス ロストフォーカス
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FunctionID_Leave(object sender, EventArgs e)
    {
      if (FunctionID.Text.Trim() == string.Empty)
      {
        return;
      }

      // 画面IDが設定されていない場合はクリアして終了
      if (!Generater.ScreenDatas.ScreenInfo.ContainsKey(ScreenID.Text))
      {
        FunctionID.DataSource = null;
        FunctionID.Text = string.Empty;
      }

      // 機能IDが存在しない場合は追加
      var screenID = ScreenID.Text;
      var functionID = FunctionID.Text;
      if (!Generater.ScreenDatas.ScreenInfo[screenID].Contains(functionID))
      {
        Generater.ScreenDatas.ScreenInfo[screenID].Add(functionID);

        FunctionID.Items.Clear();
        FunctionID.Items.AddRange(Generater.ScreenDatas.ScreenInfo[ScreenID.Text].ToArray());
        FunctionID.Text = string.Empty;

        FunctionID.SelectedText = functionID;
      }

      // Select専用チェックボックスの設定
      SelectOnly.Checked = GetSelectOnly();
    }

    /// <summary>
    /// Form・Business生成ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddBusinessMethod_Click(object sender, EventArgs e)
    {
      // パラメータ設定
      var screenID = ScreenID.Text;
      var functionID = FunctionID.Text;
      var selectOnly = SelectOnly.Checked;

      // 入力チェック
      if (screenID.Trim() == string.Empty)
      {
        MessageBox.Show($"{ScreenIDName.Text}を入力してください。");
        ScreenID.Focus();
        return;
      }
      if (functionID.Trim() == string.Empty)
      {
        MessageBox.Show($"{FunctionIDName.Text}を入力してください。");
        FunctionID.Focus();
        return;
      }

      // 対象パス指定
      var basePath = Path.Combine(RootFolder.Text, "Client");

      // 対象ファイル作成
      Generater.AddBusinessMethod(basePath, screenID, functionID, selectOnly);

      // 生成結果をグリッドに表示
      ResultView.DataSource = null;
      ResultView.DataSource = Generater.FileDatas;
      ResultView.Refresh();

      // Select専用チェックボックスの設定
      SetSelectOnly();
    }
    #endregion

    #region Select専用

    /// <summary>
    /// Select専用チェックボックス
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SelectOnly_CheckedChanged(object sender, EventArgs e)
    {
      SetSelectOnly();
    }

    #endregion

    #region 生成対象メニュー

    /// <summary>
    /// 生成対象メニューアイテムクリック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>生成対象メニューアイテムのチェック状態の更新とタイトル変更</remarks>
    private void ModeMenu_Click(object sender, EventArgs e)
    {
      var title = "ソースコード生成ツール ";
      foreach (var item in ModeMenuItems)
      {
        item.Checked = (sender == item);

        if (item.Checked)
        {
          title += $"生成対象[{item.Text}]";
          GeneraterStatus.Text = $"生成対象 ： {item.Text}";
        }
      }

      // バージョン情報を追加
      title += $"  ver {Application.ProductVersion}";

      // フォームタイトルを設定
      Text = title;
    }

    /// <summary>
    /// 生成対象取得
    /// </summary>
    /// <returns>生成対象</returns>
    private GaneraterMode GetGaneraterMode()
    {
      foreach (var item in ModeMenuItems)
      {
        if (item.Checked && item.Tag is GaneraterMode mode)
        {
          return mode;
        }
      }
      return GaneraterMode.None;
    }

    #endregion

  }
}
