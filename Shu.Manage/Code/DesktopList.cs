using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Shu.BLL;
using Shu.Comm;
using Shu.Model;
using System.Data.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Text;

namespace Shu.Manage
{
    public class DesktopList
    {
        public BasePage basepage = new BasePage();
        /// <summary>
        /// 数据列表项
        /// </summary>
        public static string type = "[{\"id\":\"1\",\"text\":\"提醒情况\"},{\"id\":\"2\",\"text\":\"通知公告\"},{\"id\":\"3\",\"text\":\"待办事项\"}]";
        /// <summary>
        /// 设置默认数据项
        /// </summary>
        public static string defaultval = "1,2,3";
        public Sys_DesktopBLL dll = new Sys_DesktopBLL();
        public string BindList(string type, string id, string name)
        {
            string html = string.Empty;
            switch (type)
            {
                //case "1"://提醒情况
                //    html = LoadWarnPersonelInfo(id, name);
                //    break;
                //case "2"://通知公告
                //    html = message(id, name);
                //    break;
                case "3"://待办事项
                    html = GetPendingMatterInfo(id, name);
                    break;
                //case "4"://通知公告
                //    html = GetMessage(id, name);
                //    break;
                //case "5"://系统消息提醒
                //    html = MySysMessage(id, name);
                //    break;
            }
            return html;
        }


        /// <summary>
        /// 提醒情况
        /// </summary>
        public string LoadWarnPersonelInfo(string id, string name)
        {
            //Sys_Desktop desktopinfo = dll.Find(p => p.DesktopID == id);
            BasePage bg = new BasePage();
            string SQLWhere = "1=1";
            string SQLOverdue = "1=1";
            if (bg.IsLogin())
            {
                SQLWhere = SQLWhere + " and " + new RoleConfig().GetSQlWhere("RepaymenRemindList.aspx", bg.CurrUserInfo().RoleID, null);
                SQLOverdue = SQLOverdue + " and " + new RoleConfig().GetSQlWhere("OverdueRemindList.aspx", bg.CurrUserInfo().RoleID, null);
                SQLWhere = SQLWhere.Replace("{0}", bg.CurrUserInfo().UserID);
                SQLWhere = SQLWhere.Replace("{1}", bg.CurrUserInfo().DepartmentCode);

                SQLOverdue = SQLOverdue.Replace("{0}", bg.CurrUserInfo().UserID);
                SQLOverdue = SQLOverdue.Replace("{1}", bg.CurrUserInfo().DepartmentCode);
            }
            else
            {
                SQLWhere = SQLWhere + " and 1<>1";
                SQLOverdue = SQLOverdue + " and 1<>1";
            }

            //var RepaymenRemind = new Business_RepaymenRemindBLL().FindWhere(SQLWhere).Count;
            //var OverdueRemind = new Business_OverdueRemindBLL().FindWhere(SQLOverdue).Count;

            StringBuilder strbd = new StringBuilder();
            strbd.Append("<div id=\"" + id + "\" class=\"gridWrap\">");
            strbd.Append("<div class=\"hTabs\">");
            strbd.Append("<ul class=\"hTabsNav\">");
            //strbd.Append("<li class=\"selected\"><a href=\"javascript:void(0)\" style=\"color: #0060cc;\" onclick=\"showTabs('" + name + "','/Manage/Supervise/BaseProjectWarn.aspx?userid=" + basepage.CurrUserInfo().UserID + "')\">" + name + "</a></li>");
            strbd.Append("<li class=\"selected\"><a href=\"javascript:void(0)\" style=\"color: #0060cc;\">" + name + "</a></li>");
            strbd.Append("</ul>");
            strbd.Append("<a class=\"edite\" href=\"javascript:void(0);\" style=\"float: right; margin-right: 15px;margin-top: 3px; color: #115dc4; font-size: 12px; font-weight: bold; display: none;\" onclick=\"list_edite('" + id + "')\"><img src=\"../Images/buttons/Bianji.gif\" /></a>");
            strbd.Append("</div>");
            strbd.Append("<div class=\"tabsContent\"><div>");
            strbd.Append("<ul class=\"notceNews\">");

            //if (RepaymenRemind > 0)
            //{
            //    strbd.Append("<li>·<a href=javascript:showTabs('还款提醒记录','/Manage/Repayment/RepaymenRemindList.aspx');>还款提醒记录" + RepaymenRemind + "条</a></li>");
            //}
            //if (OverdueRemind > 0)
            //{
            //    strbd.Append("<li>·<a href=javascript:showTabs('逾期提醒记录','/Manage/Overduepayment/OverdueRemindList.aspx');>逾期提醒记录" + OverdueRemind + "条</a></li>");
            //}

            strbd.Append("</ul></div>");
            strbd.Append("</div></div>");
            return strbd.ToString();
        }

