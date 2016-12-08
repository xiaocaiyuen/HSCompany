using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using System.Data;
using System.IO;
using YDT.Model;
using YDT.BLL;

namespace YDT.Web.Manage.Sys
{
    public partial class Educe : BasePage
    {
        Sys_DepartmentBLL depbll = new Sys_DepartmentBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn_Export_Click();
            }
        }
        public void btn_Export_Click()
        {

            string sqla = "select Department_Code as '部门编码',Department_Name as '部门名称',Department_ParentCode as '上级部门编码',Department_ParentCode as '上级部门名称' from Sys_Department ORDER BY Department_Level,Department_Sequence  asc";
            DataSet da = Pagin.Query(sqla);
            DataTable dt = da.Tables[0];

            string ph = ExportExcel(dt);
            Response.Write(ph);

        }




        /// <summary>
        ///
        /// </summary>
        /// <param name="dt"></param>
        protected String ExportExcel(DataTable dt)
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
            Microsoft.Office.Interop.Excel.Range range;
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
                    if (i==3)
                    {
                       string a= dt.Rows[r][i].ToString();
                       if (a!="0")
                       {
                           Sys_Department dep = depbll.Find(p => p.Department_Code == a);
                           if (dep != null)
                           {
                               worksheet.Cells[r + 2, i + 1] = dep.Department_Name;
                           }
                       }
                    }
                    else if (i == 2)
                    {
                        worksheet.Cells[r + 2, i + 1] ="'"+dt.Rows[r][i].ToString();
                    }
                    else if (i == 0)
                    {
                        worksheet.Cells[r + 2, i + 1] = "'" +dt.Rows[r][i].ToString();
                    }
                    else
                    {
                        worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                    }

                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            //xlApp.Visible = true;
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