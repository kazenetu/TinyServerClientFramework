using ClientFramework.ConnectLib;
using DataTransferObjects.Request;
using DataTransferObjects.Response;
using System;
using System.Deployment.Application;
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
    /// WebAPIのルートURLを取得
    /// </summary>
    /// <param name="setApiPath">APIパスの追加</param>
    /// <returns>APIのルートURL</returns>
    private string getWebApiRootAddress(bool setApiPath)
    {
      var url = string.Empty;

      if (ApplicationDeployment.IsNetworkDeployed)
      {
        // ClickOnce実行時のURL
        var updateLocation = ApplicationDeployment.CurrentDeployment.UpdateLocation;
        url = string.Format("{0}://{1}:{2}/", updateLocation.Scheme, updateLocation.Host, updateLocation.Port);
      }
      else
      {
        // デバッグ実行時のURL
        var args = Environment.GetCommandLineArgs();
        if (args.Length >= 2)
        {
          url = args[1];
          if (url[url.Length - 1] != '/')
          {
            url += "/";
          }
        }
        else
        {
          url = "http://localhost:5000/";
        }
      }

      // APIパス追加
      if (setApiPath)
      {
        url += "api/";
      }

      return url;
    }

    private void webAPIGet_Click(object sender, EventArgs e)
    {
      try
      {
        var param = new LoginRequest() { ID = "test", Password = "test" };
        var uri = webAPIUrl.Text;

        HttpConnectLib.StubWebAPIDelegate stub = null;

#if STUB
        stub = (url, data) =>
        {
          var response = data as LoginResponse;
          response.ResponseData = new LoginResponse.LoginResponseParam() { Name = "ダミー" };
          return response;
        };
#endif

        var result = HttpConnectLib.Get<LoginResponse>(string.Format("{0}api/values?id={1}&password={2}", uri, param.ID, param.Password), stub);

        MessageBox.Show($"結果:{result.Result.ToString()}{Environment.NewLine}" +
                    $"message:{result.ErrorMessage}{Environment.NewLine}" +
                    $"data:name:{result.ResponseData.Name}");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void webAPIPost_Click(object sender, EventArgs e)
    {
      try
      {
        // WebAPIのルートURLを取得
        var uri = webAPIUrl.Text;

        // GetTokenメソッド発行
        HttpConnectLib.GetToken(string.Format("{0}", uri));

        HttpConnectLib.StubWebAPIDelegate stub = null;

#if STUB
        stub = (url, data) =>
        {
          var response = data as LoginResponse;
          response.ErrorMessage = "テストメッセージ";
          response.ResponseData = new LoginResponse.LoginResponseParam() { Name = "POSTダミー" };
          return response;
        };
#endif

        // ログインWebAPI発行
        var param = new LoginRequest() { ID = "test", Password = "test" };
        var results = HttpConnectLib.Post<LoginResponse>(string.Format("{0}api/values/login", uri), param, stub);

        // 結果を表示
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"result = {results.Result}");
        sb.AppendLine($"message = {results.ErrorMessage}");
        sb.AppendLine($"data = {results.ResponseData?.Name}");
        MessageBox.Show(sb.ToString());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

    }
  }
}
