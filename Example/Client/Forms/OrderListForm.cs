using Client.BaseClasses;
using System;

namespace Client.Forms
{
  public partial class OrderListForm : FormBase
  {
    public OrderListForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// 新規作成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Create_Click(object sender, EventArgs e)
    {
      var editWindow = new OrderEditForm();
      if (editWindow.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
      {
        return;
      }

      // TODO 再検索する
    }
  }
}
