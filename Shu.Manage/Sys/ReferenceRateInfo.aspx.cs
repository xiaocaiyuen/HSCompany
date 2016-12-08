using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using YDT.BLL;
using YDT.Model;
using YDT.Utility;
using YDT.Utility.Extensions;

namespace YDT.Web.Manage.Sys
{
    public partial class ReferenceRateInfo : BasePage
    {
        private string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string reval = Request.QueryString["flag"];
            if (reval.Substring(0, 3).Equals("add"))
            {
                id = string.Empty;
            }
            else
            {
                id = reval.Split('=')[1];
            }
            if (!IsPostBack)
            {
                Show();
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void Show()
        {
            if (!string.IsNullOrEmpty(id))
            {
                Sys_ReferenceRateBLL bllReferenceRate = new Sys_ReferenceRateBLL();
                Sys_ReferenceRate ReferenceRate = bllReferenceRate.Find(p => p.ReferenceRate_ID == id);
                if (ReferenceRate != null)
                {
                    txtReferenceRate_InterestRate.Text = ReferenceRate.ReferenceRate_InterestRate.ToString();
                    txtReferenceRate_AdjustDate.Text = Convert.ToDateTime(ReferenceRate.ReferenceRate_AdjustDate).ToString("yyyy-MM-dd");
                }
                else
                {
                    MessageBox.Show(this, "没有此数据！");
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Sys_ReferenceRateBLL bllReferenceRate = new Sys_ReferenceRateBLL();
            Sys_ReferenceRate ReferenceRate = new Sys_ReferenceRate();
            string msg = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                ReferenceRate.ReferenceRate_ID = Guid.NewGuid().ToString();
                ReferenceRate.ReferenceRate_InterestRate = Convert.ToDouble(txtReferenceRate_InterestRate.Text.Trim());
                ReferenceRate.ReferenceRate_AdjustDate = Convert.ToDateTime(txtReferenceRate_AdjustDate.Text.Trim());
                ReferenceRate.ReferenceRate_StartDate = ReferenceRate.ReferenceRate_AdjustDate;
                ReferenceRate.ReferenceRate_IsEnable = false;
                ReferenceRate.ReferenceRate_IsExecute = false;
                ReferenceRate.ReferenceRate_CreateTime = DateTime.Now;
                ReferenceRate.ReferenceRate_CreateUserId = base.CurrUserInfo().UserID;
                ReferenceRate.ReferenceRate_IsDelete = false;
                if (bllReferenceRate.Add(ReferenceRate, out msg))
                {
                    MessageBox.ResponseScript(this, "alert('保存成功！');parent.location.reload();");
                }
                else { MessageBox.Show(this, msg); }
            }
            else
            {
                ReferenceRate = bllReferenceRate.Find(p => p.ReferenceRate_ID == id);
                ReferenceRate.ReferenceRate_InterestRate = Convert.ToDouble(txtReferenceRate_InterestRate.Text.Trim());
                ReferenceRate.ReferenceRate_AdjustDate = Convert.ToDateTime(txtReferenceRate_AdjustDate.Text.Trim());
                ReferenceRate.ReferenceRate_StartDate = ReferenceRate.ReferenceRate_AdjustDate;
                ReferenceRate.ReferenceRate_UpdateTime = DateTime.Now;
                ReferenceRate.ReferenceRate_UpdateUserId = base.CurrUserInfo().UserID;
                if (bllReferenceRate.Update(ReferenceRate, out msg))
                {
                    MessageBox.ResponseScript(this, "alert('保存成功');parent.location.reload();");
                }
                else
                {
                    MessageBox.Show(this, msg);
                }
            }
        }
    }
}