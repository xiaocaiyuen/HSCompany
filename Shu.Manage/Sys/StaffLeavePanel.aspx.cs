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
using Shu.Utility.Extensions;

namespace Shu.Manage.Sys
{
    public partial class StaffLeavePanel : BasePage
    {
        protected List<Sys_Role> listRole;
        private Sys_RoleBLL roleBLL = new Sys_RoleBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listRole = roleBLL.GetList(p => p.Role_Name != Constant.SysAdminRoleName && p.Role_Name != Constant.SuperAdminRoleName).OrderBy(p => p.Role_Sequence).ToList();//p.Role_IsAdmin == false &&
            }
            InitGrid();
        }

        /// <summary>
        /// 初始化表格列表
        /// </summary>
        public void InitGrid()
        {
            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/StaffLeavePanel.xml";
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            GetList();
        }

        /// <summary>
        /// 方法
        /// </summary>
        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            string id = HttpContext.Current.Request["id"];
            string optionCode = HttpContext.Current.Request["OptionCode"];
            switch (active)
            {
                case "List"://加载列表
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
                    Response.End();
                    break;
                case "UserGotoWorkButton":
                    Response.Write(UserModelSetting(id, optionCode));
                    Response.End();
                    break;
                case "UserLeaveButton":
                    Response.Write(UserModelSetting(id, optionCode));
                    Response.End();
                    break;
                case "UserLockButton":
                    Response.Write(UserModelSetting(id, optionCode));
                    Response.End();
                    break;
                default:
                    break;
            }
        }



        /// <summary>
        /// 用户模式设置
        /// </summary>
        public string UserModelSetting(string id, string code)
        {
            if (id.Length > 0)
            {
                Sys_UserInfoBLL bllUser = new Sys_UserInfoBLL();
                Sys_UserInfo UserInfo = bllUser.Get(p => p.UserInfoID == id);
                if (UserInfo.IsNotNull())
                {
                    UserInfo.UserInfo_WorkingState = code;
                    if (bllUser.Update(UserInfo))
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
            strWhere.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request["ddlRole"]))
            {
                strWhere.AppendFormat(" and UserInfo_RoleID Like '%{0}%'", Request["ddlRole"]);
            }
            return strWhere.ToString();
        }
    }
}