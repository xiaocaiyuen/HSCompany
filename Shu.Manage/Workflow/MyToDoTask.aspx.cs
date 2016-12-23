using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;

namespace Shu.Manage.Workflow
{
    public partial class MyToDoTask : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindGrid();
            }
        }

        /// <summary>
        /// 列表绑定
        /// </summary>
        public void BindGrid()
        {
            if (base.RequstStr("type") == "TQHK")
            {
                //提前还款
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTaskByTQHK.xml";
            }
            else if (base.RequstStr("type") == "YWQX")
            {
                //业务取消
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTaskByYWQX.xml";
            }
            else if (base.RequstStr("type") == "ZLBG")
            {
                //客户资料变更
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTaskByZLBG.xml";
            }
            else if (base.RequstStr("type") == "ZLKD")
            {
                //快递资料
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTaskByZLKD.xml";
            }
            else if (base.RequstStr("type") == "XBZL")
            {
                //续保
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTaskByXBZL.xml";
            }
            else if (base.RequstStr("type") == "LPZL")
            {
                //理赔
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTaskByLPZL.xml";
            }
            else if (base.RequstStr("type") == "LPKD")
            {
                //理赔资料快递
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTaskByLPKD.xml";
            }
            else if (base.RequstStr("type") == "JYTX")
            {
                //解押提醒
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTaskByJYTX.xml";
            }
            else if (base.RequstStr("type") == "YQJM")
            {
                //逾期减免
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTaskByYQJM.xml";
            }
            else
            {
                UCEasyUIDataGrid.DataSource = "~/XML/Workflow/MyToDoTask.xml";
            }
            UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            if (!string.IsNullOrEmpty(base.RequstStr("MyDesktop")))
            {
                UCEasyUIDataGrid.DetailType = 1010;
            }
            else
            {
                UCEasyUIDataGrid.DetailType = 4;
            }
            GetList();
        }


        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            string id = HttpContext.Current.Request["id"];
            switch (active)
            {
                case "List"://加载列表
                    int totalcount = int.MinValue;
                    UCEasyUIDataGrid.BasicVariable();//基本变量赋值
                    UCEasyUIDataGrid.Where();//执行获取查看范围
                    UCEasyUIDataGrid.Datas = General.GetP_GetPendingMatterExAlready(RequstStr("type"), UCEasyUIDataGrid.SQLField, UCEasyUIDataGrid.TableKey, UCEasyUIDataGrid.SQLWhere, UCEasyUIDataGrid.SQLOrder, CurrUserInfo().UserID, UCEasyUIDataGrid.PageSize, UCEasyUIDataGrid.PageIndex, out totalcount);
                    UCEasyUIDataGrid.TotalCount = totalcount;
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
                    Response.End();
                    break;
                default:
                    break;
            }
        }

        protected string GetSqlWhere()
        {
            //查询的参数
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            //提前还款
            if (!string.IsNullOrEmpty(Request["txtName"]))
            {
                strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            }
            //if (base.RequstStr("type") == "TQHK")
            //{
            //    //提前还款
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(AdvancePay_ID,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',AdvancePay_UpdateUserID)>0");
            //}
            //else if (base.RequstStr("type") == "YWQX")
            //{
            //    //业务取消
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(BasisCancelID,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',BasisCancel_UpdateUserID)>0");
            //}
            //else if (base.RequstStr("type") == "ZLBG")
            //{
            //    //客户资料变更
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(CustomerInfoChange_ID,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',CustomerInfoChange_UpdateUserID)>0");
            //}
            //else if (base.RequstStr("type") == "ZLKD")
            //{
            //    //快递资料
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(ExpressInfoIDF,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',ExpressInfo_UpdateUserId)>0");
            //}
            //else if (base.RequstStr("type") == "XBZL")
            //{
            //    //续保
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(CustomerRenewalID,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',CustomerRenewal_UpdateUserId)>0");
            //}
            //else if (base.RequstStr("type") == "LPZL")
            //{
            //    //理赔
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(CustomerClaimsID,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',CustomerClaims_UpdateUserId)>0");
            //}
            //else if (base.RequstStr("type") == "LPKD")
            //{
            //    //理赔资料快递
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(ClaimsExpressID,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',ClaimsExpress_UpdateUserID)>0");
            //}
            //else if (base.RequstStr("type") == "JYTX")
            //{
            //    //解押提醒
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(SolutionRemind_ApplyBasisID,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',SolutionExpress_UpdateOn)>0");
            //    strWhere.AppendFormat(" and SolutionExpress_UpdateOn= '" + base.CurrUserInfo().UserID + "'");
            //}
            //else if (base.RequstStr("type") == "YQJM")
            //{
            //    //逾期减免
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(OverdueReliefID,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',OverdueRelief_UpdateUserID)>0");
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(Request["txtName"]))
            //    {
            //        strWhere.AppendFormat(" and ApplyBasis_Name like '%{0}%'", Request["txtName"]);
            //    }
            //    strWhere.AppendFormat(" and dbo.F_MyToDoTaskCondWhere(ApplyBasisId,'" + base.CurrUserInfo().UserID + "','" + base.CurrUserInfo().RoleID + "',ApplyBasis_UpdateUserID)>0");
            //}
            return strWhere.ToString();
        }
    }
}