        /// <summary>
        /// 通知公告
        /// </summary>
        //public string message(string id, string name)
        //{
        //    View_Sys_NoticeBLL bllNotice = new View_Sys_NoticeBLL();
        //    List<View_Sys_Notice> noticelist = bllNotice.FindWhere(" charindex('" + basepage.CurrUserInfo().DepartmentCode + "',Notice_Scope)>0 ").OrderByDescending(p => p.Notice_AddTime).Skip(0).Take(12).ToList();
        //    //List<Sys_Notice> noticelist = bllNotice.FindWhere(p => p.Notice_IsShow == true).OrderBy(p => p.Notice_AddTime).Skip(0).Take(8).ToList();
        //    StringBuilder strbd = new StringBuilder();
        //    strbd.Append("<div id=\"" + id + "\" class=\"gridWrap\">");
        //    strbd.Append("<div class=\"hTabs\">");
        //    strbd.Append("<ul class=\"hTabsNav\">");
        //    strbd.Append("<li class=\"selected\"><a href=\"javascript:void(0)\" style=\"color: #0060cc;\" onclick=\"showTabs('" + name + "','/Manage/Sys/NoticeList.aspx')\">" + name + "</a></li>");
        //    strbd.Append("</ul>");
        //    strbd.Append("<a class=\"edite\" href=\"javascript:void(0);\" style=\"float: right; margin-right: 15px;margin-top: 3px; color: #115dc4; font-size: 12px; font-weight: bold; display: none;\" onclick=\"list_edite('" + id + "')\"><img src=\"../Images/buttons/Bianji.gif\" /></a>");
        //    strbd.Append("</div>");
        //    strbd.Append("<div class=\"tabsContent\"><div>");
        //    strbd.Append("<ul class=\"notceNews\">");
        //    foreach (View_Sys_Notice model in noticelist)
        //    {
        //        TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(model.Notice_AddTime).Ticks);
        //        TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(DateTime.Now).Ticks);
        //        TimeSpan ts = ts1.Subtract(ts2).Duration();
        //        string title = model.Notice_Title.ToString().Length > 25 ? model.Notice_Title.Substring(0, 25) + ".." : model.Notice_Title;
        //        if (ts.Days > 3)
        //        {
        //            strbd.Append("<li><a href=javascript:showTabs('详细','/Manage/Sys/NoticeShow.aspx?id=" + model.NoticeID + "');>[" + model.Notice_TypeName + "]" + title + "[" + Convert.ToDateTime(model.Notice_AddTime).ToString("yyyy-MM-dd") + "]</a></li>");
        //        }
        //        else
        //        {
        //            strbd.Append("<li><a href=javascript:showTabs('详细','/Manage/Sys/NoticeShow.aspx?id=" + model.NoticeID + "');>[" + model.Notice_TypeName + "]" + title + "[" + Convert.ToDateTime(model.Notice_AddTime).ToString("yyyy-MM-dd") + "]</a><font color=red>News</font></li>");
        //        }
        //    }
        //    strbd.Append("</ul></div>");
        //    strbd.Append("</div></div>");
        //    return strbd.ToString();
        //}

