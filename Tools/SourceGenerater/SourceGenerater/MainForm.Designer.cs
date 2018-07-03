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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.ResultView = new System.Windows.Forms.DataGridView();
      this.IsCreated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel2 = new System.Windows.Forms.Panel();
      this.SelectOnly = new System.Windows.Forms.CheckBox();
      this.AddBusinessMethod = new System.Windows.Forms.Button();
      this.FunctionID = new System.Windows.Forms.ComboBox();
      this.CreateFormBus = new System.Windows.Forms.Button();
      this.FunctionIDName = new System.Windows.Forms.Label();
      this.ScreenIDName = new System.Windows.Forms.Label();
      this.ScreenID = new System.Windows.Forms.ComboBox();
      this.RefRootFolder = new System.Windows.Forms.Button();
      this.RootFolder = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ModeClientWebAPIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ModeClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ModeWebAPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.GeneraterStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ResultView)).BeginInit();
      this.panel2.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // folderBrowserDialog1
      // 
      this.folderBrowserDialog1.ShowNewFolderButton = false;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(818, 527);
      this.tableLayoutPanel1.TabIndex = 11;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.ResultView);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 183);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(812, 341);
      this.panel1.TabIndex = 0;
      // 
      // ResultView
      // 
      this.ResultView.AllowUserToAddRows = false;
      this.ResultView.AllowUserToDeleteRows = false;
      this.ResultView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.ResultView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.ResultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ResultView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsCreated,
            this.ClassName,
            this.Remarks});
      this.ResultView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ResultView.Location = new System.Drawing.Point(0, 0);
      this.ResultView.Name = "ResultView";
      this.ResultView.ReadOnly = true;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.ResultView.RowsDefaultCellStyle = dataGridViewCellStyle1;
      this.ResultView.RowTemplate.Height = 30;
      this.ResultView.Size = new System.Drawing.Size(812, 341);
      this.ResultView.TabIndex = 11;
      this.ResultView.TabStop = false;
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
      this.Remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Remarks.DataPropertyName = "Remarks";
      this.Remarks.FillWeight = 1000F;
      this.Remarks.HeaderText = "備考";
      this.Remarks.MinimumWidth = 100;
      this.Remarks.Name = "Remarks";
      this.Remarks.ReadOnly = true;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.SelectOnly);
      this.panel2.Controls.Add(this.AddBusinessMethod);
      this.panel2.Controls.Add(this.FunctionID);
      this.panel2.Controls.Add(this.CreateFormBus);
      this.panel2.Controls.Add(this.FunctionIDName);
      this.panel2.Controls.Add(this.ScreenIDName);
      this.panel2.Controls.Add(this.ScreenID);
      this.panel2.Controls.Add(this.RefRootFolder);
      this.panel2.Controls.Add(this.RootFolder);
      this.panel2.Controls.Add(this.label3);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(812, 174);
      this.panel2.TabIndex = 1;
      // 
      // SelectOnly
      // 
      this.SelectOnly.AutoSize = true;
      this.SelectOnly.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.SelectOnly.Location = new System.Drawing.Point(456, 108);
      this.SelectOnly.Name = "SelectOnly";
      this.SelectOnly.Size = new System.Drawing.Size(134, 20);
      this.SelectOnly.TabIndex = 18;
      this.SelectOnly.Text = "Select専用機能";
      this.SelectOnly.UseVisualStyleBackColor = true;
      this.SelectOnly.CheckedChanged += new System.EventHandler(this.SelectOnly_CheckedChanged);
      // 
      // AddBusinessMethod
      // 
      this.AddBusinessMethod.Location = new System.Drawing.Point(610, 80);
      this.AddBusinessMethod.Name = "AddBusinessMethod";
      this.AddBusinessMethod.Size = new System.Drawing.Size(163, 23);
      this.AddBusinessMethod.TabIndex = 19;
      this.AddBusinessMethod.Text = "Businessメソッド・DTO追加";
      this.AddBusinessMethod.UseVisualStyleBackColor = true;
      this.AddBusinessMethod.Click += new System.EventHandler(this.AddBusinessMethod_Click);
      // 
      // FunctionID
      // 
      this.FunctionID.FormattingEnabled = true;
      this.FunctionID.Location = new System.Drawing.Point(88, 82);
      this.FunctionID.Name = "FunctionID";
      this.FunctionID.Size = new System.Drawing.Size(504, 20);
      this.FunctionID.TabIndex = 17;
      this.FunctionID.SelectedIndexChanged += new System.EventHandler(this.FunctionID_Leave);
      this.FunctionID.Leave += new System.EventHandler(this.FunctionID_Leave);
      // 
      // CreateFormBus
      // 
      this.CreateFormBus.Location = new System.Drawing.Point(610, 46);
      this.CreateFormBus.Name = "CreateFormBus";
      this.CreateFormBus.Size = new System.Drawing.Size(163, 23);
      this.CreateFormBus.TabIndex = 15;
      this.CreateFormBus.Text = "Form・Business作成";
      this.CreateFormBus.UseVisualStyleBackColor = true;
      this.CreateFormBus.Click += new System.EventHandler(this.CreateFormBus_Click);
      // 
      // FunctionIDName
      // 
      this.FunctionIDName.AutoSize = true;
      this.FunctionIDName.Location = new System.Drawing.Point(14, 85);
      this.FunctionIDName.Name = "FunctionIDName";
      this.FunctionIDName.Size = new System.Drawing.Size(40, 12);
      this.FunctionIDName.TabIndex = 16;
      this.FunctionIDName.Text = "機能ID";
      // 
      // ScreenIDName
      // 
      this.ScreenIDName.AutoSize = true;
      this.ScreenIDName.Location = new System.Drawing.Point(14, 52);
      this.ScreenIDName.Name = "ScreenIDName";
      this.ScreenIDName.Size = new System.Drawing.Size(40, 12);
      this.ScreenIDName.TabIndex = 13;
      this.ScreenIDName.Text = "画面ID";
      // 
      // ScreenID
      // 
      this.ScreenID.FormattingEnabled = true;
      this.ScreenID.Location = new System.Drawing.Point(88, 49);
      this.ScreenID.Name = "ScreenID";
      this.ScreenID.Size = new System.Drawing.Size(504, 20);
      this.ScreenID.TabIndex = 14;
      this.ScreenID.SelectedIndexChanged += new System.EventHandler(this.ScreenID_Leave);
      this.ScreenID.Leave += new System.EventHandler(this.ScreenID_Leave);
      // 
      // RefRootFolder
      // 
      this.RefRootFolder.Location = new System.Drawing.Point(682, 6);
      this.RefRootFolder.Name = "RefRootFolder";
      this.RefRootFolder.Size = new System.Drawing.Size(41, 23);
      this.RefRootFolder.TabIndex = 12;
      this.RefRootFolder.Text = "...";
      this.RefRootFolder.UseVisualStyleBackColor = true;
      // 
      // RootFolder
      // 
      this.RootFolder.Location = new System.Drawing.Point(88, 8);
      this.RootFolder.Name = "RootFolder";
      this.RootFolder.Size = new System.Drawing.Size(588, 19);
      this.RootFolder.TabIndex = 11;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(14, 11);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(68, 12);
      this.label3.TabIndex = 10;
      this.label3.Text = "ルートフォルダ";
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModeToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(818, 24);
      this.menuStrip1.TabIndex = 12;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // ModeToolStripMenuItem
      // 
      this.ModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModeClientWebAPIIToolStripMenuItem,
            this.ModeClientToolStripMenuItem,
            this.ModeWebAPIToolStripMenuItem});
      this.ModeToolStripMenuItem.Name = "ModeToolStripMenuItem";
      this.ModeToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
      this.ModeToolStripMenuItem.Text = "生成対象";
      // 
      // ModeClientWebAPIIToolStripMenuItem
      // 
      this.ModeClientWebAPIIToolStripMenuItem.Checked = true;
      this.ModeClientWebAPIIToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ModeClientWebAPIIToolStripMenuItem.Name = "ModeClientWebAPIIToolStripMenuItem";
      this.ModeClientWebAPIIToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.ModeClientWebAPIIToolStripMenuItem.Text = "クライアント・WebAPI";
      this.ModeClientWebAPIIToolStripMenuItem.Click += new System.EventHandler(this.ModeMenu_Click);
      // 
      // ModeClientToolStripMenuItem
      // 
      this.ModeClientToolStripMenuItem.Name = "ModeClientToolStripMenuItem";
      this.ModeClientToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.ModeClientToolStripMenuItem.Text = "クライアントのみ";
      this.ModeClientToolStripMenuItem.Click += new System.EventHandler(this.ModeMenu_Click);
      // 
      // ModeWebAPIToolStripMenuItem
      // 
      this.ModeWebAPIToolStripMenuItem.Name = "ModeWebAPIToolStripMenuItem";
      this.ModeWebAPIToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.ModeWebAPIToolStripMenuItem.Text = "WebAPIのみ";
      this.ModeWebAPIToolStripMenuItem.Click += new System.EventHandler(this.ModeMenu_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GeneraterStatus});
      this.statusStrip1.Location = new System.Drawing.Point(0, 529);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(818, 22);
      this.statusStrip1.TabIndex = 13;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // GeneraterStatus
      // 
      this.GeneraterStatus.Name = "GeneraterStatus";
      this.GeneraterStatus.Size = new System.Drawing.Size(0, 17);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(818, 551);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.Text = "ソースコード作成ツール";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ResultView)).EndInit();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.DataGridView ResultView;
    private System.Windows.Forms.DataGridViewCheckBoxColumn IsCreated;
    private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
    private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.CheckBox SelectOnly;
    private System.Windows.Forms.Button AddBusinessMethod;
    private System.Windows.Forms.ComboBox FunctionID;
    private System.Windows.Forms.Button CreateFormBus;
    private System.Windows.Forms.Label FunctionIDName;
    private System.Windows.Forms.Label ScreenIDName;
    private System.Windows.Forms.ComboBox ScreenID;
    private System.Windows.Forms.Button RefRootFolder;
    private System.Windows.Forms.TextBox RootFolder;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem ModeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ModeClientWebAPIIToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ModeClientToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ModeWebAPIToolStripMenuItem;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel GeneraterStatus;
  }
}

