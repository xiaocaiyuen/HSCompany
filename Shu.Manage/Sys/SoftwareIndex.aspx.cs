using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.BLL;
using Shu.Comm;
using Shu.Model;

namespace Shu.Manage.Sys
{
    public partial class SoftwareIndex : BasePage
    {
        protected List<Sys_Software> softwareList;
        Sys_ModelFileBLL bll = new Sys_ModelFileBLL();
        Sys_SoftwareBLL sfbll = new Sys_SoftwareBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitGrid();
        }
        public void InitGrid()
        {
            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridSoftwareList.xml";
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            GetList();
        }
        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            string id = HttpContext.Current.Request["id"];
            string Result = "";
            switch (active)
            {
                case "List"://加载列表
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
                    Response.End();
                    break;
                case "DelButton"://删除数据
                     //UCEasyUIDataGrid.DelInfo(id);
                    Result = this.DeleteSoftware(id);
                    Response.Write(Result);
                    //DelFile(id);
                    Response.End();

                    break;
                case "DeltBatchButton"://批量删除
                    Result = UCEasyUIDataGrid.BatchDelInfo(id);
                    Response.Write(Result.ToString());
                    //DelFiles();
                    Response.End();

                    break;
                default:
                    break;
            }

        }

        private string DeleteSoftware(string id)
        {
            Sys_Software software = sfbll.Find(s => s.SoftwareID == id);
            // 并不真实删除表中数据，仅仅更新字段，并保留删除信息
            software.Software_IsExists = 1;
            software.Software_DeleteTime = DateTime.Now;
            software.Software_DeleteUserID = CurrUserInfo().UserID;
            string msg = string.Empty;
            bool bol = sfbll.Update(software, out msg);
            // 得到ModelFile表中对象。
            var file = bll.Find(m => m.File_OperationID == id);
            FileDeleHandler fdh = new FileDeleHandler();
            // 删除Download中的文件
            string Result = fdh.PublicDele(file.FileID, "ww", file.File_Type);
            bll.Delete(file);
            return Result;
        }

        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1");
            if (!string.IsNullOrEmpty(Request["Notice_Title"]))
            {
                strWhere.AppendFormat(" and Notice_Title like '%{0}%'", Request["Notice_Title"]);
            }

            if (!string.IsNullOrEmpty(Request["txt_TimeFrom"]))
            {
                strWhere.AppendFormat(" and Notice_AddTime >='{0}'", Request["txt_TimeFrom"]);
            }
            if (!string.IsNullOrEmpty(Request["txt_TimeTo"]))
            {
                strWhere.AppendFormat(" and Notice_AddTime <='{0}'", Request["txt_TimeTo"] + " 23:59:59.00");
            }
            return strWhere.ToString();

        }
    }
}