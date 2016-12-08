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
    public partial class CompanyEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定下拉框
                BindDDL();
                //加载页面信息
                BindShow();
            }
        }

        /// <summary>
        /// 加载页面信息
        /// </summary>
        private void BindShow()
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Business_CompanyInfoBLL bllCompany = new Business_CompanyInfoBLL();
                Business_CompanyInfo CompanyInfo = bllCompany.Find(p => p.CompanyInfoID == id);
                if (CompanyInfo == null)
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "CompanyList.aspx");
                }
                else
                {
                    FormModel.SetForm<Business_CompanyInfo>(this, CompanyInfo, "txt");
                    FormModel.SetForm<Business_CompanyInfo>(this, CompanyInfo, "ddl");
                }
            }
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        private void BindDDL()
        {
            Sys_DataDictBLL balDataDict = new Sys_DataDictBLL();
            //银行类型
            List<Sys_DataDict> listCompanyInfo_Type = balDataDict.FindWhere(p => p.DataDict_ParentCode == "44").OrderBy(p => p.DataDict_Sequence).ToList();
            this.ddlCompanyInfo_Type.DataSource = listCompanyInfo_Type;
            this.ddlCompanyInfo_Type.DataTextField = "DataDict_Name";
            this.ddlCompanyInfo_Type.DataValueField = "DataDict_Code";
            this.ddlCompanyInfo_Type.DataBind();

            Business_BankBLL BankBLL = new Business_BankBLL();
            //银行信息
            List<Business_Bank> listBusiness_Bank = BankBLL.FindWhere(p => p.Bank_Deleted == false);
            this.ddlCompanyInfo_Bank.DataSource = listBusiness_Bank;
            this.ddlCompanyInfo_Bank.DataTextField = "Bank_Name";
            this.ddlCompanyInfo_Bank.DataValueField = "BankID";
            this.ddlCompanyInfo_Bank.DataBind();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request.QueryString["id"];
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                Business_CompanyInfoBLL bllCompany = new Business_CompanyInfoBLL();
                Business_CompanyInfo CompanyInfo = bllCompany.Find(p => p.CompanyInfoID == id);
                FormModel.GetForm<Business_CompanyInfo>(this, CompanyInfo, "txt");
                FormModel.GetForm<Business_CompanyInfo>(this, CompanyInfo, "ddl");
                CompanyInfo.CompanyInfo_UpdateTime = DateTime.Now;
                CompanyInfo.CompanyInfo_UpdateUserId = base.CurrUserInfo().UserID;
                bool bol = bllCompany.Update(CompanyInfo, out msg);
                if (bol)
                    MessageBox.ShowAndRedirect(this, "保存成功！", "CompanyList.aspx");
            }
            else
            {
                MessageBox.Show(this, msg);
            }
        }
    }
}