        /// <summary>
        /// 通知公告
        /// </summary>
        //public string GetMessage(string id, string name)
        //{
        //    View_Sys_NoticeBLL bllNotice = new View_Sys_NoticeBLL();
        //    List<View_Sys_Notice> noticelist = bllNotice.FindWhere(" charindex('" + basepage.CurrUserInfo().DepartmentCode + "',Notice_Scope)>0 ").OrderByDescending(p => p.Notice_AddTime).Skip(0).Take(8).ToList();
        //    StringBuilder strbd = new StringBuilder();
        //    strbd.Append("<div id=\"" + id + "\" class=\"gridWrap\">");
        //    strbd.Append("<div class=\"hTabs\">");
        //    strbd.Append("<ul class=\"hTabsNav\">");
        //    strbd.Append("<li class=\"selected\"><a href=\"javascript:void(0)\" style=\"color: #0060cc;\" onclick=\"parent.showTabs('" + name + "','/Manage/Sys/NoticeList.aspx')\">" + name + "</a></li>");
        //    strbd.Append("</ul>");
        //    strbd.Append("<a class=\"edite\" href=\"javascript:void(0);\" style=\"float: right; margin-right: 15px;margin-top: 3px; color: #115dc4; font-size: 12px; font-weight: bold; display: none;\" onclick=\"list_edite('" + id + "')\"><img src=\"../Images/buttons/Bianji.gif\" /></a>");
        //    strbd.Append("</div>");
        //    strbd.Append("<div class=\"tabsContent\"><div>");
        //    strbd.Append("<ul class=\"notceNews\">");
        //    foreach (View_Sys_Notice model in noticelist)
        //    {
        //        TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(model.Notice_AddTime).Ticks);
        //        TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(DateTime.Now).Ticks);
        //        TimeSpan ts = ts1.Subtract(ts2).Duration();
        //        string title = model.Notice_Title.ToString().Length > 200 ? model.Notice_Title.Substring(0, 200) + ".." : model.Notice_Title;
        //        if (ts.Days > 3)
        //        {
        //            strbd.Append("<li><a href=javascript:parent.showTabs('详细','/Manage/Sys/NoticeShow.aspx?id=" + model.NoticeID + "');>[" + model.Notice_TypeName + "]" + title + "[" + Convert.ToDateTime(model.Notice_AddTime).ToString("yyyy-MM-dd") + "]</a></li>");
        //        }
        //        else
        //        {
        //            strbd.Append("<li><a href=javascript:parent.showTabs('详细','/Manage/Sys/NoticeShow.aspx?id=" + model.NoticeID + "');>[" + model.Notice_TypeName + "]" + title + "[" + Convert.ToDateTime(model.Notice_AddTime).ToString("yyyy-MM-dd") + "]</a><font color=red>News</font></li>");
        //        }
        //    }
        //    strbd.Append("</ul></div>");
        //    strbd.Append("</div></div>");
        //    return strbd.ToString();
        //}

         /// <summary>
        /// 系统消息提醒
        /// </summary>
        //public string MySysMessage(string id, string name)
        //{
        //    View_Sys_MessageBLL MessageBLL = new View_Sys_MessageBLL();
        //    string currUserID = basepage.CurrUserInfo().UserID;
        //    List<View_Sys_Message> MessageList = MessageBLL.FindWhere(p => p.Message_ReceiveUserID == currUserID).OrderByDescending(p => p.Message_SendTime).Skip(0).Take(8).ToList();
        //    StringBuilder strbd = new StringBuilder();
        //    strbd.Append("<div id=\"" + id + "\" class=\"gridWrap\">");
        //    strbd.Append("<ul class=\"MyDesktopMessage\">");
        //    foreach (View_Sys_Message model in MessageList)
        //    {
        //        TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(model.Message_SendTime).Ticks);
        //        TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(DateTime.Now).Ticks);
        //        TimeSpan ts = ts1.Subtract(ts2).Duration();
        //        if (ts.Days > 3)
        //        {
        //            strbd.Append("<li id='" + model.MessageID + "'>" + model.Message_Content + " [" + Convert.ToDateTime(model.Message_SendTime).ToString("yyyy-MM-dd") + "] " + "<br /><hr style=\"height:1px;border:none;border-top:1px dashed #0066CC;\" /></li>");
        //        }
        //        else
        //        {
        //            strbd.Append("<li id='" + model.MessageID + "'>" + model.Message_Content + " [" + Convert.ToDateTime(model.Message_SendTime).ToString("yyyy-MM-dd") + "] " + "<font color=red>News</font><br /><hr style=\"height:1px;border:none;border-top:1px dashed #0066CC;\" /></li>");
        //        }
        //    }
        //    strbd.Append("</ul></div>");
        //    return strbd.ToString();
        //}
        


