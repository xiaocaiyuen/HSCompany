using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Data;
using System.Web;
using System.IO;

namespace Shu.Comm
{
    public class AsposeExcel
    {
        /// <summary> 
        /// 测试程序 
        /// </summary> 
        public static void testOut()
        {

            //DataTable dt = new DataTable();
            //dt.Columns.Add("name");
            //dt.Columns.Add("sex");
            //DataRow dr = dt.NewRow();
            //dr["name"] = "名称1";
            //dr["sex"] = "性别1";
            //dt.Rows.Add(dr);

            //DataRow dr1 = dt.NewRow();
            //dr1["name"] = "名称2";
            //dr1["sex"] = "性别2";
            //dt.Rows.Add(dr1);

            //OutFileToDisk(dt, "测试标题", @"d:\测试.xls");
        }

        /// <summary> 
        /// 导出数据到本地 
        /// </summary> 
        /// <param name="dt">要导出的数据</param> 
        /// <param name="WorkBookName">导出的Excel的工作簿名称</param>
        public static string OutFileToDisk(DataTable dt, string WorkBookName, bool IsDownload = true)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            sheet.Name = WorkBookName;
            Cells cells = sheet.Cells;//单元格 

            //为标题设置样式     
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleTitle.Font.Name = "宋体";//文字字体 
            styleTitle.Font.Size = 18;//文字大小 
            styleTitle.Font.IsBold = true;//粗体 

            //样式2 
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style2.Font.Name = "宋体";//文字字体 
            style2.Font.Size = 14;//文字大小 
            style2.Font.IsBold = true;//粗体 
            //style2.IsTextWrapped = true;//单元格内容自动换行 
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3 
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style3.Font.Name = "宋体";//文字字体 
            style3.Font.Size = 12;//文字大小 
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数 

            ////生成行1 标题行    
            //cells.Merge(0, 0, 1, Colnum);//合并单元格 
            //cells[0, 0].PutValue(tableName);//填写内容 
            //cells[0, 0].SetStyle(styleTitle);
            //cells.SetRowHeight(0, 38);

            //生成行2 列名行 
            for (int i = 0; i < Colnum; i++)
            {
                cells[0, i].PutValue(dt.Columns[i].ColumnName);
                cells[0, i].SetStyle(style2);
                cells.SetColumnWidth(i, 20);
                cells.SetRowHeight(0, 25);
            }

            //生成数据行 
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    cells[1 + i, k].PutValue(dt.Rows[i][k].ToString());
                    cells[1 + i, k].SetStyle(style3);
                }
                cells.SetRowHeight(1 + i, 24);
            }

            string tempPath = UploadFileCommon.CreateDir("EXL");
            if (!Directory.Exists(tempPath))//查看当前文件夹是否存在
            {
                Directory.CreateDirectory(tempPath);
            }
            try
            {
                // string sNewFileName = DateTime.Now.ToString("yyyyMMddhhmmsfff");//上传后的文件名字
                string sNewFileName = WorkBookName;
                string ph = tempPath + @"/" + sNewFileName + ".xlsx";
                workbook.Save(System.Web.HttpContext.Current.Request.MapPath(ph));
                if (IsDownload)
                {
                    ExportToExcel(ph);//导出Excel
                }
                return ph;
            }
            catch { return "0"; }
        }

        public static MemoryStream OutFileToStream(DataTable dt, string tableName)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格 

            //为标题设置样式     
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleTitle.Font.Name = "宋体";//文字字体 
            styleTitle.Font.Size = 18;//文字大小 
            styleTitle.Font.IsBold = true;//粗体 

            //样式2 
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style2.Font.Name = "宋体";//文字字体 
            style2.Font.Size = 14;//文字大小 
            style2.Font.IsBold = true;//粗体 
            style2.IsTextWrapped = true;//单元格内容自动换行 
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3 
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style3.Font.Name = "宋体";//文字字体 
            style3.Font.Size = 12;//文字大小 
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数 

            //生成行1 标题行    
            cells.Merge(0, 0, 1, Colnum);//合并单元格 
            cells[0, 0].PutValue(tableName);//填写内容 
            cells[0, 0].SetStyle(styleTitle);
            cells.SetRowHeight(0, 38);

            //生成行2 列名行 
            for (int i = 0; i < Colnum; i++)
            {
                cells[1, i].PutValue(dt.Columns[i].ColumnName);
                cells[1, i].SetStyle(style2);
                cells.SetRowHeight(1, 25);
            }

            //生成数据行 
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    cells[2 + i, k].PutValue(dt.Rows[i][k].ToString());
                    cells[2 + i, k].SetStyle(style3);
                }
                cells.SetRowHeight(2 + i, 24);
            }

            MemoryStream ms = workbook.SaveToStream();
            return ms;
        }

        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="fileName">含该文件的相对地址</param>
        public static void ExportToExcel(string fileName)
        {
            HttpContext.Current.Response.ContentType = "application/ms-download";
            string s_path = HttpContext.Current.Server.MapPath(fileName);
            FileInfo file = new FileInfo(s_path);
            //判断浏览器版本
            string UserAgent = HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower();
            string filename = file.Name;
            //非火狐浏览器 
            if (UserAgent.IndexOf("firefox") == -1)
            {
                filename = HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8);
            }
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                HttpContext.Current.Response.WriteFile(file.FullName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Clear();
                File.Delete(s_path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 输出到下载文件        /// <summary>
        /// 文件流下载文件
        /// </summary>
        /// <param name="memoryStram">内存流</param>
        /// <param name="fileName">文件名称</param>
        public static void DownLoad(MemoryStream memoryStram, string fileName)
        {
            //设置输出编码格式
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //设置输出流
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            //防止中文乱码
            fileName = HttpUtility.UrlEncode(fileName);
            //设置输出文件名
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename = " + fileName + ".xls");
            //输出
            HttpContext.Current.Response.BinaryWrite(memoryStram.GetBuffer());
        }
        #endregion
    }
}
