using Client.Forms;
using Framework.Client.BaseClasses;
using System;
using System.Deployment.Application;
using System.Windows.Forms;

namespace Client
{
  static class Program
  {
    /// <summary>
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
    static void Main()
    {
      if (ApplicationDeployment.IsNetworkDeployed)
      {
        // BusinessBaseのWebAPIのベースURLに「ClickOnce実行時のURL」を設定
        var updateLocation = ApplicationDeployment.CurrentDeployment.UpdateLocation;
        BussinessBase.WebAPIBaseUrl = 
          string.Format("{0}://{1}:{2}/", updateLocation.Scheme, updateLocation.Host, updateLocation.Port);
      }

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Sample());
    }
  }
}
