namespace TableDTOGenerater
{
  partial class Form1
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
      this.label1 = new System.Windows.Forms.Label();
      this.DatabaseCombo = new System.Windows.Forms.ComboBox();
      this.ConnectionString = new System.Windows.Forms.TextBox();
      this.Generate = new System.Windows.Forms.Button();
      this.CreateResult = new System.Windows.Forms.TextBox();
      this.RootFolder = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.RefRootFolder = new System.Windows.Forms.Button();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(53, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "Database";
      // 
      // DatabaseCombo
      // 
      this.DatabaseCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.DatabaseCombo.FormattingEnabled = true;
      this.DatabaseCombo.Location = new System.Drawing.Point(82, 12);
      this.DatabaseCombo.Name = "DatabaseCombo";
      this.DatabaseCombo.Size = new System.Drawing.Size(144, 20);
      this.DatabaseCombo.TabIndex = 1;
      this.DatabaseCombo.SelectedValueChanged += new System.EventHandler(this.DatabaseCombo_SelectedValueChanged);
      // 
      // ConnectionString
      // 
      this.ConnectionString.Location = new System.Drawing.Point(242, 12);
      this.ConnectionString.Name = "ConnectionString";
      this.ConnectionString.Size = new System.Drawing.Size(527, 19);
      this.ConnectionString.TabIndex = 2;
      this.ConnectionString.Validating += new System.ComponentModel.CancelEventHandler(this.ConnectionString_Validating);
      // 
      // Generate
      // 
      this.Generate.Location = new System.Drawing.Point(108, 67);
      this.Generate.Name = "Generate";
      this.Generate.Size = new System.Drawing.Size(614, 23);
      this.Generate.TabIndex = 3;
      this.Generate.Text = "生成";
      this.Generate.UseVisualStyleBackColor = true;
      this.Generate.Click += new System.EventHandler(this.Generate_Click);
      // 
      // CreateResult
      // 
      this.CreateResult.Location = new System.Drawing.Point(12, 105);
      this.CreateResult.Multiline = true;
      this.CreateResult.Name = "CreateResult";
      this.CreateResult.ReadOnly = true;
      this.CreateResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.CreateResult.Size = new System.Drawing.Size(757, 300);
      this.CreateResult.TabIndex = 4;
      // 
      // RootFolder
      // 
      this.RootFolder.Location = new System.Drawing.Point(82, 39);
      this.RootFolder.Name = "RootFolder";
      this.RootFolder.Size = new System.Drawing.Size(640, 19);
      this.RootFolder.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 42);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(68, 12);
      this.label2.TabIndex = 6;
      this.label2.Text = "ルートフォルダ";
      // 
      // RefRootFolder
      // 
      this.RefRootFolder.Location = new System.Drawing.Point(728, 37);
      this.RefRootFolder.Name = "RefRootFolder";
      this.RefRootFolder.Size = new System.Drawing.Size(41, 23);
      this.RefRootFolder.TabIndex = 7;
      this.RefRootFolder.Text = "...";
      this.RefRootFolder.UseVisualStyleBackColor = true;
      this.RefRootFolder.Click += new System.EventHandler(this.RefRootFolder_Click);
      // 
      // folderBrowserDialog1
      // 
      this.folderBrowserDialog1.ShowNewFolderButton = false;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.RefRootFolder);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.RootFolder);
      this.Controls.Add(this.CreateResult);
      this.Controls.Add(this.Generate);
      this.Controls.Add(this.ConnectionString);
      this.Controls.Add(this.DatabaseCombo);
      this.Controls.Add(this.label1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox DatabaseCombo;
    private System.Windows.Forms.TextBox ConnectionString;
    private System.Windows.Forms.Button Generate;
    private System.Windows.Forms.TextBox CreateResult;
    private System.Windows.Forms.TextBox RootFolder;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button RefRootFolder;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
  }
}

