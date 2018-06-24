using System;
using System.Text;
using TableDTOGenerater.Common.Interfaces;
using static TableDTOGenerater.Common.DatabaseData;

namespace TableDTOGenerater.Templates
{
  partial class TestTable : ITransformText
  {
    /// <summary>
    /// テーブル情報
    /// </summary>
    public TableData Table { set; get; }

    /// <summary>
    /// 登録メソッドのパラメータ取得
    /// </summary>
    /// <returns></returns>
    private string GetInsertMethodParam()
    {
      var sb = new StringBuilder();

      var isFirstItem = true;
      foreach (var column in Table.Columns)
      {
        if (!isFirstItem)
        {
          sb.AppendLine();
        }
        isFirstItem = false;

        sb.Append($"                              , {column.GetColumnTypeName()} {column.ColumnName}");
      }

      return sb.ToString();
    }

    /// <summary>
    /// 登録SQLのパラメータ取得
    /// </summary>
    /// <returns></returns>
    private string GetInsertSQLParams()
    {
      var sb = new StringBuilder();

      var isFirstItem = true;
      foreach (var column in Table.Columns)
      {
        if (!isFirstItem)
        {
          sb.AppendLine();
        }
        isFirstItem = false;

        sb.Append($"      db.AddParam(\"@{column.ColumnOriginalName.ToUpper()}\", targetDTO.{column.ColumnName});");
      }

      return sb.ToString();
    }

    /// <summary>
    /// 取得メソッドのパラメータ取得
    /// </summary>
    /// <returns></returns>
    private string GetFindMethodParam()
    {
      var sb = new StringBuilder();

      var isFirstItem = true;
      foreach (var column in Table.Columns)
      {
        if (!isFirstItem)
        {
          sb.AppendLine();
        }
        isFirstItem = false;

        var columType = column.GetColumnTypeName();
        if (columType != "string")
        {
          columType += "?";
        }

        sb.Append($"                              , {columType} {column.ColumnName} = null");
      }

      return sb.ToString();
    }

    /// <summary>
    /// 取得メソッドのコメント取得
    /// </summary>
    /// <returns>コメント</returns>
    private string GetFindMethodComments()
    {
      var sb = new StringBuilder();

      var isFirstItem = true;
      foreach (var column in Table.Columns)
      {
        if (!isFirstItem)
        {
          sb.AppendLine();
        }
        isFirstItem = false;
        sb.Append($"    /// <param name=\"{column.ColumnName}\">{column.ColumnLogicalName.Replace(Environment.NewLine, " ")}</param>");
      }

      return sb.ToString();
    }

    /// <summary>
    /// カラムタイプ名からSQLでシングルクォートまたはstring.Emptyを返す
    /// </summary>
    /// <param name="columnTypeName">カラムタイプ名</param>
    /// <returns>シングルクォートまたはstring.Empty</returns>
    private string GetSingleQuote(string columnTypeName)
    {
      switch (columnTypeName)
      {
        case nameof(Int32):
        case nameof(Int64):
        case nameof(Double):
        case nameof(Decimal):
        case nameof(Single):
          return string.Empty;
      }
      return "'";
    }

    /// <summary>
    /// カラムタイプ名からSQL結果からクラスインスタンスのキャスト文字列を返す
    /// </summary>
    /// <param name="columnData">カラム情報</param>
    /// <returns>キャスト文字列</returns>
    private string GetDBCoumnCastString(TableColumnData columnData)
    {
      switch (columnData.ColumnType.Name)
      {
        case nameof(String):
          return $"row[\"{columnData.ColumnOriginalName.ToUpper()}\"].ToString()";
      }
      return $"{columnData.GetColumnTypeName()}.Parse(row[\"{columnData.ColumnOriginalName.ToUpper()}\"].ToString())";
    }
  }
}

