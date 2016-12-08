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
    public partial class CompanyList : BasePage
    {
        protected List<Sys_DataDict> listDataDict;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定查询条件下拉框
                BindDDL();
                //绑定列表信息
                InitGrid();
            }
        }

        /// <summary>
        /// 绑定查询条件下拉框
        /// </summary>
        public void BindDDL()
        {
            Sys_DataDictBLL balDataDict = new Sys_DataDictBLL();
            listDataDict = balDataDict.FindWhere(p => p.DataDict_ParentCode == "44").OrderBy(p => p.DataDict_Sequence).ToList();
            listDataDict.Insert(0, new Sys_DataDict { DataDict_Name = "--全部--", DataDict_Code = "" });
        }

        /// <summary>
        /// 绑定列表信息
        /// </summary>
        public void InitGrid()
        {
            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridCompanyList.xml";
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            GetList();
        }

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
            //公司名称
            if (!string.IsNullOrEmpty(Request["txtCompanyInfo_Name"]))
            {
                strWhere.AppendFormat(" and CompanyInfo_CNName like '%{0}%'", Request["txtCompanyInfo_Name"]);
            }
            //公司类型
            if (!string.IsNullOrEmpty(Request["ddlCompanyInfo_Type"]))
            {
                strWhere.AppendFormat(" and CompanyInfo_Type = '{0}'", Request["ddlCompanyInfo_Type"]);
            }
            return strWhere.ToString();
        }
    }
}