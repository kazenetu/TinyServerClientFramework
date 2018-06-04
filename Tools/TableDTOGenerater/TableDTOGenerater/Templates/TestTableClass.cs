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

      foreach (var column in Table.Columns)
      {
        sb.AppendLine($"                            , {column.GetColumnTypeName()} {column.ColumnName}");
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

      foreach (var column in Table.Columns)
      {
        sb.AppendLine($"    db.AddParam(\"@{column.ColumnOriginalName.ToUpper()}\",{column.ColumnName});");
      }

      return sb.ToString();
    }

    /// <summary>
    /// 登録メソッドのコメント取得
    /// </summary>
    /// <returns>コメント</returns>
    private string GetInsertMethodComments()
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
        sb.Append($"  /// <param name=\"{column.ColumnName}\">{column.ColumnLogicalName.Replace(Environment.NewLine, " ")}</param>");
      }

      return sb.ToString();
    }
  }
}
