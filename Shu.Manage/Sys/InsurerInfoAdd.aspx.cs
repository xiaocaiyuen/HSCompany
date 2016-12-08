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
    public partial class InsurerInfoAdd : BasePage
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
                Sys_InsurerInfoBLL bllInsurer = new Sys_InsurerInfoBLL();
                Sys_InsurerInfo InsurerInfo = bllInsurer.Find(p => p.InsurerInfoId == id);
                if (InsurerInfo == null)
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "InsurerInfoList.aspx");
                }
                else
                {
                    FormModel.SetForm<Sys_InsurerInfo>(this, InsurerInfo, "txt");
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
            Sys_InsurerInfoBLL bllInsurer = new Sys_InsurerInfoBLL();
            if (!string.IsNullOrEmpty(id))  //编辑保存
            {
                Sys_InsurerInfo InsurerInfo = bllInsurer.Find(p => p.InsurerInfoId == id);
                FormModel.GetForm<Sys_InsurerInfo>(this, InsurerInfo, "txt");
                InsurerInfo.InsurerInfo_UpdateTime = DateTime.Now;
                InsurerInfo.InsurerInfo_UpdateUserId = base.CurrUserInfo().UserID;
                bool bol = bllInsurer.Update(InsurerInfo, out msg);
                if (bol)
                {
                    MessageBox.ShowAndRedirect(this, "保存成功！", "InsurerInfoList.aspx");
                }
                else
                {
                    MessageBox.Show(this, msg);
                }
            }
            else  //新增保存
            {
                Sys_InsurerInfo InsurerInfo = bllInsurer.Find(p => p.InsurerInfo_Name == txtInsurerInfo_Name.Text.Trim());
                if (InsurerInfo != null)    //保险公司名称是否重复
                {
                    MessageBox.Show(this, "此保险公司已经存在，无法再次添加！");
                }
                else
                {
                    InsurerInfo = new Sys_InsurerInfo();
                    FormModel.GetForm<Sys_InsurerInfo>(this, InsurerInfo, "txt");
                    InsurerInfo.InsurerInfoId = Guid.NewGuid().ToString();
                    InsurerInfo.InsurerInfo_UpdateTime = DateTime.Now;
                    InsurerInfo.InsurerInfo_UpdateUserId = base.CurrUserInfo().UserID;
                    InsurerInfo.InsurerInfo_IsDelete = false;
                    bool bol = bllInsurer.Add(InsurerInfo, out msg);
                    if (bol)
                    {
                        MessageBox.ShowAndRedirect(this, "保存成功！", "InsurerInfoList.aspx");
                    }
                    else
                    {
                        MessageBox.Show(this, msg);
                    }
                }
            }
        }
    }
}