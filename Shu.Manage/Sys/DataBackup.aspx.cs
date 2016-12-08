using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.IO;

using YDT.Model;
using YDT.BLL;
using System.Text;
using YDT.Utility;

namespace YDT.Web.Manage.Sys
{
    public partial class DataBackup : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitBind();
            }
            BindGrid();
        }

        /// <summary>
        /// 列表绑定
        /// </summary>
        public void BindGrid()
        {
            UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridBackup.xml";
            UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            GetList();
        }


        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            string id = HttpContext.Current.Request["id"];
            switch (active)
            {
                case "List"://加载列表
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
                    Response.End();
                    break;
                default:
                    break;
            }
        }

        protected string GetSqlWhere()
        {
            //查询的参数
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            return strWhere.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void InitBind()
        {
            //得到文件的名称
            string fileName = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            string filePath = Server.MapPath(CommTools.GetConfigName("dbPath"));
            lblFileName.Text = fileName;
            lblFilePath.Text = filePath;
        }


        /// <summary>
        /// 数据备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, ImageClickEventArgs e)
        {
            string filepath = lblFilePath.Text + "\\" + lblFileName.Text + ".bak";
            DataBackUp(filepath);

        }

        protected void DataBackUp(string filepath)
        {
            try
            {
                string strSql = @"backup database " + CommTools.GetConfigName("dbName") + " to disk='" + filepath + "' with init";
                Database db = DatabaseFactory.CreateDatabase("conMaster");
                DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());

                db.ExecuteNonQuery(dbCommand);
                string msg = string.Empty;
                Sys_Backup entity = new Sys_Backup();
                Sys_BackupBLL bll = new Sys_BackupBLL();
                entity.Backup_AddTime = DateTime.Now;
                entity.Backup_AddUserID = CurrUserInfo().UserID;
                entity.Backup_Name = lblFileName.Text;
                entity.Backup_Path = filepath;
                entity.BackupID = Guid.NewGuid().ToString();
                if (!bll.Add(entity, out msg))
                {
                    MessageBox.Show(this, msg);
                }

                MessageBox.Show(this, "数据备份成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "数据备份失败");
            }
        }
    }
}