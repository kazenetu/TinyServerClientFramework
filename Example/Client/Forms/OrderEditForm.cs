using Client.BaseClasses;
using System;

namespace Client.Forms
{
  public partial class OrderEditForm : FormBase
  {
    public OrderEditForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Save_Click(object sender, EventArgs e)
    {
      // TODO 登録・更新処理


      // OKを返す
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }

    /// <summary>
    /// キャンセル
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Cancel_Click(object sender, EventArgs e)
    {
      // Cancelを返す
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
  }
}
