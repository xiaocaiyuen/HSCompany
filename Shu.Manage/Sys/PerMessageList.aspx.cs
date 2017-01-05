using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;

namespace Shu.Manage.Sys
{
    public partial class PerMessageList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitGrid();
        }

        public void InitGrid()
        {

            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/PerMessageList.xml";
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
            if (!string.IsNullOrEmpty(Request["Message_Content"]))
            {
                strWhere.AppendFormat(" and Message_Content like '%{0}%'", Request["Message_Content"]);
            }
            return strWhere.ToString();

        }
    }
}