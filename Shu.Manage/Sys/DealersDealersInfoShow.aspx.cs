using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using YDT.BLL;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class DealersDealersInfoShow : BasePage
    {
        protected Sys_ModelFileBLL bllFiles = new Sys_ModelFileBLL();
        protected View_Dealers_DealersInfo model = new View_Dealers_DealersInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            //加载页面信息
            BindShow();
        }

        /// <summary>
        /// 加载页面信息
        /// </summary>
        private void BindShow()
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                View_Dealers_DealersInfoBLL bllDealers_DealersInfo = new View_Dealers_DealersInfoBLL();
                model = bllDealers_DealersInfo.Find(p => p.DealersInfoID == id);
                if (model == null)
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "DealersDealersInfoList.aspx");
                }
                else
                {

                    FormModel.SetForm<View_Dealers_DealersInfo>(this, model, "lbl");
                    lblDealersInfo_CLDate.Text = model.DealersInfo_CLDate == null ? "" : Convert.ToDateTime(model.DealersInfo_CLDate).ToString("yyyy年MM月dd日");
                    lblDealersInfo_ValidityPeriod.Text = model.DealersInfo_ValidityPeriod == null ? "" : Convert.ToDateTime(model.DealersInfo_ValidityPeriod).ToString("yyyy年MM月dd日");
                    lblDealersInfo_CooperativeValidity.Text = model.DealersInfo_CooperativeValidity == null ? "" : Convert.ToDateTime(model.DealersInfo_CooperativeValidity).ToString("yyyy年MM月dd日");
                }
            }
        }
    }
}