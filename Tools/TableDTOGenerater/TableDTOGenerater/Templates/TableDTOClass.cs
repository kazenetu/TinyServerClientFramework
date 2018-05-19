using System;
using System.Collections.Generic;
using System.Linq;
using static TableDTOGenerater.Common.DatabaseData;

namespace TableDTOGenerater.Templates
{
  partial class TableDTO
  {
    /// <summary>
    /// テーブル情報
    /// </summary>
    public TableData Table { set; get; }

    /// <summary>
    /// クラス名(テーブル名) 取得
    /// </summary>
    /// <returns>クラス名(テーブル名)</returns>
    private string getTableName()
    {
      return getSplitStrings(Table.TableLogicalName).First();
    }

    /// <summary>
    /// クラス(テーブル)備考 取得
    /// </summary>
    /// <returns>クラス(テーブル)備考</returns>
    private List<string> getTableRemarks()
    {
      return getSplitStrings(Table.TableLogicalName).Where((data) => data != getTableName()).ToList();
    }

    /// <summary>
    /// プロパティ名(カラム名) 取得
    /// </summary>
    /// <param name="columnLogicalName">カラム論理名</param>
    /// <returns>プロパティ名(カラム名)</returns>
    private string getColumnName(string columnLogicalName)
    {
      return getSplitStrings(columnLogicalName).First();
    }

    /// <summary>
    /// プロパティ(カラム)備考 取得
    /// </summary>
    /// <param name="columnLogicalName">カラム論理名</param>
    /// <returns>プロパティ(カラム)備考</returns>
    private List<string> getColumnRemarks(string columnLogicalName)
    {
      return getSplitStrings(columnLogicalName).Where((data) => data != getColumnName(columnLogicalName)).ToList();
    }

    /// <summary>
    /// 改行単位に文字列を切り出す
    /// </summary>
    /// <param name="src">改行情報も含めた文字列</param>
    /// <returns></returns>
    private string[] getSplitStrings(string src)
    {
      var srcData = src.Replace("\t", Environment.NewLine);
      return srcData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
    }
  }
}
