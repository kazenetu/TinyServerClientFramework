﻿using SourceGenerater.GeneraterEngine;
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

      if (!generater.ScreenDatas.ScreenInfo.ContainsKey(ScreenID.Text))
      {
        var screenID = ScreenID.Text;
        generater.ScreenDatas.ScreenInfo.Add(screenID, new System.Collections.Generic.List<string>());

        ScreenID.DataSource = generater.ScreenDatas.ScreenInfo.Keys.ToList();
        ScreenID.Text = string.Empty;

        ScreenID.SelectedText = screenID;
      }

      // 機能IDの設定
      FunctionID.Items.Clear();
      FunctionID.Items.AddRange(generater.ScreenDatas.ScreenInfo[ScreenID.Text].ToArray());
      FunctionID.Text = generater.ScreenDatas.ScreenInfo[ScreenID.Text].FirstOrDefault();
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
      var basePath = Path.Combine(RootFolder.Text, @"Client\Client");

      // 対象ファイル作成
      var generater = new GenerateClient();
      generater.Generate(basePath, screenID);

      // 生成結果をグリッドに表示
      ResultView.DataSource = generater.FileDatas;
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
      if (!generater.ScreenDatas.ScreenInfo.ContainsKey(ScreenID.Text))
      {
        FunctionID.DataSource = null;
        FunctionID.Text = string.Empty;
      }

      // 機能IDが存在しない場合は追加
      var screenID = ScreenID.Text;
      var functionID = FunctionID.Text;
      if (!generater.ScreenDatas.ScreenInfo[screenID].Contains(functionID))
      {
        generater.ScreenDatas.ScreenInfo[screenID].Add(functionID);

        FunctionID.Items.Clear();
        FunctionID.Items.AddRange(generater.ScreenDatas.ScreenInfo[ScreenID.Text].ToArray());
        FunctionID.Text = string.Empty;

        FunctionID.SelectedText = functionID;
      }
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
      var basePath = Path.Combine(RootFolder.Text, @"Client\Client");

      // 対象ファイル作成
      var generater = new GenerateClient();
      generater.AddBusinessMethod(basePath, screenID, functionID);

      // 生成結果をグリッドに表示
      ResultView.DataSource = generater.FileDatas;
    }
    #endregion

  }
}
