using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.BLL;
using Shu.Comm;
using Shu.Model;
using Shu.Utility;
using Shu.Utility.Extensions;

namespace Shu.Manage.Workflow
{
    public partial class WorkflowTasksInfoEx : BasePage
    {
        private string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = WebUtil.GetQuery("id");
            hid_id.Value = id;
            if (!IsPostBack)
            {
                BindDDL();
                Show();
            }
        }

        private void Show()
        {
            if (!string.IsNullOrEmpty(id))
            {
                View_Workflow_TasksEx entity = new View_Workflow_TasksEx();
                View_Workflow_TasksExBLL bll = new View_Workflow_TasksExBLL();
                entity = bll.Get(p => p.TasksEx_InstanceID == id);
                if (entity != null)
                {
                    txtWorkflowTasks_InstanceNo.Text = entity.TasksEx_InstanceNo;
                    txtWorkflowTasks_InstanceVerNo.Text = entity.TasksEx_InstanceVerNo;
                    txtWorkflowTasks_Name.Text = entity.TasksEx_Name;
                    drp_WorkflowTasks_IsValid.SelectedValue = entity.TasksEx_IsValid.ToString();
                    drp_WorkflowTasks_Type.SelectedValue = entity.TasksEx_Type;
                }
                else
                {
                    MessageBox.Show(this, "没有此数据！");
                }
            }
        }

        public void BindDDL()
        {
            Sys_DataDictBLL balDataDict = new Sys_DataDictBLL();
            List<Sys_DataDict> listDataDict = balDataDict.GetList(p => p.DataDict_ParentCode == "06").OrderBy(p => p.DataDict_Sequence).ToList();
            this.drp_WorkflowTasks_Type.DataSource = listDataDict;
            this.drp_WorkflowTasks_Type.DataTextField = "DataDict_Name";
            this.drp_WorkflowTasks_Type.DataValueField = "DataDict_Code";
            this.drp_WorkflowTasks_Type.DataBind();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 操作
        /// </summary>
        private void Save()
        {
            Workflow_TasksExBLL bll = new Workflow_TasksExBLL();
            Workflow_TasksEx entity = new Workflow_TasksEx();
            string msg = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                entity.TasksEx_AddUserID = CurrUserInfo().UserID;
                entity.TasksEx_InstanceNo = txtWorkflowTasks_InstanceNo.Text.Trim();
                entity.TasksEx_InstanceVerNo = txtWorkflowTasks_InstanceVerNo.Text.Trim();
                entity.TasksEx_IsValid = Convert.ToBoolean(drp_WorkflowTasks_IsValid.SelectedValue);
                entity.TasksEx_Name = txtWorkflowTasks_Name.Text.Trim();
                entity.TasksEx_InstanceID = Guid.NewGuid().ToString();
                entity.TasksEx_AddTime = DateTime.Now;
                entity.TasksEx_Type = drp_WorkflowTasks_Type.SelectedValue;
                if (bll.Add(entity))
                {
                    MessageBox.ResponseScript(this, "alert('保存成功！');parent.location.reload();");
                }
                else { MessageBox.Show(this, msg); }
            }
            else
            {
                entity = bll.Get(p => p.TasksEx_InstanceID == id);
                entity.TasksEx_InstanceNo = txtWorkflowTasks_InstanceNo.Text.Trim();
                entity.TasksEx_InstanceVerNo = txtWorkflowTasks_InstanceVerNo.Text.Trim();
                entity.TasksEx_IsValid = Convert.ToBoolean(drp_WorkflowTasks_IsValid.SelectedValue);
                entity.TasksEx_Name = txtWorkflowTasks_Name.Text.Trim();
                entity.TasksEx_Type = drp_WorkflowTasks_Type.SelectedValue;
                if (bll.Update(entity))
                {
                    MessageBox.ResponseScript(this, "alert('保存成功');parent.location.reload();");
                }
                else
                {
                    MessageBox.Show(this, msg);
                }
            }
        }
    }
}