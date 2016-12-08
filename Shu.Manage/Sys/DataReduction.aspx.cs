using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using System.Data;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;

namespace YDT.Web.Manage.Sys
{
    public partial class DataReduction : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitBind();
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void InitBind()
        {
            string path = Server.MapPath(CommTools.GetConfigName("dbPath"));
            GetFiles(path);
        }

        ///<summary>
        ///遍文件夹下的所有子文件夹下的文件
        ///</summary>
        ///<param name="ObjDirPath">文件夹</param>
        public void GetFiles(string path)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("fileName", typeof(string)));
            dt.Columns.Add(new DataColumn("createTime", typeof(string)));
            dt.Columns.Add(new DataColumn("filePath", typeof(string)));
            DataRow dr;

            DirectoryInfo SourceDir = new DirectoryInfo(path);

            foreach (FileSystemInfo FSI in SourceDir.GetFileSystemInfos())
            {
                DateTime createTime = FSI.CreationTime;
                string strCreateTime = createTime.ToString("yyyy年MM月dd日 ") + createTime.Hour + "时" + createTime.Hour + "分" + createTime.Second + "秒";
                string fileName = FSI.FullName.Substring(FSI.FullName.LastIndexOf("\\") + 1);
                string filePath = FSI.FullName.Substring(0, FSI.FullName.LastIndexOf("\\"));

                dr = dt.NewRow();
                dr[0] = fileName;
                dr[1] = createTime;
                dr[2] = filePath;
                dt.Rows.Add(dr);

            }
            gridFiles.DataSource = dt;
            gridFiles.DataBind();
        }

        /// <summary>
        /// 数据还原
        /// </summary>
        /// <param name="fileName"></param>
        protected void DataHy(string fileName)
        {

            string strSql = @"SELECT spid FROM  master.dbo.sysprocesses , master.dbo.sysdatabases WHERE sysprocesses.dbid=sysdatabases.dbid AND sysdatabases.Name='" + CommTools.GetConfigName("dbName") + "'";
            Database db = DatabaseFactory.CreateDatabase("conMaster");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());

            IDataReader dr;
            dr =db.ExecuteReader(dbCommand);
            ArrayList list = new ArrayList();
            while (dr.Read())
            {
                list.Add(dr.GetInt16(0));
            }
            dr.Close();
            for (int i = 0; i < list.Count; i++)
            {

                dbCommand = db.GetSqlStringCommand(string.Format("KILL {0}", list[i]));
                db.ExecuteNonQuery(dbCommand);
            }

            string restoreSql = @"use master;restore database " + CommTools.GetConfigName("dbName") + " from disk='" + fileName + "' WITH REPLACE  ";
            DbCommand cmdRT = db.GetSqlStringCommand(restoreSql.ToString());
            try
            {
                db.ExecuteNonQuery(cmdRT);
                MessageBox.Show(this, "数据还原成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "数据还原失败");
            }
        }


        protected void gridFiles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string fileName = gridFiles.DataKeys[e.NewEditIndex].Value.ToString();
            string filePath = Server.MapPath(CommTools.GetConfigName("dbPath")) + "\\" + fileName;
            DataHy(filePath);
            InitBind();
        }
    }
}