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
    public partial class SalesConsultantList : BasePage
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
            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridSalesConsultantList.xml";
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
                    Result = DelBank(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 删除一条银行数据
        /// </summary>
        /// <param name="id"></param>
        public string DelBank(string id)
        {
            string msg = string.Empty;
            //Dealers_SalesConsultantBLL bllSalesConsultant = new Dealers_SalesConsultantBLL();
            //Dealers_SalesConsultant SalesConsultant = bllSalesConsultant.Find(p => p.SalesConsultant_ID == id);
            //SalesConsultant.Bank_UpdateTime = DateTime.Now;
            //SalesConsultant.Bank_UpdateUserId = base.CurrUserInfo().UserID;
            //SalesConsultant.Bank_Deleted = true;
            //bool bol = bllSalesConsultant.Update(model, out msg);
            //if (bol)
            //{
            //    //删除成功
            //    msg = "1";
            //}
            //else
            //{
            //    //删除失败
            //    msg = "0";
            //}
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
            //银行名称
            if (!string.IsNullOrEmpty(Request["txtBank_Name"]))
            {
                strWhere.AppendFormat(" and Bank_Name like '%{0}%'", Request["txtBank_Name"]);
            }
            //银行类型
            if (!string.IsNullOrEmpty(Request["ddlBank_Type"]))
            {
                strWhere.AppendFormat(" and Bank_Type = '{0}'", Request["ddlBank_Type"]);
            }
            return strWhere.ToString();
        }
    }
}