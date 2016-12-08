using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.BLL;
using Shu.Comm;

namespace Shu.Manage.Sys
{
    public partial class EmployeeLeaveSetting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitGrid();
        }

        /// <summary>
        /// 初始化表格列表
        /// </summary>
        public void InitGrid()
        {
            hidUserInfoID.Value = base.RequstStr("UserInfoID");
            hidCond.Value = base.RequstStr("cond");
            hidRoleID.Value = base.RequstStr("roleid");
            if (hidCond.Value == "0")
            {
                this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/EmployeeLeaveSetting.xml";
            }
            if (hidCond.Value == "1")
            {
                this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/EmployeeLeaveSettingWait.xml";
            }
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            GetList();
        }


        /// <summary>
        /// 方法
        /// </summary>
        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            string ids = HttpContext.Current.Request["ids"];
            switch (active)
            {
                case "List"://加载列表
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
                    Response.End();
                    break;
                case "AssignmentMode"://转入人工分单模式
                    Response.Write(AssignmentMode(ids));
                    Response.End();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 用户模式设置
        /// </summary>
        public string AssignmentMode(string ids)
        {
            if (ids.Length > 0)
            {
                ApprovalPool_TaskPoolBLL TaskPoolBLL = new ApprovalPool_TaskPoolBLL();
                if (TaskPoolBLL.AutoModeConvertToManualMode(ids))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            if (hidCond.Value == "0")
            {
                strWhere.Append(" 1=1 and PendingMatter_ToRoleName='" + hidUserInfoID.Value + "'");
            }
            if (hidCond.Value == "1")
            {
                //strWhere.Append(" 1=1 and SysAutoTaskPool_InputResWFRoleID='" + hidRoleID.Value + "' Or SysAutoTaskPool_InputResWFRoleID='" + hidUserInfoID.Value + "'");
            }
            return strWhere.ToString();
        }
    }
}