using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SourceGenerater
{
  static class Program
  {
    /// <summary>
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
    static void Main()
    {
      // 設定ファイルの読み込み
      GeneraterEngine.GenerateClient.GetInstance().Load(Application.StartupPath);

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());

      // 設定ファイルの書き込み
      GeneraterEngine.GenerateClient.GetInstance().Save(Application.StartupPath);
    }
  }
}
