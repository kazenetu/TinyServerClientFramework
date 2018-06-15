
namespace Client.Forms
{
  partial class OrderListForm
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.UserID = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.Search = new System.Windows.Forms.Button();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.Create = new System.Windows.Forms.Button();
      this.OrderNoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.OrderUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.UpdateColumn = new System.Windows.Forms.DataGridViewButtonColumn();
      this.tableLayoutPanel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.panel1);
      this.groupBox1.Controls.Add(this.UserID);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(794, 94);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "検索条件";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(35, 33);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(56, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "ユーザーID";
      // 
      // UserID
      // 
      this.UserID.Location = new System.Drawing.Point(97, 30);
      this.UserID.Name = "UserID";
      this.UserID.Size = new System.Drawing.Size(159, 19);
      this.UserID.TabIndex = 1;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.Create);
      this.panel1.Controls.Add(this.Search);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel1.Location = new System.Drawing.Point(698, 15);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(93, 76);
      this.panel1.TabIndex = 3;
      // 
      // Search
      // 
      this.Search.Location = new System.Drawing.Point(3, 50);
      this.Search.Name = "Search";
      this.Search.Size = new System.Drawing.Size(75, 23);
      this.Search.TabIndex = 3;
      this.Search.Text = "検索";
      this.Search.UseVisualStyleBackColor = true;
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderNoColumn,
            this.OrderUserName,
            this.UpdateColumn});
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(3, 103);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.RowTemplate.Height = 21;
      this.dataGridView1.Size = new System.Drawing.Size(794, 344);
      this.dataGridView1.TabIndex = 1;
      // 
      // Create
      // 
      this.Create.Location = new System.Drawing.Point(3, 7);
      this.Create.Name = "Create";
      this.Create.Size = new System.Drawing.Size(75, 23);
      this.Create.TabIndex = 4;
      this.Create.Text = "新規作成";
      this.Create.UseVisualStyleBackColor = true;
      this.Create.Click += new System.EventHandler(this.Create_Click);
      // 
      // OrderNoColumn
      // 
      this.OrderNoColumn.DataPropertyName = "OderNo";
      this.OrderNoColumn.Frozen = true;
      this.OrderNoColumn.HeaderText = "注文番号";
      this.OrderNoColumn.Name = "OrderNoColumn";
      this.OrderNoColumn.ReadOnly = true;
      // 
      // OrderUserName
      // 
      this.OrderUserName.DataPropertyName = "OrderUserName";
      this.OrderUserName.Frozen = true;
      this.OrderUserName.HeaderText = "注文者";
      this.OrderUserName.Name = "OrderUserName";
      this.OrderUserName.ReadOnly = true;
      this.OrderUserName.Width = 500;
      // 
      // UpdateColumn
      // 
      this.UpdateColumn.HeaderText = "更新";
      this.UpdateColumn.Name = "UpdateColumn";
      this.UpdateColumn.ReadOnly = true;
      // 
      // OrderListForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "OrderListForm";
      this.Text = "注文一覧";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox UserID;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button Search;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.Button Create;
    private System.Windows.Forms.DataGridViewTextBoxColumn OrderNoColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn OrderUserName;
    private System.Windows.Forms.DataGridViewButtonColumn UpdateColumn;
  }
}

