using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Model;
using Shu.BLL;
using Shu.Comm;

namespace Shu.Manage.Sys
{
    public partial class RoleManage : BasePage
    {
        Sys_RolePurviewBLL dalUserRole = new Sys_RolePurviewBLL();
        Sys_RoleBLL bllRole = new Sys_RoleBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    hid_RoleID.Value = Request.QueryString["id"];
                    InitBind();
                }
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        public void InitBind()
        {
            if (hid_RoleID.Value != "")
            {
                Sys_Role modelRole = bllRole.Get(p => p.RoleID == hid_RoleID.Value);
                if (modelRole != null)
                {
                    FormModel.SetForm(this, modelRole, "txt");
                    hid_RoleName.Value = modelRole.Role_Name;
                }
                else
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "RoleList.aspx");
                }
            }
        }

        public Sys_Role GetBaseInfo()
        {

            Sys_Role modelRole = new Sys_Role();

            FormModel.GetForm(this, modelRole, "txt");

            modelRole.Role_AddUserID = "";

            modelRole.Role_AddTime = DateTime.Now;

            modelRole.RoleID = hid_RoleID.Value;

            modelRole.Role_AddUserID = CurrUserInfo().UserID;

            modelRole.Role_IsDel = "0";

            modelRole.Role_ShowType = Convert.ToInt32(ddlShowType.SelectedValue);
            return modelRole;
        }

        public List<Sys_RolePurview> SaveRole(string roleID)
        {
            string[] strList = hid_Save.Value.Split('|');

            List<Sys_RolePurview> list = new List<Sys_RolePurview>();

            if (strList.Length > 0)
            {
                foreach (string str in strList)
                {
                    if (str != "")
                    {
                        string[] roleInfo = str.Split('_');
                        string s = roleInfo[0];
                        List<Sys_RolePurview> rl = dalUserRole.GetList(p => p.RolePurview_RoleID == roleID && p.RolePurview_MenuCode == s).ToList();

                        Sys_RolePurview model = new Sys_RolePurview();
                        if (rl.Count > 0)
                        {
                            model = rl[0];
                        }
                        model.RolePurviewID = Guid.NewGuid().ToString();
                        model.RolePurview_RoleID = roleID;

                        model.RolePurview_MenuCode = roleInfo[0];

                        model.RolePurview_OperatePurview = roleInfo[1];




                        list.Add(model);
                    }
                }

            }

            return list;
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (hid_RoleID.Value == "")
            {
                Sys_Role model = GetBaseInfo();
                model.RoleID = Guid.NewGuid().ToString();

                if (bllRole.Add(model, SaveRole(model.RoleID)))
                {
                    MessageBox.ShowAndRedirect(this, "添加成功", "RoleList.aspx");
                }
                else
                {
                    MessageBox.ShowAndRedirect(this, "添加成功", "RoleList.aspx");
                }
            }
            else
            {
                Sys_Role model = GetBaseInfo();
                if (bllRole.Update(model, SaveRole(model.RoleID)))
                {
                    MessageBox.ShowAndRedirect(this, "修改成功", "RoleList.aspx");
                }
                else
                {
                    MessageBox.ShowAndRedirect(this, "修改成功", "RoleList.aspx");
                }
            }


        }
    }
}