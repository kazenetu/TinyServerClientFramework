using Client.BaseClasses;
using System;

namespace Client.Forms
{
  public partial class OrderEditForm : FormBase
  {
    /// <summary>
    /// 更新か否か
    /// </summary>
    private bool IsModify = false;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public OrderEditForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// 更新用コンストラクタ
    /// </summary>
    /// <param name="orderNo">注文番号</param>
    /// <param name="orderUserID">注文者ID</param>
    public OrderEditForm(int orderNo,string orderUserID)
    {
      InitializeComponent();

      // 値を設定
      IsModify = true;
      OrderNo.Text = OrderNo.ToString();
      UserID.Text = orderUserID;

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
