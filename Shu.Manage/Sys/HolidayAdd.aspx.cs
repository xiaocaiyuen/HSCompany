using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using YDT.BLL;
using YDT.Model;
using System.Text;

namespace YDT.Web.Manage.Sys
{
    public partial class HolidayAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                Sys_HolidayBLL bllHoliday = new Sys_HolidayBLL();
                Sys_Holiday model = bllHoliday.Find(p => p.HolidayId == id);
                if (model == null)
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "HolidayList.aspx");
                }
                else
                {
                    txtHoliday_Name.Text = model.Holiday_Name;
                    txtHoliday_StartDate.Text = Convert.ToDateTime(model.Holiday_StartDate).ToString("yyyy年MM月dd日");
                    txtHoliday_EndDate.Text = Convert.ToDateTime(model.Holiday_EndDate).ToString("yyyy年MM月dd日");
                }
            }
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
            Sys_HolidayBLL bllHoliday = new Sys_HolidayBLL();
            StringBuilder strWhere = new StringBuilder();
            string startDate = Convert.ToDateTime(txtHoliday_StartDate.Text.Trim()).ToString("yyyy-MM-dd") + " 00:00:00";
            string endDate = Convert.ToDateTime(txtHoliday_EndDate.Text.Trim()).ToString("yyyy-MM-dd") + " 23:59:59";
            strWhere.AppendFormat(" ('{0}' <= Holiday_EndDate AND '{1}' >= Holiday_StartDate AND '{0}' <= '{1}' ) AND HolidayId <> '{2}' ", startDate, endDate,id);
            //获取待保存的“节假日时间段”是否与其它节假日时间段有交集
            List<Sys_Holiday> list = bllHoliday.FindWhere(strWhere.ToString());
            if (list.Count > 0)
            {
                MessageBox.Show(this, "此节假日时间段与其它节假日时间段存在交集，无法保存！");
                return;
            }

            //编辑保存
            if (!string.IsNullOrEmpty(id))
            {
                Sys_Holiday model = bllHoliday.Find(p => p.HolidayId == id);
                model.Holiday_Name = txtHoliday_Name.Text.Trim();
                model.Holiday_StartDate = Convert.ToDateTime(startDate);
                model.Holiday_EndDate = Convert.ToDateTime(endDate);
                model.Holiday_UpdateTime = DateTime.Now;
                model.Holiday_UpdateUserId = base.CurrUserInfo().UserID;
                bool bol = bllHoliday.Update(model, out msg);
                if (bol)
                {
                    MessageBox.ShowAndRedirect(this, "保存成功！", "HolidayList.aspx");
                }
                else
                {
                    MessageBox.Show(this, msg);
                }
            }
            else  //新增保存
            {
                Sys_Holiday model = new Sys_Holiday();
                model.HolidayId = Guid.NewGuid().ToString();
                model.Holiday_Name = txtHoliday_Name.Text.Trim();
                model.Holiday_StartDate = Convert.ToDateTime(startDate);
                model.Holiday_EndDate = Convert.ToDateTime(endDate);
                model.Holiday_UpdateTime = DateTime.Now;
                model.Holiday_UpdateUserId = base.CurrUserInfo().UserID;
                model.Holiday_IsDelete = false;
                bool bol = bllHoliday.Add(model, out msg);
                if (bol)
                {
                    txtHoliday_Name.Text = "";
                    txtHoliday_StartDate.Text = "";
                    txtHoliday_EndDate.Text = "";
                    MessageBox.Show(this, "保存成功！请继续添加。");
                }
                else
                {
                    MessageBox.Show(this, msg);
                }
            }
        }
    }
}