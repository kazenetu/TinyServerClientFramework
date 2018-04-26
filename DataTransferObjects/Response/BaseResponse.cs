using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Response
{
  /// <summary>
  /// Responseのスーパークラス
  /// </summary>
  public class BaseResponse<T> where T : class
  {
    /// <summary>
    /// ステータス列挙型
    /// </summary>
    public enum Results
    {
      OK, NG
    }

    /// <summary>
    /// ステータス
    /// </summary>
    protected Results result;

    /// <summary>
    /// ステータス(文字列)
    /// </summary>
    public string Result
    {
      get
      {
        return result.ToString();
      }
      set
      {
        Enum.TryParse(value,out result);
      }
    }

    /// <summary>
    /// エラーメッセージ
    /// </summary>
    public string ErrorMessage { set; get; }

    /// <summary>
    /// データ
    /// </summary>
    public T ResponseData { set; get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="result">結果</param>
    /// <param name="errorMessage">エラーメッセージ</param>
    public BaseResponse(Results result, string errorMessage)
    {
      this.result = result;
      this.ErrorMessage = errorMessage;
      ResponseData = null;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="result">結果</param>
    /// <param name="errorMessage">エラーメッセージ</param>
    /// <param name="responseData">データ</param>
    public BaseResponse(Results result, string errorMessage, T responseData)
    {
      this.result = result;
      this.ErrorMessage = errorMessage;
      this.ResponseData = responseData;
    }

    /// <summary>
    /// Windowsアプリ用コンストラクタ
    /// </summary>
    public BaseResponse()
    {
    }
  }
}
