using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDTOGenerater.Common;

namespace TableDTOGenerater
{
  static class Program
  {
    /// <summary>
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
    static void Main()
    {
      // DBごとの接続文字列の読み込み
      DatabaseData.GetInstance().LoadDatabaseConnectionStrings(Application.StartupPath);

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());

      // DBごとの接続文字列の保存
      DatabaseData.GetInstance().SaveDatabaseConnectionStrings(Application.StartupPath);
    }
  }
}
