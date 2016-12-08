using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shu.Comm;

namespace Shu.Manage.Sys
{
    public partial class RoleList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                InitGrid();
            //}
        }
        public void InitGrid()
        {

            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridRole.xml";
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
                    Result = UCEasyUIDataGrid.DelInfo(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                case "DeltBatchButton"://批量删除
                    Result = UCEasyUIDataGrid.BatchDelInfo(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                default:
                    break;
            }
        }

        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1");
            if (!string.IsNullOrEmpty(Request["txtRoleName"]))
            {
                strWhere.AppendFormat(" and Role_Name like '%{0}%'", Request["txtRoleName"]);
            }
            if (!base.CurrUserInfo().RoleName.Contains("超级管理员"))
            {
                strWhere.AppendFormat(" and Role_Name<>'超级管理员'");
            }
            return strWhere.ToString();

        }
    }
}