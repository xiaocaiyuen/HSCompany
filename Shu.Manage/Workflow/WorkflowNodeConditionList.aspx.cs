using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;

namespace Shu.Manage.Workflow
{
    public partial class WorkflowNodeConditionList : BasePage
    {
        protected BasePage bg = new BasePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        /// <summary>
        /// 列表绑定
        /// </summary>
        public void BindGrid()
        {
            UCEasyUIDataGrid.DataSource = "~/XML/Workflow/WorkflowNodeConditionList.xml";
            UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            //UCEasyUIDataGrid.AddType = 3;
            //UCEasyUIDataGrid.EditType = 3;
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

        protected string GetSqlWhere()
        {
            //查询的参数
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request["txtName"]))
            {
                strWhere.AppendFormat(" and TasksEx_Name like '%{0}%'", Request["txtName"]);
            }
            return strWhere.ToString();
        }
    }
}