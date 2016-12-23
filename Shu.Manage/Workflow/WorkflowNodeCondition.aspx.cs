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
    public partial class WorkflowNodeCondition : BasePage
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

            this.ddlNextWfConfigNodeID.DataSource = nodeConfigExList;
            this.ddlNextWfConfigNodeID.DataTextField = "NodeConfigEx_Name";
            this.ddlNextWfConfigNodeID.DataValueField = "NodeConfigExID";
            this.ddlNextWfConfigNodeID.DataBind();
            
        }

        private void Show()
        {
            string id = Request["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Workflow_NodeCondition entity = new Workflow_NodeCondition();
                entity = new Workflow_NodeConditionBLL().Get(p => p.NodeConditionID == id);
                if(entity.IsNotNull())
                {
                    List<Workflow_NodeConfigEx> nodeConfigExList = new Workflow_NodeConfigExBLL().GetList(p => p.NodeConfigExID == entity.NodeCondition_WfConfigNodeID).ToList();
                    if(nodeConfigExList.Count>0)
                    {
                        ddlworkflowTasksEx.SelectedValue = nodeConfigExList[0].NodeConfigEx_TasksInstanceID;
                        BingWfConfigNodeID();
                        ddlWfConfigNodeID.SelectedValue = entity.NodeCondition_WfConfigNodeID;
                        this.ddlNextWfConfigNodeID.SelectedValue = entity.NodeCondition_NextWfConfigNodeID;
                    }

                    txtDescription.Text = entity.NodeCondition_Description;
                    txtContent.Text = entity.NodeCondition_Content;
                    ddlParameterType.SelectedValue = entity.NodeCondition_ParameterType;
                    txtParameterStart.Text = entity.NodeCondition_ParameterStart.ToString();
                    txtParameterEnd.Text = entity.NodeCondition_ParameterEnd.ToString();
                    txtFixedParameter.Text = entity.NodeCondition_FixedParameter;
                    ddlFixedParameterType.SelectedValue = entity.NodeCondition_FixedParameterType;
                    txtNextWfOptionURL.Text = entity.NodeCondition_NextWfOptionURL;
                }
            }
        }


        protected void btn_Tijiao_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request["id"];
            Workflow_NodeCondition entity = new Workflow_NodeCondition();
            entity = new Workflow_NodeConditionBLL().Get(p => p.NodeConditionID == id);
            if (entity.IsNull())
            {
                entity = new Workflow_NodeCondition();
                entity.NodeConditionID = Guid.NewGuid().ToString();
            }
            entity.NodeCondition_WfConfigNodeID = ddlWfConfigNodeID.SelectedValue;
            entity.NodeCondition_NextWfConfigNodeID = ddlNextWfConfigNodeID.SelectedValue;
            entity.NodeCondition_Description = txtDescription.Text;
            entity.NodeCondition_Content = txtContent.Text;
            entity.NodeCondition_ParameterType = ddlParameterType.SelectedValue;
            entity.NodeCondition_UpdateTime = DateTime.Now;
            entity.NodeCondition_UpdateUserID = base.CurrUserInfo().UserID;
            entity.NodeCondition_NextWfOptionURL = txtNextWfOptionURL.Text;
            entity.NodeCondition_IsDeleted = false;
            if (txtParameterStart.Text != "")
            {
                entity.NodeCondition_ParameterStart = Convert.ToDouble(txtParameterStart.Text);
            }
            if (txtParameterEnd.Text != "")
            {
                entity.NodeCondition_ParameterEnd = Convert.ToDouble(txtParameterEnd.Text);
            }

            entity.NodeCondition_FixedParameter = txtFixedParameter.Text;
            entity.NodeCondition_FixedParameterType = ddlFixedParameterType.SelectedValue;
            string msg = "";
            bool rtn = true;
            if (string.IsNullOrEmpty(id))rtn= new Workflow_NodeConditionBLL().Add(entity);
            else rtn= new Workflow_NodeConditionBLL().Update(entity);

            if (rtn)
            {
                MessageBox.ShowAndRedirect(this, "保存成功！", "WorkflowNodeConditionList.aspx");
            }
            else
            {
                MessageBox.Show(this, "保存失败");
            }
        }
    }
}