        /// <summary>
        /// 获取待办事项信息(取前8条)
        /// </summary>
        /// <returns></returns>
        public string GetPendingMatterInfo(string id, string name)
        {
            List<string> list = LoadPendingMatterDetailInfo().Skip(0).Take(11).ToList();

            StringBuilder strbd = new StringBuilder();
            strbd.Append("<div id=\"" + id + "\" class=\"gridWrapRight\">");
            strbd.Append("<div class=\"hTabs\">");
            strbd.Append("<ul class=\"hTabsNav\">");
            strbd.Append("<li class=\"selected\"><a href=\"javascript:void(0)\" style=\"color: #0060cc;\" onclick=\"showTabs('" + name + "','/Manage/Sys/SysPendingMatterList.aspx')\">" + name + "</a></li>");
            strbd.Append("</ul>");
            strbd.Append("<a class=\"edite\" href=\"javascript:void(0);\" style=\"float: right; margin-right: 15px;margin-top: 3px; color: #115dc4; font-size: 12px; font-weight: bold; display: none;\" onclick=\"list_edite('" + id + "')\"><img src=\"../Images/buttons/Bianji.gif\" /></a>");
            strbd.Append("</div>");
            strbd.Append("<div class=\"tabsContent\">");
            if (list.Count > 0)
            {
                strbd.Append("<table style=\"width:100%;\">");
                strbd.Append("<tr>");
                strbd.Append("<th width=40></th>");
                strbd.Append("<th width=120>任务状态</th>");
                strbd.Append("<th width=90>申请编号</th>");
                strbd.Append("<th width=90>客户姓名</th>");
                strbd.Append("<th width=90>车辆类型</th>");
                //strbd.Append("<th width=80>融资金额</th>");
                strbd.Append("<th width=140>申请日期</th>");
                strbd.Append("<th>操作</th>");
                strbd.Append("</tr>");
                foreach (string item in list)
                {
                    strbd.Append(item);
                }
                strbd.Append("<table>");
            }
            else
            {
                strbd.Append("<table>");
                strbd.Append("<tr>");
                strbd.Append("<th width=200 algin=center>暂无待办任务</th>");
                strbd.Append("<tr>");
                strbd.Append("<table>");
            }
            //strbd.Append("<ul class=\"notceNews\">");
            //foreach (string item in list)
            //{
            //    strbd.Append(item);
            //}
            //strbd.Append("</ul>");
            strbd.Append("</div></div>");
            return strbd.ToString();
        }


