using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace Shu.Comm
{
    public class ExportExcel
    {
        /// <summary>
        /// 组件导出Excel
        /// 返回的是文件路径，如果返回是“0”导出失败（失败：1、DataTable为null或者没有数据，2、没有Excel组件，3、文件存放错误）
        /// </summary>
        /// <param name="dt">数据DataTable</param>
        /// <param name="WorkBookName">导出的Excel的工作簿名称</param>
        /// <returns></returns>
        public string ExportExcels(DataTable dt, string WorkBookName)
        {
            if (dt == null || dt.Rows.Count == 0) return "0";
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return "0";
            }
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            worksheet.Name = WorkBookName;
            Microsoft.Office.Interop.Excel.Range range;
            range = worksheet.Range["A1", worksheet.Cells[1, dt.Columns.Count]];
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            //xlApp.Visible = true;
            range.NumberFormatLocal = "@";
            workbook.Saved = true;
            string tempPath = UploadFileCommon.CreateDir("EXL");
            if (!Directory.Exists(tempPath))//查看当前文件夹是否存在
            {
                Directory.CreateDirectory(tempPath);
            }
            try
            {
                string sNewFileName = DateTime.Now.ToString("yyyyMMddhhmmsfff");//上传后的文件名字
                string ph = tempPath + @"/" + sNewFileName + ".xlsx";
                workbook.SaveAs(System.Web.HttpContext.Current.Request.MapPath(ph));
                return ph;
            }
            catch { return "0"; }
            finally
            {
                workbook.Close(true, Type.Missing, Type.Missing);
                workbook = null;
                xlApp.Quit();
                xlApp = null;
            }
        }
    }
}
