using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.BLL;
using YDT.Comm;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class AuditAreaMappingPanel : BasePage
    {
        protected List<Sys_Role> listRole;
        private Sys_RoleBLL roleBLL = new Sys_RoleBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                listRole = roleBLL.FindWhere(p => p.Role_IsAdmin == false && p.Role_Name != Constant.SysAdminRoleName && p.Role_Name != Constant.SuperAdminRoleName).OrderBy(p => p.Role_Sequence).ToList();
            }
        }
    }
}