        /// <summary>
        /// 获取待办事项明细信息
        /// </summary>
        public List<string> LoadPendingMatterDetailInfo()
        {
            string SQLApplyBasis = string.Empty;
            //if (basepage.CurrUserInfo().RoleName.Contains("运营专员"))
            //{
            //    SQLApplyBasis = SQLWhere("ApplyList.aspx");
            //}

            SQLApplyBasis = SQLWhere("/ApplyList.aspx");

            //else if (basepage.CurrUserInfo().RoleName.Contains("初审专员"))
            //{ SQLApplyBasis = SQLWhere("AuditInstructionsList.aspx?step=1"); }
            //else if (basepage.CurrUserInfo().RoleName.Contains("信贷专员"))
            //{ SQLApplyBasis = SQLWhere("AuditInstructionsList.aspx?step=2"); }
            //else if (basepage.CurrUserInfo().RoleName.Contains("信贷主管"))
            //{ SQLApplyBasis = SQLWhere("AuditInstructionsList.aspx?step=3"); }
            //else if (basepage.CurrUserInfo().RoleName.Contains("贷前审核"))
            //{ SQLApplyBasis = SQLWhere("AuditInstructionsList.aspx?step=5"); }
            //else if (basepage.CurrUserInfo().RoleName.Contains("财务放款"))
            //{ SQLApplyBasis = SQLWhere("AuditInstructionsList.aspx?step=6"); }


            //string SQLApplyBasis = SQLWhere("ApplyList.aspx");
            //SQLApplyBasis += SQLWhere("AuditInstructionsList.aspx?step=1");
            //SQLApplyBasis += SQLWhere("AuditInstructionsList.aspx?step=2");
            //SQLApplyBasis += SQLWhere("AuditInstructionsList.aspx?step=3");
            //SQLApplyBasis += SQLWhere("AuditInstructionsList.aspx?step=4");
            //SQLApplyBasis += SQLWhere("AuditInstructionsList.aspx?step=5");
            //SQLApplyBasis += SQLWhere("AuditInstructionsList.aspx?step=6");
            SQLApplyBasis = SQLApplyBasis.Replace("'", "''");

            List<string> PendingMatterList = new List<string>();//待办事项集合
            string sqlString = "exec GetPendingMatterRecordDetail '" + basepage.CurrUserInfo().RoleID + "','" + basepage.CurrUserInfo().UserID + "','" + SQLApplyBasis + "'";
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlString);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string trStr = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            trStr = "";
                            trStr += "<tr>";
                            trStr += "<td align=center style=\"border-bottom:1pt dotted\"><img src=\"/Images/Desktop/audit.png\" /></td>";
                            trStr += "<td align=center style=\"border-bottom:1pt dotted\">" + ds.Tables[0].Rows[i]["Stauts"].ToString() + "</td>";
                            trStr += "<td align=center style=\"border-bottom:1pt dotted\">" + ds.Tables[0].Rows[i]["ApplyBasis_Code"].ToString() + "</td>";
                            trStr += "<td align=center style=\"border-bottom:1pt dotted\">" + ds.Tables[0].Rows[i]["ApplyBasis_Name"].ToString() + "</td>";
                            trStr += "<td align=center style=\"border-bottom:1pt dotted\">" + ds.Tables[0].Rows[i]["VehicleTypeName"].ToString() + "</td>";
                            //trStr += "<td align=center style=\"border-bottom:1pt dotted\">" + ds.Tables[0].Rows[i]["CustomProduct_Loan"].ToString() + "</td>";
                            trStr += "<td align=center style=\"border-bottom:1pt dotted\">" + ds.Tables[0].Rows[i]["ApplyBasis_Date"].ToString() + "</td>";
                            //trStr += "<td align=center style=\"border-bottom:1pt dotted\">" + GetAuditUrl(ds.Tables[0].Rows[i]["PendingMatter_ModuleID"].ToString(), ds.Tables[0].Rows[i]["PendingMatter_ModuleName"].ToString(), ds.Tables[0].Rows[i]["WorkflowTasksInstanceID"].ToString(), ds.Tables[0].Rows[i]["WorkflowTasksSetp"].ToString(), ds.Tables[0].Rows[i]["ApplyBasis_Type"].ToString(), ds.Tables[0].Rows[i]["Stauts"].ToString()) + "</td>";
                            trStr += "</tr>";
                            PendingMatterList.Add(trStr);
                        }
                    }
                }
            }
            return PendingMatterList;
        }


        /// <summary>
        /// 获取待办事项调转地址
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="moduleName"></param>
        /// <param name="instanceID"></param>
        /// <param name="workflowTasksSetp"></param>
        /// <returns></returns>
        //private string GetAuditUrl(string moduleID, string moduleName, string instanceID, string workflowTasksSetp, string applyBasisType, string taskStatus)
        //{
        //    string sRtn = string.Empty;
        //    string showType = basepage.CurrUserInfo().RoleShowType;
        //    if (moduleName == Constant.TaskCYGYWSP)//车易购业务审批
        //    {
        //        switch (workflowTasksSetp)
        //        {
        //            case "0":
        //                if (applyBasisType == "1")
        //                {
        //                    sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/ApplyAddPersonage.aspx?Desktop=true&id=" + moduleID + "&showType=" + showType + "');>办理</a>";
        //                }
        //                if (applyBasisType == "2")
        //                {
        //                    sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/ApplyAddCompany.aspx?Desktop=true&id=" + moduleID + "&showType=" + showType + "');>办理</a>";
        //                }
        //                break;
        //            case "1":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption1.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
        //                break;
        //            case "2":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption2.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
        //                break;
        //            case "3":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption3.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
        //                break;
        //            case "4":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption4.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
        //                break;
        //            case "5":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption5.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
        //                break;
        //            case "6":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption6.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
        //                break;
        //        }
        //    }
        //    if (moduleName == Constant.TaskCYGTQHK)//车易购业务提前还款
        //    {
        //        switch (workflowTasksSetp)
        //        {
        //            case "0":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Repayment/AdvancePayAdd.aspx?Desktop=true&id=" + moduleID + "&step=" + workflowTasksSetp + "');>办理</a>";
        //                break;
        //            case "1":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Repayment/AdvancePayApproveAudit.aspx?Desktop=true&id=" + moduleID + "&step=" + workflowTasksSetp + "');>办理</a>";
        //                break;
        //        }

        //    }

        //    string ExpressInfo_Status = string.Empty;
        //    string ApplyBasis_Code = string.Empty;
        //    string ApplyBasis_Name = string.Empty;
        //    string Department_Name = string.Empty;
        //    string ApplyBasis_VehicleType = string.Empty;
        //    string ApplyBasisID = string.Empty;
        //    if (moduleName == Constant.TaskCYGTQHK || moduleName == Constant.TaskCLIENTFILE || moduleName == Constant.TaskCLIENTFILESHPI)
        //    {
        //        Business_ExpressInfo ExpressInfo = new Business_ExpressInfoBLL().Find(p => p.ExpressInfoID == moduleID);
        //        if (ExpressInfo != null)
        //        {
        //            ExpressInfo_Status = ExpressInfo.ExpressInfo_Status;
        //            ApplyBasisID = ExpressInfo.ExpressInfo_ApplyBasisId;
        //            View_Business_ApplyBasis ApplyBasis = new View_Business_ApplyBasisBLL().Find(p => p.ApplyBasisId == ExpressInfo.ExpressInfo_ApplyBasisId);
        //            if (ApplyBasis != null)
        //            {
        //                ApplyBasis_Code = ApplyBasis.ApplyBasis_Code;
        //                ApplyBasis_Name = ApplyBasis.ApplyBasis_Name;
        //                Department_Name = ApplyBasis.Department_Name;
        //                ApplyBasis_VehicleType = ApplyBasis.ApplyBasis_VehicleType;
        //            }
        //        }

        //    }
        //    if (moduleName == Constant.TaskCLIENTFILE)//客户资料管理
        //    {
        //        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Archives/ExpressInfoAudit.aspx?Desktop=true&id=" + ApplyBasisID + "&ExpressInfoID=" + moduleID + "&ExpressInfo_Status=" + ExpressInfo_Status + "&ApplyBasis_Code=" + ApplyBasis_Code + "&ApplyBasis_Name=" + ApplyBasis_Name + "&Department_Name=" + Department_Name + "&ApplyBasis_VehicleType=" + ApplyBasis_VehicleType + "');>办理</a>";
        //    }
        //    if (moduleName == Constant.TaskCLIENTFILESHPI)//快递资料审批
        //    {
        //        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Archives/ExpressInfoEdit.aspx?Desktop=true&id=" + ApplyBasisID + "&ExpressInfoID=" + moduleID + "&ExpressInfo_Status=" + ExpressInfo_Status + "&ApplyBasis_Code=" + ApplyBasis_Code + "&ApplyBasis_Name=" + ApplyBasis_Name + "&Department_Name=" + Department_Name + "&ApplyBasis_VehicleType=" + ApplyBasis_VehicleType + "');>办理</a>";
        //    }
        //    if (moduleName == Constant.TaskCYGYWKHXXBG)//车易购业务客户信息变更
        //    {
        //        string ApplyBasis_Type = string.Empty;
        //        string CustomerInfoChange_ApplyBasisId = string.Empty;
        //        string CustomerInfoChange_StateCode = string.Empty;
        //        Business_CustomerInfoChange CustomerInfoChange = new Business_CustomerInfoChangeBLL().Find(p => p.CustomerInfoChange_ID == moduleID);
        //        CustomerInfoChange_ApplyBasisId = CustomerInfoChange.CustomerInfoChange_ApplyBasisId;
        //        CustomerInfoChange_StateCode = CustomerInfoChange.CustomerInfoChange_State;
        //        if (CustomerInfoChange != null)
        //        {
        //            View_Business_ApplyBasis ApplyBasis = new View_Business_ApplyBasisBLL().Find(p => p.ApplyBasisId == CustomerInfoChange_ApplyBasisId);
        //            if (ApplyBasis != null)
        //            {
        //                ApplyBasis_Type = ApplyBasis.ApplyBasis_Type.ToString();
        //            }
        //        }

        //        switch (workflowTasksSetp)
        //        {
        //            case "0":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/CustomerInfoChangeAdd.aspx?Desktop=true&id=" + moduleID + "&step=" + workflowTasksSetp + "&ApplyBasis_Type=" + ApplyBasis_Type + "&CustomerInfoChange_StateCode=" + CustomerInfoChange_StateCode + "');>办理</a>";
        //                break;
        //            case "1":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/CustomerInfoApprove.aspx?Desktop=true&id=" + moduleID + "&step=" + workflowTasksSetp + "&ApplyBasis_Type=" + ApplyBasis_Type + "&CustomerInfoChange_ApplyBasisId=" + CustomerInfoChange_ApplyBasisId + "');>办理</a>";
        //                break;
        //        }
        //    }
        //    if (moduleName == Constant.TaskCYGYWBDXB)//车易购业务保单续保
        //    {
        //        switch (workflowTasksSetp)
        //        {
        //            case "0":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Warranty/WarrantyRenewalApply.aspx?Desktop=true&BusinessID=RenewalApplyList,Modify&id=" + moduleID + "&step=" + workflowTasksSetp + "');>办理</a>";
        //                break;
        //            case "1":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Warranty/WarrantyRenewalApprove.aspx?Desktop=true&id=" + moduleID + "&step=" + workflowTasksSetp + "');>办理</a>";
        //                break;
        //        }
        //    }
        //    if (moduleName == Constant.TaskCYGYWBDLP)//车易购业务保单理赔
        //    {
        //        switch (workflowTasksSetp)
        //        {
        //            case "0":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Warranty/ClaimsApply.aspx?Desktop=true&BusinessID=CustomerClaimsList&id=" + moduleID + "&step=" + workflowTasksSetp + "');>办理</a>";
        //                break;
        //            case "1":
        //                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Warranty/ClaimsApprove.aspx?Desktop=true&id=" + moduleID + "&step=" + workflowTasksSetp + "');>办理</a>";
        //                break;
        //        }
        //    }
        //    return sRtn;
        //}



        /// <summary>
        /// 其他待办事项(PendingMatter_ToRoleName:多角色 userInfo.RoleName:多角色)
        /// </summary>
        public List<string> LoadPendingMatterInfo()
        {
            List<string> PendingMatterList = new List<string>();//待办事项集合
            string sqlString = "exec GetPendingMatterRecord '" + basepage.CurrUserInfo().RoleID + "','" + basepage.CurrUserInfo().UserID + "'";
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlString);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string title = ds.Tables[0].Rows[i]["PendingMatter_Title"].ToString();
                            string url = ds.Tables[0].Rows[i]["PendingMatter_URL"].ToString();
                            string iCount = ds.Tables[0].Rows[i]["iCount"].ToString();
                            title = string.Format(Constant.TesksAuditTitle, title, iCount);
                            PendingMatterList.Add("<li>·<a href=javascript:showTabs('待办事项','" + url + "');>" + title + "</a></li>");
                        }
                    }
                }
            }
            return PendingMatterList;
        }

        private string SQLWhere(string url)
        {
            BasePage bg = new BasePage();
            string SQLWhere = string.Empty;

            
            if (bg.IsLogin())
            {
                List<View_Sys_RolePurviewAndMenu> menuList = new List<View_Sys_RolePurviewAndMenu>();
                menuList = new Sys_RolePurviewBLL().GetRoleMenus(bg.CurrUserInfo().UserID);//加载用户菜单
                if (menuList.Exists(p => p.Menu_Url.Contains(url)))
                {
                    SQLWhere = SQLWhere + " and " + new RoleConfig().GetSQlWhere(url, bg.CurrUserInfo().RoleID, null);
                    SQLWhere = SQLWhere.Replace("{0}", bg.CurrUserInfo().UserID);
                    SQLWhere = SQLWhere.Replace("{1}", bg.CurrUserInfo().DepartmentCode);
                }
            }
            else
            {
                SQLWhere = SQLWhere + " and 1<>1";
            }
            return SQLWhere;
        }
    }
}