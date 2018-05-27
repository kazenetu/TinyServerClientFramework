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
      this.RefRootFolder = new System.Windows.Forms.Button();
      this.RootFolder = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.ScreenID = new System.Windows.Forms.ComboBox();
      this.ScreenIDName = new System.Windows.Forms.Label();
      this.CreateFormBus = new System.Windows.Forms.Button();
      this.ResultView = new System.Windows.Forms.DataGridView();
      this.FunctionIDName = new System.Windows.Forms.Label();
      this.FunctionID = new System.Windows.Forms.ComboBox();
      this.IsCreated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.AddBusinessMethod = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.ResultView)).BeginInit();
      this.SuspendLayout();
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
      this.ScreenID.Size = new System.Drawing.Size(504, 20);
      this.ScreenID.TabIndex = 11;
      this.ScreenID.Leave += new System.EventHandler(this.ScreenID_Leave);
      // 
      // ScreenIDName
      // 
      this.ScreenIDName.AutoSize = true;
      this.ScreenIDName.Location = new System.Drawing.Point(12, 56);
      this.ScreenIDName.Name = "ScreenIDName";
      this.ScreenIDName.Size = new System.Drawing.Size(40, 12);
      this.ScreenIDName.TabIndex = 12;
      this.ScreenIDName.Text = "画面ID";
      // 
      // CreateFormBus
      // 
      this.CreateFormBus.Location = new System.Drawing.Point(608, 50);
      this.CreateFormBus.Name = "CreateFormBus";
      this.CreateFormBus.Size = new System.Drawing.Size(163, 23);
      this.CreateFormBus.TabIndex = 13;
      this.CreateFormBus.Text = "Form・Business作成";
      this.CreateFormBus.UseVisualStyleBackColor = true;
      this.CreateFormBus.Click += new System.EventHandler(this.CreateFormBus_Click);
      // 
      // ResultView
      // 
      this.ResultView.AllowUserToAddRows = false;
      this.ResultView.AllowUserToDeleteRows = false;
      this.ResultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ResultView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsCreated,
            this.ClassName,
            this.Remarks});
      this.ResultView.Location = new System.Drawing.Point(13, 142);
      this.ResultView.Name = "ResultView";
      this.ResultView.ReadOnly = true;
      this.ResultView.RowTemplate.Height = 21;
      this.ResultView.Size = new System.Drawing.Size(758, 289);
      this.ResultView.TabIndex = 15;
      // 
      // FunctionIDName
      // 
      this.FunctionIDName.AutoSize = true;
      this.FunctionIDName.Location = new System.Drawing.Point(12, 89);
      this.FunctionIDName.Name = "FunctionIDName";
      this.FunctionIDName.Size = new System.Drawing.Size(40, 12);
      this.FunctionIDName.TabIndex = 12;
      this.FunctionIDName.Text = "機能ID";
      // 
      // FunctionID
      // 
      this.FunctionID.FormattingEnabled = true;
      this.FunctionID.Location = new System.Drawing.Point(86, 86);
      this.FunctionID.Name = "FunctionID";
      this.FunctionID.Size = new System.Drawing.Size(504, 20);
      this.FunctionID.TabIndex = 16;
      this.FunctionID.Leave += new System.EventHandler(this.FunctionID_Leave);
      // 
      // IsCreated
      // 
      this.IsCreated.DataPropertyName = "IsCreated";
      this.IsCreated.FillWeight = 60F;
      this.IsCreated.HeaderText = "生成";
      this.IsCreated.MinimumWidth = 50;
      this.IsCreated.Name = "IsCreated";
      this.IsCreated.ReadOnly = true;
      this.IsCreated.Width = 50;
      // 
      // ClassName
      // 
      this.ClassName.DataPropertyName = "ClassName";
      this.ClassName.HeaderText = "クラス名";
      this.ClassName.MinimumWidth = 100;
      this.ClassName.Name = "ClassName";
      this.ClassName.ReadOnly = true;
      // 
      // Remarks
      // 
      this.Remarks.DataPropertyName = "Remarks";
      this.Remarks.FillWeight = 1000F;
      this.Remarks.HeaderText = "備考";
      this.Remarks.MinimumWidth = 100;
      this.Remarks.Name = "Remarks";
      this.Remarks.ReadOnly = true;
      this.Remarks.Width = 500;
      // 
      // AddBusinessMethod
      // 
      this.AddBusinessMethod.Location = new System.Drawing.Point(608, 84);
      this.AddBusinessMethod.Name = "AddBusinessMethod";
      this.AddBusinessMethod.Size = new System.Drawing.Size(163, 23);
      this.AddBusinessMethod.TabIndex = 17;
      this.AddBusinessMethod.Text = "Businessメソッド・DTO追加";
      this.AddBusinessMethod.UseVisualStyleBackColor = true;
      this.AddBusinessMethod.Click += new System.EventHandler(this.AddBusinessMethod_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(783, 455);
      this.Controls.Add(this.AddBusinessMethod);
      this.Controls.Add(this.FunctionID);
      this.Controls.Add(this.ResultView);
      this.Controls.Add(this.CreateFormBus);
      this.Controls.Add(this.FunctionIDName);
      this.Controls.Add(this.ScreenIDName);
      this.Controls.Add(this.ScreenID);
      this.Controls.Add(this.RefRootFolder);
      this.Controls.Add(this.RootFolder);
      this.Controls.Add(this.label3);
      this.Name = "MainForm";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.ResultView)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button RefRootFolder;
    private System.Windows.Forms.TextBox RootFolder;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.ComboBox ScreenID;
    private System.Windows.Forms.Label ScreenIDName;
    private System.Windows.Forms.Button CreateFormBus;
    private System.Windows.Forms.DataGridView ResultView;
    private System.Windows.Forms.Label FunctionIDName;
    private System.Windows.Forms.ComboBox FunctionID;
    private System.Windows.Forms.DataGridViewCheckBoxColumn IsCreated;
    private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
    private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
    private System.Windows.Forms.Button AddBusinessMethod;
  }
}

