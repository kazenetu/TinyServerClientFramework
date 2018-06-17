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
    /// 注文番号のバージョン
    /// </summary>
    /// <remarks>新規作成時は-1</remarks>
    private int OrderVersion = -1;

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

      // DBからユーザー名・更新バージョンを取得
      UserID.Text = result.ResponseData.OrderUserID;
      OrderVersion = result.ResponseData.ModVersion;

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

      try
      {
        var result = string.Empty;
        var resultOrderNo = 0;
        var errorMessage = string.Empty;
        if (IsModify)
        {
          // パラメータ設定
          var request = new ModifyRequest();
          request.OrderNo = int.Parse(OrderNo.Text);
          request.OrderUserID = UserID.Text;
          request.ModVersion = OrderVersion;

          // 更新処理
          var resultDTO = new OrderEditBusiness().Modify(request);

          // 結果取得
          result = resultDTO.Result;
          resultOrderNo = resultDTO.ResponseData.OrderNo;
          errorMessage = resultDTO.ErrorMessage;
        }
        else
        {
          // パラメータ設定
          var request = new SaveRequest();
          request.OrderNo = -1;
          request.OrderUserID = UserID.Text;
          request.ModVersion = OrderVersion;

          // 登録処理
          var resultDTO = new OrderEditBusiness().Save(request);

          // 結果取得
          result = resultDTO.Result;
          resultOrderNo = resultDTO.ResponseData.OrderNo;
          errorMessage = resultDTO.ErrorMessage;
        }

        // 更新結果確認
        if (result == Statics.ResultOK)
        {
          // 注文番号を設定
          OrderNo.Text = resultOrderNo.ToString();

          MessageBox.Show("保存完了しました。");

          // OKを返す
          DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        else
        {
          MessageBox.Show(errorMessage);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);

      }
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
      // ユーザー名取得
      SetUserName(false);
    }
  }
}
