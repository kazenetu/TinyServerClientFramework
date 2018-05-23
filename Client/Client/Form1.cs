using Client.Business;
using DataTransferObjects.Request.User;
using System;
using System.Windows.Forms;

namespace Client
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    /// <summary>
    /// GETボタンクリック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>Get発行サンプル</remarks>
    private void webAPIGet_Click(object sender, EventArgs e)
    {
      try
      {
        var param = new LoginRequest() { ID = "test", Password = "test" };

        // GET WebAPI発行
        var business = new SampleBusiness();
        var result = business.GetSample(param);

        MessageBox.Show($"結果:{result.Result.ToString()}{Environment.NewLine}" +
                    $"message:{result.ErrorMessage}{Environment.NewLine}" +
                    $"data:name:{result.ResponseData.Name}");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    /// <summary>
    /// POSTボタンクリック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>POST発行サンプル</remarks>
    private void webAPIPost_Click(object sender, EventArgs e)
    {
      try
      {
        var param = new LoginRequest() { ID = "test", Password = "test" };

        // ログインWebAPI発行
        var business = new SampleBusiness();
        var result = business.PostSample(param);

        // 結果を表示
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"result = {result.Result}");
        sb.AppendLine($"message = {result.ErrorMessage}");
        sb.AppendLine($"data = {result.ResponseData?.Name}");
        MessageBox.Show(sb.ToString());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

    }
  }
}
