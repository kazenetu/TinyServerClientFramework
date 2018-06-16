using Client.BaseClasses;
using Client.Business.OrderList;
using DataTransferObjects.CustomTables;
using DataTransferObjects.Request.OrderList;
using System;

namespace Client.Forms
{
  public partial class OrderListForm : FormBase
  {
    public OrderListForm()
    {
      InitializeComponent();
      OrderList.AutoGenerateColumns = false;
    }

    /// <summary>
    /// 検索処理
    /// </summary>
    private void SearchProc()
    {
      // クリア
      OrderList.DataSource = null;

      // パラメータ設定
      var request = new SearchRequest() { SearchUserID = UserID.Text };

      // 検索処理
      var result = new OrderListBusiness().Search(request);

      // 結果をグリッドに反映
      OrderList.DataSource = result.ResponseData.List;
      OrderList.Refresh();
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

      // 再検索する
      SearchProc();
    }

    /// <summary>
    /// 検索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Search_Click(object sender, EventArgs e)
    {
      // 検索する
      SearchProc();
    }

    /// <summary>
    /// 結果グリッドの更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderList_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
    {
      // Headerは何もせずに終了
      if (e.RowIndex < 0)
      {
        return;
      }

      var targetData = OrderList.Rows[e.RowIndex].DataBoundItem as CustomTOrder;
      if(targetData == null)
      {
        return;
      }

      // 更新ウィンドウを表示する
      var editWindow = new OrderEditForm(targetData.OrderNo,targetData.OrderUserId);
      if (editWindow.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
      {
        return;
      }

      // 再検索する
      SearchProc();
    }
  }
}
