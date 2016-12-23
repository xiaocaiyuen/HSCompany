using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using Shu.Utility;

namespace Shu.Manage.Workflow
{
    public partial class MyWaitingTask : BasePage
    {
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
            UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyWaitingTask.xml";
            UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            if (!string.IsNullOrEmpty(base.RequstStr("MyDesktop")))
            {
                UCEasyUIDataGrid.OperatingType = 1010;
            }
            else
            {
                UCEasyUIDataGrid.OperatingType = 4;
            }
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
            var roleList = base.CurrUserInfo().RoleID.Split(',');
            if (roleList.Length > 1)
            {
                string sqlin = string.Empty;
                foreach (var roleid in roleList)  //循环当前用户的角色
                {
                    sqlin += "'" + roleid + "',";
                }
                sqlin = sqlin.Substring(0, sqlin.Length - 1);
                strWhere.Append(" 1=1 and (PendingMatter_ToRoleName in ("+ sqlin + ") Or PendingMatter_ToRoleName='" + base.CurrUserInfo().UserID + "') ");
            }
            else
            {
                strWhere.Append(" 1=1 and (PendingMatter_ToRoleName Like '%" + base.CurrUserInfo().RoleID + "%' Or PendingMatter_ToRoleName='" + base.CurrUserInfo().UserID + "') ");
            }
            
            if (!string.IsNullOrEmpty(Request["txtName"]))
            {
                strWhere.AppendFormat(" and PendingMatter_Title like '%{0}%'", Request["txtName"]);
            }
            return strWhere.ToString();
        }
    }
}