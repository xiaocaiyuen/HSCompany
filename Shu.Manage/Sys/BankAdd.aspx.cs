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
    public partial class BankAdd : BasePage
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
                Business_BankBLL bllBank = new Business_BankBLL();
                Business_Bank model = bllBank.Find(p => p.BankID == id);
                if (model == null)
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "BankList.aspx");
                }
                else
                {
                    txtBank_Name.Text = model.Bank_Name;
                    txtBank_No.Text = model.Bank_No;
                    txtBank_Contact.Text = model.Bank_Contact;
                    txtBank_Telephone.Text = model.Bank_Telephone;
                    ddlBank_Type.SelectedValue = model.Bank_Type;
                    txtBank_Address.Text = model.Bank_Address;
                    txtBank_Account.Text = model.Bank_Account;
                    txtBank_AccountName.Text = model.Bank_AccountName;
                    ddlBank_AccountType.SelectedValue = model.Bank_AccountType;
                    txtBank_OpenBName.Text = model.Bank_OpenBName;
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
            List<Sys_DataDict> listBank_Type = balDataDict.FindWhere(p => p.DataDict_ParentCode == "47").OrderBy(p => p.DataDict_Sequence).ToList();
            this.ddlBank_Type.DataSource = listBank_Type;
            this.ddlBank_Type.DataTextField = "DataDict_Name";
            this.ddlBank_Type.DataValueField = "DataDict_Code";
            this.ddlBank_Type.DataBind();
            //账户类型
            List<Sys_DataDict> listAccountType = balDataDict.FindWhere(p => p.DataDict_ParentCode == "48").OrderBy(p => p.DataDict_Sequence).ToList();
            this.ddlBank_AccountType.DataSource = listAccountType;
            this.ddlBank_AccountType.DataTextField = "DataDict_Name";
            this.ddlBank_AccountType.DataValueField = "DataDict_Code";
            this.ddlBank_AccountType.DataBind();
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
            Business_BankBLL bllBank = new Business_BankBLL();
            //编辑保存
            if (!string.IsNullOrEmpty(id))
            {
                Business_Bank model = bllBank.Find(p => p.BankID == id);
                FormModel.GetForm<Business_Bank>(this, model, "txt");
                FormModel.GetForm<Business_Bank>(this, model, "ddl");
                model.Bank_UpdateTime = DateTime.Now;
                model.Bank_UpdateUserId = base.CurrUserInfo().UserID;
                bool bol = bllBank.Update(model, out msg);
                if (bol)
                {
                    MessageBox.ShowAndRedirect(this, "保存成功！", "BankList.aspx");
                }
                else
                {
                    MessageBox.Show(this, msg);
                }
            }
            else  //新增保存
            {
                Business_Bank model = new Business_Bank();
                FormModel.GetForm<Business_Bank>(this, model, "txt");
                FormModel.GetForm<Business_Bank>(this, model, "ddl");
                model.BankID = Guid.NewGuid().ToString();
                model.Bank_Deleted = false;
                bool bol = bllBank.Add(model, out msg);
                if (bol)
                {
                    MessageBox.ShowAndRedirect(this, "保存成功！", "BankList.aspx");
                }
                else
                {
                    MessageBox.Show(this, msg);
                }
            }
        }
    }
}