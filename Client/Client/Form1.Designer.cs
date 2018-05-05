namespace Client
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
      this.webAPIGet = new System.Windows.Forms.Button();
      this.webAPIPost = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // webAPIGet
      // 
      this.webAPIGet.Location = new System.Drawing.Point(24, 65);
      this.webAPIGet.Name = "webAPIGet";
      this.webAPIGet.Size = new System.Drawing.Size(75, 23);
      this.webAPIGet.TabIndex = 2;
      this.webAPIGet.Text = "Getメソッド";
      this.webAPIGet.UseVisualStyleBackColor = true;
      this.webAPIGet.Click += new System.EventHandler(this.webAPIGet_Click);
      // 
      // webAPIPost
      // 
      this.webAPIPost.Location = new System.Drawing.Point(24, 106);
      this.webAPIPost.Name = "webAPIPost";
      this.webAPIPost.Size = new System.Drawing.Size(75, 23);
      this.webAPIPost.TabIndex = 2;
      this.webAPIPost.Text = "Postメソッド";
      this.webAPIPost.UseVisualStyleBackColor = true;
      this.webAPIPost.Click += new System.EventHandler(this.webAPIPost_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.webAPIPost);
      this.Controls.Add(this.webAPIGet);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button webAPIGet;
    private System.Windows.Forms.Button webAPIPost;
  }
}

