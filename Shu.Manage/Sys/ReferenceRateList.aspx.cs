using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using System.Text;
using YDT.BLL;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class ReferenceRateList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定列表信息
                InitGrid();
            }
        }

        /// <summary>
        /// 绑定列表信息
        /// </summary>
        public void InitGrid()
        {
            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridReferenceRateList.xml";
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            UCEasyUIDataGrid.AddType = 3;
            UCEasyUIDataGrid.EditType = 3;
            GetList();
        }

        /// <summary>
        /// 方法
        /// </summary>
        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            string id = HttpContext.Current.Request["id"];
            string Result = "";
            switch (active)
            {
                case "List"://加载列表
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
                    Response.End();
                    break;
                case "OpenButton"://启用
                    Result = OpenReferenceRate(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                case "DelButton"://删除数据
                    Result = DelReferenceRate(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 启用一条基准利率
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string OpenReferenceRate(string id)
        {
            string msg = string.Empty;
            string flag = string.Empty;
            Sys_ReferenceRateBLL bllReferenceRate = new Sys_ReferenceRateBLL();
            flag = bllReferenceRate.OpenReferenceRate(id, base.CurrUserInfo().UserID);
            return flag;
        }

        /// <summary>
        /// 删除一条基准利率
        /// </summary>
        /// <param name="id"></param>
        public string DelReferenceRate(string id)
        {
            string msg = string.Empty;
            Sys_ReferenceRateBLL bllReferenceRate = new Sys_ReferenceRateBLL();
            Sys_ReferenceRate model = new Sys_ReferenceRate();
            model = bllReferenceRate.Find(p => p.ReferenceRate_ID == id);
            model.ReferenceRate_UpdateTime = DateTime.Now;
            model.ReferenceRate_UpdateUserId = base.CurrUserInfo().UserID;
            model.ReferenceRate_IsDelete = true;
            bool bol = bllReferenceRate.Update(model, out msg);
            if (bol)
            {
                //删除成功
                msg = "1";
            }
            else
            {
                //删除失败
                msg = "0";
            }
            return msg;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1");
            //调整日时段
            string startDate = Constant.Sys_MinDateTime;
            if (!string.IsNullOrEmpty(Request["txtReferenceRate_StartDate"]))
            {
                startDate = Request["txtReferenceRate_StartDate"];
            }
            string endDate = Constant.Sys_MaxDateTime;
            if (!string.IsNullOrEmpty(Request["txtReferenceRate_EndDate"]))
            {
                endDate = Request["txtReferenceRate_EndDate"] + " 23:59:59";
            }
            strWhere.AppendFormat(" AND ('{0}' <= ReferenceRate_AdjustDate AND '{1}' >= ReferenceRate_AdjustDate )", startDate, endDate);
            return strWhere.ToString();
        }
    }
}