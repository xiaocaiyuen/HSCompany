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
    public class WorkflowNodeRouteHandler : IHttpHandler, IRequiresSessionState
    {

        SessionUserModel currentUser = null;
        //Sys_WorkflowNodeConfigBLL bll = new Sys_WorkflowNodeConfigBLL();
        //Sys_WorkflowNodeConfig entity = new Sys_WorkflowNodeConfig();
        //List<Sys_WorkflowNodeConfig> entityList = new List<Sys_WorkflowNodeConfig>();
        //Sys_WorkflowTasksBLL bllTasks = new Sys_WorkflowTasksBLL();
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
                //case "up":
                //    this.getUpData(context);
                //    break;
                //case "down":
                //    this.getDownData(context);
                //    break;
                //case "sort":
                //    this.getSortData(context);
                //    break;
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

//        private void getSortData(HttpContext context)
//        {
//            string id = context.Request.QueryString["id"];
//            entityList = bll.FindWhere(p => p.WorkflowNodeConfig_TasksInstanceID == id).OrderBy(p => p.WorkflowNodeConfig_Setp).OrderBy
//(p => p.WorkflowNodeConfig_UpdateTime).ToList();
//            int? sort = 1;
//            if (entityList.Count > 0)
//            {
//                sort = entityList.Last().WorkflowNodeConfig_Setp + 1;

//            }
//            context.Response.Write("{\"sort\":\"" + sort + "\"}");
//        }

//        private void getDownData(HttpContext context)
//        {
//            string id = context.Request.QueryString["id"];
//            int? sort = context.Request.QueryString["sort"].ToInt(0);
//            string parid = context.Request.QueryString["parid"];
//            string msg = string.Empty;
//            //entityList = bll.FindWhere(p => p.WorkflowNodeConfig_TasksInstanceID == id && p.WorkflowNodeConfig_Setp>sort).OrderBy(p => p.WorkflowNodeConfig_Setp).ToList();
//            //if (entityList.Count > 0)
//            //{
//            //    sort = entityList.Last().WorkflowNodeConfig_Setp;
//            //    entity = bll.Find(p => p.WorkflowNodeConfigID == parid);
//            //    entity.WorkflowNodeConfig_Setp = sort;
//            //    if(!bll.Update(entity,out msg))
//            //    {
//            //        context.Response.Write("{\"data\":\"1\"}");
//            //    }
//            //}
//            entity = bll.Find(p => p.WorkflowNodeConfigID == parid);

//            entity.WorkflowNodeConfig_Setp = sort + 1;
//            entity.WorkflowNodeConfig_UpdateTime = DateTime.Now;
//            if (bll.Update(entity, out msg))
//            {
//                context.Response.Write("{\"data\":\"1\"}");
//            }
//            else { context.Response.Write("{\"data\":\"0\"}"); }

//        }

//        private void getUpData(HttpContext context)
//        {
//            string id = context.Request.QueryString["id"];
//            int? sort = context.Request.QueryString["sort"].ToInt(0);
//            string parid = context.Request.QueryString["parid"];
//            string msg = string.Empty;
//            //entityList = bll.FindWhere(p => p.WorkflowNodeConfig_TasksInstanceID == id && p.WorkflowNodeConfig_Setp < sort).OrderBy(p => p.WorkflowNodeConfig_Setp).ToList();
//            //if (entityList.Count > 0)
//            //{
//            //    sort = entityList.Last().WorkflowNodeConfig_Setp;
//            //    entity = bll.Find(p => p.WorkflowNodeConfigID == parid);
//            //    entity.WorkflowNodeConfig_Setp = sort;
//            //    if (!bll.Update(entity, out msg))
//            //    {
//            //        context.Response.Write("{\"data\":\"1\"}");
//            //    }
//            //}
//            entity = bll.Find(p => p.WorkflowNodeConfigID == parid);
//            if (sort > 1)
//            {
//                entity.WorkflowNodeConfig_Setp = sort - 1;
//                entity.WorkflowNodeConfig_UpdateTime = DateTime.Now;
//                if (bll.Update(entity, out msg))
//                {
//                    context.Response.Write("{\"data\":\"1\"}");
//                }
//                else { context.Response.Write("{\"data\":\"0\"}"); }
//            }
//            else
//            {
//                context.Response.Write("{\"data\":\"2\"}");
//            }
//        }

        private void getDeleteData(HttpContext context)
        {
            string msg = string.Empty;
            string id = context.Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                if (new Workflow_NodeRouteBLL().Delete(p => p.NodeRouteID == id))
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
            string msg = string.Empty;
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
                context.Response.Write("{\"data\":\"添加出错\"}");
            }
        }

        private void getTreeData(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();

            List<Workflow_NodeConfigEx> nodeConfigExList = new List<Workflow_NodeConfigEx>();

            List<View_Workflow_NodeRoute> NodeRouteList = new View_Workflow_NodeRouteBLL().GetAll().ToList();

            nodeConfigExList = new Workflow_NodeConfigExBLL().GetAll().OrderBy(p => p.NodeConfigEx_Setp).ToList();
            List<Workflow_TasksEx> entityTasksList = new Workflow_TasksExBLL().GetAll().ToList();
            List<combotree> comtree = new List<combotree>();
            foreach (var tree in entityTasksList)
            {
                comtree.Add(new combotree { id = tree.TasksEx_InstanceID, text = tree.TasksEx_Name, state = "open", children = SetMenu(nodeConfigExList, tree.TasksEx_InstanceID, NodeRouteList) });
            }
            string jsonPerson = JsonConvert.SerializeObject(comtree);
            sb.Append(jsonPerson);
            context.Response.Write(sb.ToString());
        }
        private List<combotree> SetMenu(List<Workflow_NodeConfigEx> menuList, string Menu_Code, List<View_Workflow_NodeRoute> NodeRouteList)
        {
            List<combotree> comtree = new List<combotree>();
            List<Workflow_NodeConfigEx> NodeConfigExList = menuList.Where(p=>p.NodeConfigEx_TasksInstanceID== Menu_Code).ToList();
            foreach (var tree in NodeConfigExList)
            {
                comtree.Add(new combotree { id = tree.NodeConfigExID, text = tree.NodeConfigEx_Name, attributes = tree.NodeConfigEx_Setp.ToString(), children = SetNodeRoute(NodeRouteList, tree.NodeConfigExID) });
            }
            return comtree;

        }

        private List<combotree> SetNodeRoute(List<View_Workflow_NodeRoute> menuList, string Menu_Code)
        {
            List<combotree> comtree = new List<combotree>();
            List<View_Workflow_NodeRoute> NodeRouteList = menuList.Where(p=>p.NodeRoute_WfConfigNodeID== Menu_Code).ToList();
            foreach (var tree in NodeRouteList)
            {
                comtree.Add(new combotree { id = tree.NodeRouteID, text = tree.AuditActionDefinition_Name, attributes = tree.NodeConfigEx_Setp.ToString(), children = null });
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