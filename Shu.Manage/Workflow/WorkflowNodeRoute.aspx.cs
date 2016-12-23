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
    public partial class WorkflowNodeRoute : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BingDDL();
                Show();
            }
        }

        private void BingDDL()
        {
            Workflow_TasksExBLL workflowTasksExBLL = new Workflow_TasksExBLL();
            List<Workflow_TasksEx> tasksExList = workflowTasksExBLL.GetAll().ToList();
            this.ddlworkflowTasksEx.DataSource = tasksExList;
            this.ddlworkflowTasksEx.DataTextField = "TasksEx_Name";
            this.ddlworkflowTasksEx.DataValueField = "TasksEx_InstanceID";
            this.ddlworkflowTasksEx.DataBind();

            this.ddlworkflowTasksEx.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });


            Workflow_AuditActionDefinitionBLL auditActionDefinitionBLL = new Workflow_AuditActionDefinitionBLL();
            List<Workflow_AuditActionDefinition> auditActionDefinitionList = auditActionDefinitionBLL.GetAll().ToList();
            this.ddlAuditActionDefinition.DataSource = auditActionDefinitionList;
            this.ddlAuditActionDefinition.DataTextField = "AuditActionDefinition_Name";
            this.ddlAuditActionDefinition.DataValueField = "AuditActionDefinitionID";
            this.ddlAuditActionDefinition.DataBind();

            BingWfConfigNodeID();
        }

        protected void ddlworkflowTasksEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            BingWfConfigNodeID();
        }

        private void BingWfConfigNodeID()
        {
            Workflow_NodeConfigExBLL workflowNodeConfigExBLL = new Workflow_NodeConfigExBLL();
            List<Workflow_NodeConfigEx> nodeConfigExList = workflowNodeConfigExBLL.GetList(p => p.NodeConfigEx_TasksInstanceID == this.ddlworkflowTasksEx.SelectedValue).ToList();
            this.ddlWfConfigNodeID.DataSource = nodeConfigExList;
            this.ddlWfConfigNodeID.DataTextField = "NodeConfigEx_Name";
            this.ddlWfConfigNodeID.DataValueField = "NodeConfigExID";
            this.ddlWfConfigNodeID.DataBind();

            this.ddlNextStep.DataSource = nodeConfigExList;
            this.ddlNextStep.DataTextField = "NodeConfigEx_Name";
            this.ddlNextStep.DataValueField = "NodeConfigExID";
            this.ddlNextStep.DataBind();

        }

        private void Show()
        {
            string id = Request["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Workflow_NodeRoute entity = new Workflow_NodeRoute();
                entity = new Workflow_NodeRouteBLL().Get(p => p.NodeRouteID == id);
                if (entity.IsNotNull())
                {
                    List<Workflow_NodeConfigEx> nodeConfigExList = new Workflow_NodeConfigExBLL().GetList(p => p.NodeConfigExID == entity.NodeRoute_WfConfigNodeID).ToList();
                    if (nodeConfigExList.Count > 0)
                    {
                        ddlworkflowTasksEx.SelectedValue = nodeConfigExList[0].NodeConfigEx_TasksInstanceID;
                        BingWfConfigNodeID();
                        ddlWfConfigNodeID.SelectedValue = entity.NodeRoute_WfConfigNodeID;
                        this.ddlNextStep.SelectedValue = entity.NodeRoute_NextWfConfigNodeID;
                    }

                    ddlAuditActionDefinition.SelectedValue = entity.NodeRoute_WfAuditActionDefinitionID;
                    ddlNextStep.SelectedValue = entity.NodeRoute_NextWfConfigNodeID;
                    ddlIfCondJump.SelectedValue = entity.NodeRoute_IfCondJump.ToString();
                    txtNextWfOptionURL.Text = entity.NodeRoute_NextWfOptionURL;
                    if (entity.NodeRoute_NextWfOptionUserAttribute != null)
                    {
                        ddlNextWfOptionUserAttribute.SelectedValue = entity.NodeRoute_NextWfOptionUserAttribute.ToString();
                    }
                }
            }
        }


        protected void btn_Tijiao_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request["id"];

            Workflow_NodeRoute entity = new Workflow_NodeRoute();
            entity = new Workflow_NodeRouteBLL().Get(p => p.NodeRouteID == id);

            if (entity.IsNull())
            {
                entity = new Workflow_NodeRoute();
                entity.NodeRouteID = Guid.NewGuid().ToString();
            }
            entity.NodeRoute_IfCondJump = Convert.ToBoolean(ddlIfCondJump.SelectedValue);
            entity.NodeRoute_IsDeleted = false;
            entity.NodeRoute_NextWfConfigNodeID = ddlNextStep.SelectedValue;
            entity.NodeRoute_NextWfOptionURL = txtNextWfOptionURL.Text;
            entity.NodeRoute_WfAuditActionDefinitionID = ddlAuditActionDefinition.SelectedValue;
            entity.NodeRoute_WfConfigNodeID = ddlWfConfigNodeID.SelectedValue;
            entity.NodeRoute_NextWfOptionUserAttribute = Convert.ToInt32(ddlNextWfOptionUserAttribute.SelectedValue);
            entity.NodeRoute_UpdateTime = DateTime.Now;
            entity.NodeRoute_UpdateUserID = base.CurrUserInfo().UserID;
            bool rtn = false;
            if (string.IsNullOrEmpty(id))
            {
                rtn=new Workflow_NodeRouteBLL().Add(entity);
            }
            else
            {
                rtn = new Workflow_NodeRouteBLL().Update(entity);
            }

            if (rtn)
            {
                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.ResponseScript(this, "window.location.href=document.URL;alert('保存成功！');window.parent.WorkFlowNodeTree();");
                }
                else {
                    MessageBox.Show(this, "保存成功");
                }
                //MessageBox.ShowAndRedirect(this, "保存成功！", "WorkflowNodeRouteList.aspx");
            }
            else
            {
                MessageBox.Show(this, "保存失败");
            }
        }
    }
}