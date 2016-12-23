using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.BLL;
using Shu.Comm;
using Shu.Model;
using Shu.Utility.Extensions;

namespace Shu.Manage.Workflow
{
    public partial class WorkflowConfigureEx : BasePage
    {
        Workflow_NodeConfigExBLL bll = new Workflow_NodeConfigExBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Show();

                BingDDL();
            }
        }

        private void BingDDL()
        {

        }

        private void Show()
        {
            string id = Request["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Workflow_NodeConfigEx entity = new Workflow_NodeConfigEx();
                Sys_UserInfoBLL UserBll = new Sys_UserInfoBLL();
                Sys_RoleBLL RoleBll = new Sys_RoleBLL();
                entity = bll.Get(p => p.NodeConfigExID == id);
                if (entity.IsNotNull())
                {
                    txtWorkflowNodeConfig_Name.Text = entity.NodeConfigEx_Name;
                    txtWorkflowNodeConfig_Setp.Text = entity.NodeConfigEx_Setp == null ? "" : entity.NodeConfigEx_Setp.ToString();
                    drpWorkflowNodeConfig_AuditMode.SelectedValue = entity.NodeConfigEx_AuditMode;
                    ddlMechanism.SelectedValue = entity.NodeConfigEx_Mechanism.SafeToString("0");
                    hidAutoOption.Value = entity.NodeConfigEx_DistributionRule;
                    string objectId = hid_DepCode.Value = entity.NodeConfigEx_AuditObjectID;
                    ddlNodeSign.SelectedValue = entity.NodeConfigEx_NodeSign;
                    hid_NodeConfigEx_DataMappingID.Value = entity.NodeConfigEx_DataMappingID;
                    ckoNodeConfigEx_IsDisplayContract.Checked = entity.NodeConfigEx_IsDisplayContract.ToType(false);
                    if (!string.IsNullOrEmpty(objectId))
                    {
                        if (drpWorkflowNodeConfig_AuditMode.SelectedValue == "0")
                        {
                            Sys_Role role = RoleBll.Get(p => p.RoleID == objectId);
                            if (role.IsNotNull())
                            {
                                txt_Dep.Text = role.Role_Name;
                            }
                        }
                        else
                        {
                            Sys_UserInfo UserInfo = UserBll.Get(p => p.UserInfoID == objectId);
                            if (UserInfo.IsNotNull())
                            {
                                txt_Dep.Text = UserInfo.UserInfo_FullName;
                            }
                        }
                    }
                }
            }
        }

        protected void btn_Tijiao_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Workflow_NodeConfigEx entity = new Workflow_NodeConfigEx();
                entity = bll.Get(p => p.NodeConfigExID == id);
                entity.NodeConfigEx_Setp = txtWorkflowNodeConfig_Setp.Text.Trim().ToInt(0);
                entity.NodeConfigEx_Name = txtWorkflowNodeConfig_Name.Text.Trim();
                entity.NodeConfigEx_AuditMode = drpWorkflowNodeConfig_AuditMode.SelectedValue;
                entity.NodeConfigEx_AuditObjectID = hid_DepCode.Value;
                entity.NodeConfigEx_Mechanism = ddlMechanism.SelectedValue.ToInt(0);
                entity.NodeConfigEx_NodeSign = ddlNodeSign.SelectedValue;
                entity.NodeConfigEx_UpdateUserID = base.CurrUserInfo().UserID;
                entity.NodeConfigEx_UpdateTime = DateTime.Now;
                entity.NodeConfigEx_IsDisplayContract = ckoNodeConfigEx_IsDisplayContract.Checked;
                entity.NodeConfigEx_DataMappingID = hid_NodeConfigEx_DataMappingID.Value;
                if (entity.NodeConfigEx_Mechanism == 2)//自动分配规则
                {
                    entity.NodeConfigEx_DistributionRule = Request.Form["radioAutoOption"];
                }

                string msg = "";
                if (bll.Update(entity))
                {
                    MessageBox.ResponseScript(this, "window.location.href=document.URL;alert('保存成功！');window.parent.WorkFlowNodeTree();");
                }
                else
                {
                    MessageBox.Show(this, "保存错误");
                }
            }
            else
            {
                MessageBox.Show(this, "请选择步骤");
            }
        }
    }
}