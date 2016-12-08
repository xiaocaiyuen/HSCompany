using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using YDT.BLL;
using System.Data;
using YDT.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace YDT.Web.Manage.Sys
{
    public partial class SysPendingMatterList : BasePage
    {

        public Common_BLL combll = new Common_BLL();
        public SessionUserModel userInfo = new SessionUserModel();
        public int MaxPageSize = 999;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userInfo = base.CurrUserInfo();
                Bind(1);
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="pageindex"></param>
        protected void Bind(int pageindex)
        {
            string SQLApplyBasis = string.Empty;
            //if (CurrUserInfo().RoleName.Contains("运营专员"))
            //{
            SQLApplyBasis = SQLWhere("/ApplyList.aspx");
            //}
            SQLApplyBasis = SQLApplyBasis.Replace("'", "''");

            string sqlString = "exec GetPendingMatterRecordDetail '" + userInfo.RoleID + "','" + userInfo.UserID + "','" + SQLApplyBasis + "'";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlString);
            DataSet Tz = db.ExecuteDataSet(dbCommand);
            this.hid_Count.Value = Tz.Tables[0].Rows.Count.ToString();
            string strContent = "";

            #region 待审核待办事项
            //for (int i = 0; i < Tz.Tables[0].Rows.Count; i++)
            //{
            //    string title = Tz.Tables[0].Rows[i]["PendingMatter_Title"].ToString();
            //    string url = Tz.Tables[0].Rows[i]["PendingMatter_URL"].ToString();
            //    string iCount = Tz.Tables[0].Rows[i]["iCount"].ToString();
            //    string colStyle =string.Empty;
            //    if (i % 2 == 0)
            //    {
            //        colStyle = "scope=\"col\"";
            //    }

            //    if (title.Contains("士官选取") || title.Contains("干部晋升"))
            //    {
            //    }
            //    else
            //    {
            //        title = string.Format(Constant.TesksAuditTitle, title, iCount);
            //    }
            //    strContent += "<tr style=\"border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #000000;\">";
            //    strContent += "<th " + colStyle + " style=\" text-align:left;\" >&#8226;&nbsp;&nbsp;" + title + "</td>";
            //    strContent += "<th " + colStyle + "  style=\" text-align:center;font-size: 10px;\" ><a href='#' onclick=\"javascript:window.parent.addTab('待办列表','" + url + "')\"><span>办理</span></a></td>";
            //    strContent += "</tr>";
            //}
            #endregion


            #region 待审核待办事项
            for (int i = 0; i < Tz.Tables[0].Rows.Count; i++)
            {
                strContent += "<tr>";
                strContent += "<td align=center style=\"border-bottom:1pt dotted\"><img src=\"/Images/Desktop/audit.png\" /></td>";
                strContent += "<td align=center style=\"border-bottom:1pt dotted\">" + Tz.Tables[0].Rows[i]["Stauts"].ToString() + "</td>";
                strContent += "<td align=center style=\"border-bottom:1pt dotted\">" + Tz.Tables[0].Rows[i]["ApplyBasis_Code"].ToString() + "</td>";
                strContent += "<td align=center style=\"border-bottom:1pt dotted\">" + Tz.Tables[0].Rows[i]["ApplyBasis_Name"].ToString() + "</td>";
                strContent += "<td align=center style=\"border-bottom:1pt dotted\">" + Tz.Tables[0].Rows[i]["VehicleTypeName"].ToString() + "</td>";
                //strContent += "<td align=center style=\"border-bottom:1pt dotted\">" + Tz.Tables[0].Rows[i]["CustomProduct_Loan"].ToString() + "</td>";
                strContent += "<td align=center style=\"border-bottom:1pt dotted\">" + Tz.Tables[0].Rows[i]["ApplyBasis_Date"].ToString() + "</td>";
                strContent += "<td align=center style=\"border-bottom:1pt dotted\">" + GetAuditUrl(Tz.Tables[0].Rows[i]["PendingMatter_ModuleID"].ToString(), Tz.Tables[0].Rows[i]["PendingMatter_ModuleName"].ToString(), Tz.Tables[0].Rows[i]["WorkflowTasksInstanceID"].ToString(), Tz.Tables[0].Rows[i]["WorkflowTasksSetp"].ToString(), Tz.Tables[0].Rows[i]["ApplyBasis_Type"].ToString(), Tz.Tables[0].Rows[i]["Stauts"].ToString()) + "</td>";
                strContent += "</tr>";
            }
            #endregion

            this.LitLCT.Text = strContent;
        }


        /// <summary>
        /// 获取待办事项调转地址
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="moduleName"></param>
        /// <param name="instanceID"></param>
        /// <param name="workflowTasksSetp"></param>
        /// <returns></returns>
        private string GetAuditUrl(string moduleID, string moduleName, string instanceID, string workflowTasksSetp, string applyBasisType, string taskStatus)
        {
            string sRtn = string.Empty;
            string showType = base.CurrUserInfo().RoleShowType;
            if (moduleName == Constant.TaskCYGYWSP)//车易购业务审批
            {
                switch (workflowTasksSetp)
                {
                    case "0":
                        if (applyBasisType == "1")
                        {
                            sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/ApplyAddPersonage.aspx?Desktop=true&id=" + moduleID + "&showType=" + showType + "');>办理</a>";
                        }
                        if (applyBasisType == "2")
                        {
                            sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/ApplyAddCompany.aspx?Desktop=true&id=" + moduleID + "&showType=" + showType + "');>办理</a>";
                        }
                        break;
                    case "1":
                        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption1.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
                        break;
                    case "2":
                        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption2.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
                        break;
                    case "3":
                        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption3.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
                        break;
                    case "4":
                        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption4.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
                        break;
                    case "5":
                        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption5.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
                        break;
                    case "6":
                        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Business/InstructionsAuditOption6.aspx?Desktop=true&id=" + moduleID + "&ApplyBasis_Type=" + applyBasisType + "&step=" + workflowTasksSetp + "&showType=" + showType + "');>办理</a>";
                        break;
                }
            }
            if (moduleName == Constant.TaskCYGTQHK)//车易购业务提前还款
            {
                switch (workflowTasksSetp)
                {
                    case "0":
                        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Repayment/AdvancePayAdd.aspx?Desktop=true&id=" + moduleID + "&step=" + workflowTasksSetp + "');>办理</a>";
                        break;
                    case "1":
                        sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Repayment/AdvancePayApproveAudit.aspx?Desktop=true&id=" + moduleID + "&step=" + workflowTasksSetp + "');>办理</a>";
                        break;
                }
            }

            string ExpressInfo_Status = string.Empty;
            string ApplyBasis_Code = string.Empty;
            string ApplyBasis_Name = string.Empty;
            string Department_Name = string.Empty;
            string ApplyBasis_VehicleType = string.Empty;
            string ApplyBasisID = string.Empty;
            if (moduleName == Constant.TaskCYGTQHK || moduleName == Constant.TaskCLIENTFILE || moduleName == Constant.TaskCLIENTFILESHPI)
            {
                Business_ExpressInfo ExpressInfo = new Business_ExpressInfoBLL().Find(p => p.ExpressInfoID == moduleID);
                if (ExpressInfo != null)
                {
                    ExpressInfo_Status = ExpressInfo.ExpressInfo_Status;
                    ApplyBasisID = ExpressInfo.ExpressInfo_ApplyBasisId;
                    View_Business_ApplyBasis ApplyBasis = new View_Business_ApplyBasisBLL().Find(p => p.ApplyBasisId == ExpressInfo.ExpressInfo_ApplyBasisId);
                    if (ApplyBasis != null)
                    {
                        ApplyBasis_Code = ApplyBasis.ApplyBasis_Code;
                        ApplyBasis_Name = ApplyBasis.ApplyBasis_Name;
                        Department_Name = ApplyBasis.Department_Name;
                        ApplyBasis_VehicleType = ApplyBasis.ApplyBasis_VehicleType;
                    }
                }
            }
            if (moduleName == Constant.TaskCLIENTFILE)//客户资料管理
            {
                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Archives/ExpressInfoAudit.aspx?Desktop=true&id=" + ApplyBasisID + "&ExpressInfoID=" + moduleID + "&ExpressInfo_Status=" + ExpressInfo_Status + "&ApplyBasis_Code=" + ApplyBasis_Code + "&ApplyBasis_Name=" + ApplyBasis_Name + "&Department_Name=" + Department_Name + "&ApplyBasis_VehicleType=" + ApplyBasis_VehicleType + "');>办理</a>";
            }
            if (moduleName == Constant.TaskCLIENTFILESHPI)//快递资料审批
            {
                sRtn = "<a href=javascript:TaskShowClick('" + taskStatus + "','/Manage/Archives/ExpressInfoEdit.aspx?Desktop=true&id=" + ApplyBasisID + "&ExpressInfoID=" + moduleID + "&ExpressInfo_Status=" + ExpressInfo_Status + "&ApplyBasis_Code=" + ApplyBasis_Code + "&ApplyBasis_Name=" + ApplyBasis_Name + "&Department_Name=" + Department_Name + "&ApplyBasis_VehicleType=" + ApplyBasis_VehicleType + "');>办理</a>";
            }
            return sRtn;
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