namespace SourceGenerater
{
  partial class MainForm
  {
    /// <summary>
    /// 必要なデザイナー変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows フォーム デザイナーで生成されたコード

    /// <summary>
    /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディターで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.RefRootFolder = new System.Windows.Forms.Button();
      this.RootFolder = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.ScreenID = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.CreateFormBus = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(537, 143);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(175, 28);
      this.button1.TabIndex = 0;
      this.button1.Text = "Form・Business作成テスト";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(324, 137);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(175, 40);
      this.button2.TabIndex = 1;
      this.button2.Text = "Businessメソッド・専用DTO\r\n追加テスト";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // RefRootFolder
      // 
      this.RefRootFolder.Location = new System.Drawing.Point(680, 10);
      this.RefRootFolder.Name = "RefRootFolder";
      this.RefRootFolder.Size = new System.Drawing.Size(41, 23);
      this.RefRootFolder.TabIndex = 10;
      this.RefRootFolder.Text = "...";
      this.RefRootFolder.UseVisualStyleBackColor = true;
      this.RefRootFolder.Click += new System.EventHandler(this.RefTargetFolder_Click);
      // 
      // RootFolder
      // 
      this.RootFolder.Location = new System.Drawing.Point(86, 12);
      this.RootFolder.Name = "RootFolder";
      this.RootFolder.Size = new System.Drawing.Size(588, 19);
      this.RootFolder.TabIndex = 9;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 15);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(68, 12);
      this.label3.TabIndex = 8;
      this.label3.Text = "ルートフォルダ";
      // 
      // folderBrowserDialog1
      // 
      this.folderBrowserDialog1.ShowNewFolderButton = false;
      // 
      // ScreenID
      // 
      this.ScreenID.FormattingEnabled = true;
      this.ScreenID.Location = new System.Drawing.Point(86, 53);
      this.ScreenID.Name = "ScreenID";
      this.ScreenID.Size = new System.Drawing.Size(454, 20);
      this.ScreenID.TabIndex = 11;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 56);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 12);
      this.label1.TabIndex = 12;
      this.label1.Text = "画面ID";
      // 
      // CreateFormBus
      // 
      this.CreateFormBus.Location = new System.Drawing.Point(569, 51);
      this.CreateFormBus.Name = "CreateFormBus";
      this.CreateFormBus.Size = new System.Drawing.Size(143, 23);
      this.CreateFormBus.TabIndex = 13;
      this.CreateFormBus.Text = "Form・Business作成";
      this.CreateFormBus.UseVisualStyleBackColor = true;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(783, 455);
      this.Controls.Add(this.CreateFormBus);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ScreenID);
      this.Controls.Add(this.RefRootFolder);
      this.Controls.Add(this.RootFolder);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Name = "MainForm";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button RefRootFolder;
    private System.Windows.Forms.TextBox RootFolder;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.ComboBox ScreenID;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button CreateFormBus;
  }
}

