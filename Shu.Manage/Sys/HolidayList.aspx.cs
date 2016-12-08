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
    public partial class HolidayList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtHoliday_StartDate.Value = DateTime.Now.ToString("yyyy") + "-01-01";
                //绑定列表信息
                InitGrid();
            }
        }

        /// <summary>
        /// 绑定列表信息
        /// </summary>
        public void InitGrid()
        {
            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridHolidayList.xml";
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            GetList();
        }

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
                case "DelButton"://删除数据
                    Result = UCEasyUIDataGrid.DelInfo(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1");
            //节假日名称
            if (!string.IsNullOrEmpty(Request["txtHoliday_Name"]))
            {
                strWhere.AppendFormat(" and Holiday_Name like '%{0}%'", Request["txtHoliday_Name"]);
            }
            //节假日时段
            string startDate = Constant.Sys_MinDateTime;
            if (!string.IsNullOrEmpty(Request["txtHoliday_StartDate"]))
            {
                startDate = Request["txtHoliday_StartDate"];
            }
            string endDate = Constant.Sys_MaxDateTime;
            if (!string.IsNullOrEmpty(Request["txtHoliday_EndDate"]))
            {
                endDate = Request["txtHoliday_EndDate"] + " 23:59:59";
            }
            strWhere.AppendFormat(" AND ('{0}' <= Holiday_StartDate AND '{1}' >= Holiday_StartDate )", startDate, endDate);
            return strWhere.ToString();
        }
    }
}