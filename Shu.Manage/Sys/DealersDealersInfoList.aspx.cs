using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.BLL;
using YDT.Comm;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class DealersDealersInfoList : BasePage
    {
        protected List<Sys_DataDict> listDataDict;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定查询条件下拉框
                BindDDL();
                //初始化表格列表
                InitGrid();
            }
        }

        /// <summary>
        /// 绑定查询条件下拉框
        /// </summary>
        public void BindDDL()
        {
            Sys_DataDictBLL balDataDict = new Sys_DataDictBLL();
            listDataDict = balDataDict.FindWhere(p => p.DataDict_Code == "4504" || p.DataDict_Code == "4505").OrderBy(p => p.DataDict_Sequence).ToList();
            listDataDict.Insert(0, new Sys_DataDict { DataDict_Name = "--全部--", DataDict_Code = "" });
        }

        /// <summary>
        /// 初始化表格列表
        /// </summary>
        public void InitGrid()
        {
            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridDealersDealersInfoList.xml";
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            GetList();
        }

        /// <summary>
        /// 方法
        /// </summary>
        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            switch (active)
            {
                case "List"://加载列表
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
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
            //经销商名称
            if (!string.IsNullOrEmpty(Request["txtDealersInfo_FullName"]))
            {
                strWhere.AppendFormat(" AND DealersInfo_FullName Like '%{0}%'", Request["txtDealersInfo_FullName"]);
            }
            //类型
            if (!string.IsNullOrEmpty(Request["ddlDealersInfo_Type"]))
            {
                strWhere.AppendFormat(" and DealersInfo_Type = '{0}'", Request["ddlDealersInfo_Type"]);
            }

            return strWhere.ToString();
        }
    }
}