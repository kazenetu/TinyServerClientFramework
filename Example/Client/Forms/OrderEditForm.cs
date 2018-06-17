using Client.BaseClasses;
using Client.Business.OrderEdit;
using DataTransferObjects.Request.OrderEdit;
using System;
using System.Windows.Forms;

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
    /// ユーザー名取得
    /// </summary>
    /// <param name="isShowMessage">メッセージ表示するか</param>
    /// <returns></returns>
    private bool SetUserName(bool isShowMessage)
    {
      // パラメータ設定
      var request = new FindUserNameRequest() { OrderUserID = UserID.Text };

      // 検索処理
      var result = new OrderEditBusiness().FindUserName(request);

      // 結果判定
      if(result.Result == Statics.ResultNG)
      {
        UserName.Text =  string.Empty;

        if (isShowMessage)
        {
          // エラーメッセージ表示
          MessageBox.Show(result.ErrorMessage);
        }

        return false;
      }

      // ユーザー名の設定
      UserName.Text = result.ResponseData.OrderUserName;

      return true;
    }

    /// <summary>
    /// 更新用コンストラクタ
    /// </summary>
    /// <param name="orderNo">注文番号</param>
    public OrderEditForm(int orderNo)
    {
      InitializeComponent();

      // 値を設定
      IsModify = true;
      OrderNo.Text = orderNo.ToString();

      // パラメータ設定
      var request = new InitializeRequest() { OrderNo = orderNo };

      var business = new OrderEditBusiness();
      var result = business.Initialize(request);

      // DBからユーザー名を取得
      UserID.Text = result.ResponseData.OrderUserID;

      // ユーザー名取得
      SetUserName(false);
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Save_Click(object sender, EventArgs e)
    {
      // ユーザー名取得
      if (!SetUserName(true))
      {
        // 取得失敗時は終了
        return;
      }

      // 登録・更新処理
      if (IsModify)
      {
        // TODO 更新処理
      }
      else
      {
        // TODO 登録処理
      }

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

    /// <summary>
    /// ユーザーIDのロストフォーカス
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserID_Leave(object sender, EventArgs e)
    {
      // TODO ユーザーIDに紐づくユーザー名を表示
    }
  }
}
