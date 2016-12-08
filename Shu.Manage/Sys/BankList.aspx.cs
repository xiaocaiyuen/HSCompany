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
    public partial class BankList : BasePage
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
            listDataDict = balDataDict.FindWhere(p => p.DataDict_ParentCode == "47").OrderBy(p => p.DataDict_Sequence).ToList();
            listDataDict.Insert(0, new Sys_DataDict { DataDict_Name = "--全部--", DataDict_Code = "" });
        }

        /// <summary>
        /// 绑定列表信息
        /// </summary>
        public void InitGrid()
        {
            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridBankList.xml";
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
            Business_BankBLL bllBank = new Business_BankBLL();
            Business_Bank model = new Business_Bank();
            model = bllBank.Find(p => p.BankID == id);
            model.Bank_UpdateTime = DateTime.Now;
            model.Bank_UpdateUserId = base.CurrUserInfo().UserID;
            model.Bank_Deleted = true;
            bool bol = bllBank.Update(model, out msg);
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