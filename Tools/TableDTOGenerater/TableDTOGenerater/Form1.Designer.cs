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
      this.databaseCombo = new System.Windows.Forms.ComboBox();
      this.connectionString = new System.Windows.Forms.TextBox();
      this.generate = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(53, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "Database";
      // 
      // databaseCombo
      // 
      this.databaseCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.databaseCombo.FormattingEnabled = true;
      this.databaseCombo.Location = new System.Drawing.Point(82, 13);
      this.databaseCombo.Name = "databaseCombo";
      this.databaseCombo.Size = new System.Drawing.Size(144, 20);
      this.databaseCombo.TabIndex = 1;
      this.databaseCombo.SelectedValueChanged += new System.EventHandler(this.databaseCombo_SelectedValueChanged);
      // 
      // connectionString
      // 
      this.connectionString.Location = new System.Drawing.Point(242, 13);
      this.connectionString.Name = "connectionString";
      this.connectionString.Size = new System.Drawing.Size(449, 19);
      this.connectionString.TabIndex = 2;
      this.connectionString.Validating += new System.ComponentModel.CancelEventHandler(this.connectionString_Validating);
      // 
      // generate
      // 
      this.generate.Location = new System.Drawing.Point(714, 8);
      this.generate.Name = "generate";
      this.generate.Size = new System.Drawing.Size(75, 23);
      this.generate.TabIndex = 3;
      this.generate.Text = "生成";
      this.generate.UseVisualStyleBackColor = true;
      this.generate.Click += new System.EventHandler(this.generate_Click);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(15, 51);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textBox1.Size = new System.Drawing.Size(757, 299);
      this.textBox1.TabIndex = 4;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.generate);
      this.Controls.Add(this.connectionString);
      this.Controls.Add(this.databaseCombo);
      this.Controls.Add(this.label1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox databaseCombo;
    private System.Windows.Forms.TextBox connectionString;
    private System.Windows.Forms.Button generate;
    private System.Windows.Forms.TextBox textBox1;
  }
}

