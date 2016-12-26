using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Shu.Model;
using Shu.Comm;
using Shu.BLL;
using System.Text;
using Newtonsoft.Json;
using Shu.Utility.Extensions;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// WorkflowNodeExHandler 的摘要说明
    /// </summary>
    public class WorkflowNodeExHandler : IHttpHandler, IRequiresSessionState
    {

        SessionUserModel currentUser = null;
        Workflow_NodeConfigExBLL bll = new Workflow_NodeConfigExBLL();
        Workflow_NodeConfigEx entity = new Workflow_NodeConfigEx();
        List<Workflow_NodeConfigEx> entityList = new List<Workflow_NodeConfigEx>();
        Workflow_TasksExBLL bllTasks = new Workflow_TasksExBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            try
            {
                currentUser = context.Session["UserInfo"] as SessionUserModel;
            }
            catch
            {
                context.Response.Redirect("~/Login.aspx");
            }

            if (currentUser == null)
            {
                context.Response.Redirect("~/Login.aspx");
            }

            string active = context.Request.QueryString["active"];
            switch (active)
            {
                case "Tree":
                    this.getTreeData(context);
                    break;
                case "Add":
                    this.getAddData(context);
                    break;
                case "Update":
                    this.getUpdateData(context);
                    break;
                case "Delete":
                    this.getDeleteData(context);
                    break;
                case "up":
                    this.getUpData(context);
                    break;
                case "down":
                    this.getDownData(context);
                    break;
                case "sort":
                    this.getSortData(context);
                    break;
                //case "OpenHistoryList":
                //    this.OpenHistoryList(context);
                //    break;
            }

        }

        /// <summary>
        /// 获取阶段环节的审批记录
        /// </summary>
        /// <param name="context"></param>
        //private void OpenHistoryList(HttpContext context)
        //{
        //    string instanceID = context.Request.QueryString["instanceID"];
        //    string modelID = context.Request.QueryString["modelID"];
        //    string step = context.Request.QueryString["step"];
        //    if (instanceID != "" && modelID != "" && step != "")
        //    {
        //        int iStep = Convert.ToInt32(step);
        //        List<View_WorkflowNodeBusi> workflowBusiList = new View_WorkflowNodeBusiBLL().FindWhere(" WorkflowNodeBusi_TasksInstanceID='" + instanceID + "' and WorkflowNodeBusi_ModuleID='" + modelID + "' and WorkflowNodeBusi_Setp=" + iStep + "");
        //        string HtmlString = "<table width=\"550\" class=\"tab\" align=\"center\" border=\"0\" cellpadding=\"10\" cellspacing=\"1\">";
        //        HtmlString += "<tr>";
        //        HtmlString += "<th style=\"width:100\">";
        //        HtmlString += "经办人";
        //        HtmlString += "</th>";
        //        HtmlString += "<th style=\"width:200\">";
        //        HtmlString += "经办意见";
        //        HtmlString += "</th>";
        //        HtmlString += "<th style=\"width:100\">";
        //        HtmlString += "经办时间";
        //        HtmlString += "</th>";
        //        HtmlString += "<th style=\"width:100\">";
        //        HtmlString += "动作描述";
        //        HtmlString += "</th>";
        //        HtmlString += "</tr>";
        //        foreach (View_WorkflowNodeBusi flow in workflowBusiList)
        //        {
        //            if (!string.IsNullOrEmpty(flow.WorkflowNodeBusi_AuditUserID))
        //            {
        //                HtmlString += "<tr>";
        //                HtmlString += "<td style=\"word-break :break-all;word-wrap:break-word;text-align:center;\">" + flow.UserInfo_FullName + "</td>";
        //                HtmlString += "<td style=\"word-break :break-all;word-wrap:break-word;text-align:left;\">" + flow.WorkflowNodeBusi_AuditOpinion + "</td>";
        //                HtmlString += "<td style=\"text-align:center;\">" + flow.WorkflowNodeBusi_AuditTime + "</td>";
        //                HtmlString += "<td style=\"text-align:center;\">" + flow.WorkflowNodeBusi_Status + "</td>";
        //                HtmlString += "</tr>";
        //            }
        //        }
        //        HtmlString += " </table>";
        //        context.Response.Write(HtmlString);
        //    }
        //}

        private void getSortData(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            entityList = bll.GetList(p => p.NodeConfigEx_TasksInstanceID == id).OrderBy(p => p.NodeConfigEx_Setp).OrderBy
(p => p.NodeConfigEx_UpdateTime).ToList();
            int? sort = 1;
            if (entityList.Count > 0)
            {
                sort = entityList.Last().NodeConfigEx_Setp + 1;

            }
            context.Response.Write("{\"sort\":\"" + sort + "\"}");
        }

        private void getDownData(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            int? sort = context.Request.QueryString["sort"].ToInt(0);
            string parid = context.Request.QueryString["parid"];
            //entityList = bll.FindWhere(p => p.WorkflowNodeConfig_TasksInstanceID == id && p.WorkflowNodeConfig_Setp>sort).OrderBy(p => p.WorkflowNodeConfig_Setp).ToList();
            //if (entityList.Count > 0)
            //{
            //    sort = entityList.Last().WorkflowNodeConfig_Setp;
            //    entity = bll.Find(p => p.WorkflowNodeConfigID == parid);
            //    entity.WorkflowNodeConfig_Setp = sort;
            //    if(!bll.Update(entity,out msg))
            //    {
            //        context.Response.Write("{\"data\":\"1\"}");
            //    }
            //}
            entity = bll.Get(p => p.NodeConfigExID == parid);

            entity.NodeConfigEx_Setp = sort + 1;
            entity.NodeConfigEx_UpdateTime = DateTime.Now;
            if (bll.Update(entity))
            {
                context.Response.Write("{\"data\":\"1\"}");
            }
            else { context.Response.Write("{\"data\":\"0\"}"); }

        }

        private void getUpData(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            int? sort = context.Request.QueryString["sort"].ToInt(0);
            string parid = context.Request.QueryString["parid"];
            //entityList = bll.FindWhere(p => p.WorkflowNodeConfig_TasksInstanceID == id && p.WorkflowNodeConfig_Setp < sort).OrderBy(p => p.WorkflowNodeConfig_Setp).ToList();
            //if (entityList.Count > 0)
            //{
            //    sort = entityList.Last().WorkflowNodeConfig_Setp;
            //    entity = bll.Find(p => p.WorkflowNodeConfigID == parid);
            //    entity.WorkflowNodeConfig_Setp = sort;
            //    if (!bll.Update(entity, out msg))
            //    {
            //        context.Response.Write("{\"data\":\"1\"}");
            //    }
            //}
            entity = bll.Get(p => p.NodeConfigExID == parid);
            if (sort > 1)
            {
                entity.NodeConfigEx_Setp = sort - 1;
                entity.NodeConfigEx_UpdateTime = DateTime.Now;
                if (bll.Update(entity))
                {
                    context.Response.Write("{\"data\":\"1\"}");
                }
                else { context.Response.Write("{\"data\":\"0\"}"); }
            }
            else
            {
                context.Response.Write("{\"data\":\"2\"}");
            }
        }

        private void getDeleteData(HttpContext context)
        {
            string msg = string.Empty;
            string id = context.Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                if (bll.Delete(p => p.NodeConfigExID == id))
                {
                    context.Response.Write("{\"data\":\"删除成功\"}");
                }
                else
                {
                    context.Response.Write("{\"data\":\"" + msg + "\"}");
                }
            }
            else
            {
                context.Response.Write("{\"data\":\"删除出错\"}");
            }
        }

        private void getUpdateData(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Workflow_NodeConfigEx nodeConfigEx = new Workflow_NodeConfigEx();
                nodeConfigEx = new Workflow_NodeConfigExBLL().Get(p => p.NodeConfigExID == id);
                nodeConfigEx.NodeConfigEx_Name = context.Request.QueryString["WorkflowNodeConfig_Name"];
                nodeConfigEx.NodeConfigEx_Setp = context.Request.QueryString["WorkflowNodeConfig_Setp"].ToInt(0);
                nodeConfigEx.NodeConfigEx_UpdateTime = DateTime.Now;
                if (new Workflow_NodeConfigExBLL().Update(nodeConfigEx))
                {
                    var jsonperson = new { id = nodeConfigEx.NodeConfigEx_Setp.ToString(), text = nodeConfigEx.NodeConfigEx_Name, data = "更新成功" };
                    string jsonPerson = JsonConvert.SerializeObject(jsonperson);
                    context.Response.Write(jsonPerson);
                }
                else
                {
                    context.Response.Write("{\"data\":\"更新出错\"}");
                }
            }
            else
            {
                context.Response.Write("{\"data\":\"更新出错\"}");
            }
        }

        private void getAddData(HttpContext context)
        {
            Workflow_NodeConfigEx nodeConfigEx = new Workflow_NodeConfigEx();
            nodeConfigEx.NodeConfigEx_Name = context.Request.QueryString["WorkflowNodeConfig_Name"];
            nodeConfigEx.NodeConfigEx_Setp = context.Request.QueryString["WorkflowNodeConfig_Setp"].ToInt(0);
            nodeConfigEx.NodeConfigEx_TasksInstanceID = context.Request.QueryString["id"];
            nodeConfigEx.NodeConfigExID = Guid.NewGuid().ToString();
            nodeConfigEx.NodeConfigEx_UpdateTime = DateTime.Now;
            if (new Workflow_NodeConfigExBLL().Add(nodeConfigEx))
            {
                var jsonperson = new { id = nodeConfigEx.NodeConfigEx_Setp.ToString(), text = nodeConfigEx.NodeConfigEx_Name, data = "添加成功" };
                string jsonPerson = JsonConvert.SerializeObject(jsonperson);
                context.Response.Write(jsonPerson);
                //context.Response.Write("{\"data\":\"添加成功\"}");
            }
            else
            {
                context.Response.Write("{\"data\":\"添加错误\"}");
            }
        }

        private void getTreeData(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            string parentId = string.Empty;
            if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
            {
                parentId = context.Request.QueryString["id"];
            }

            List<Workflow_NodeConfigEx> nodeConfigExList = new List<Workflow_NodeConfigEx>();

            nodeConfigExList = new Workflow_NodeConfigExBLL().GetList(p => p.NodeConfigEx_TasksInstanceID == parentId).OrderBy(p => p.NodeConfigEx_Setp).ToList();
            Workflow_TasksEx entityTasks = new Workflow_TasksExBLL().Get(p => p.TasksEx_InstanceID == parentId);
            List<combotree> comtree = new List<combotree>();
            comtree.Add(new combotree { id = entityTasks.TasksEx_InstanceID, text = entityTasks.TasksEx_Name, state = "open", children = SetMenu(nodeConfigExList, entityTasks.TasksEx_InstanceID) });
            string jsonPerson = JsonConvert.SerializeObject(comtree);
            sb.Append(jsonPerson);
            context.Response.Write(sb.ToString());
        }
        private List<combotree> SetMenu(List<Workflow_NodeConfigEx> menuList, string Menu_Code)
        {
            StringBuilder strMenu = new StringBuilder();
            List<combotree> comtree = new List<combotree>();
            foreach (var tree in menuList)
            {
                comtree.Add(new combotree { id = tree.NodeConfigExID, text = tree.NodeConfigEx_Name, attributes = tree.NodeConfigEx_Setp.ToString(), children = null });
            }
            return comtree;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}