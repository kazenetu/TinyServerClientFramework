
namespace Client.Forms
{
  partial class OrderEditForm
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
      this.OrderNo = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.UserID = new System.Windows.Forms.TextBox();
      this.UserName = new System.Windows.Forms.TextBox();
      this.Cancel = new System.Windows.Forms.Button();
      this.Save = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(21, 31);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(53, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "注文番号";
      // 
      // OrderNo
      // 
      this.OrderNo.Location = new System.Drawing.Point(106, 28);
      this.OrderNo.Name = "OrderNo";
      this.OrderNo.ReadOnly = true;
      this.OrderNo.Size = new System.Drawing.Size(127, 19);
      this.OrderNo.TabIndex = 1;
      this.OrderNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(21, 68);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(56, 12);
      this.label2.TabIndex = 0;
      this.label2.Text = "ユーザーID";
      // 
      // UserID
      // 
      this.UserID.Location = new System.Drawing.Point(106, 65);
      this.UserID.Name = "UserID";
      this.UserID.Size = new System.Drawing.Size(127, 19);
      this.UserID.TabIndex = 1;
      this.UserID.Leave += new System.EventHandler(this.UserID_Leave);
      // 
      // UserName
      // 
      this.UserName.Enabled = false;
      this.UserName.Location = new System.Drawing.Point(239, 65);
      this.UserName.Name = "UserName";
      this.UserName.ReadOnly = true;
      this.UserName.Size = new System.Drawing.Size(127, 19);
      this.UserName.TabIndex = 1;
      // 
      // Cancel
      // 
      this.Cancel.Location = new System.Drawing.Point(291, 138);
      this.Cancel.Name = "Cancel";
      this.Cancel.Size = new System.Drawing.Size(75, 23);
      this.Cancel.TabIndex = 2;
      this.Cancel.Text = "キャンセル";
      this.Cancel.UseVisualStyleBackColor = true;
      this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
      // 
      // Save
      // 
      this.Save.Location = new System.Drawing.Point(106, 138);
      this.Save.Name = "Save";
      this.Save.Size = new System.Drawing.Size(75, 23);
      this.Save.TabIndex = 2;
      this.Save.Text = "保存";
      this.Save.UseVisualStyleBackColor = true;
      this.Save.Click += new System.EventHandler(this.Save_Click);
      // 
      // OrderEditForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(414, 202);
      this.ControlBox = false;
      this.Controls.Add(this.Save);
      this.Controls.Add(this.Cancel);
      this.Controls.Add(this.UserName);
      this.Controls.Add(this.UserID);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.OrderNo);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "OrderEditForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "注文登録";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox OrderNo;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox UserID;
    private System.Windows.Forms.TextBox UserName;
    private System.Windows.Forms.Button Cancel;
    private System.Windows.Forms.Button Save;
  }
}

