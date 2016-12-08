using CKEditor.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.BLL;
using Shu.Comm;
using Shu.Model;

namespace Shu.Manage.Sys
{
    public partial class SoftwareAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Software_Context.config.uiColor = "#BFEE62";
                Software_Context.config.enterMode = EnterMode.BR;
                if (Request.QueryString["id"] != null)
                {
                    hid_Id.Value = Request.QueryString["id"];
                }
                BindShow();

            }
        }

        public void BindShow()
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))  //修改时加载
            {

                Sys_SoftwareBLL bll = new Sys_SoftwareBLL();
                //Sys_NoticeFileBLL bllAttach = new Sys_NoticeFileBLL();

                Sys_Software model = bll.Find(p => p.SoftwareID == id);
                if (model == null)
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "SoftwareIndex.aspx");
                }
                else
                {
                    Software_Title.Text = model.Software_Name;
                    Software_Context.Text = model.Software_Introduction;
                    Software_No.Text = model.Software_No.ToString();
                    Session.Remove("ww");
                    this.File.FileOperationType = "Modify";
                    this.File.FileSizeLimit = "3000";
                    this.File.FilesNname = "File";
                    this.File.FileSessionID = "ww";
                    this.File.FileMulti = "false";
                    this.File.FileType = "系统必备工具";
                    this.File.FileTypeDesc = "支持格式：.exe;.msi;.rar;.7z;.zip;";
                    this.File.FileTypeExts = "*.exe;*.msi;*.rar;*.7z;*.zip;";
                    this.File.FileOperationID = model.SoftwareID;
                }
            }
            else
            {
                Session.Remove("ww");
                this.File.FileOperationType = "Add";
                this.File.FilesNname = "File";
                this.File.FileSessionID = "ww";
                this.File.FileMulti = "false";
                this.File.FileSizeLimit = "3000";
                this.File.FileType = "系统必备工具";
                this.File.FileTypeDesc = "支持格式：.exe;.msi;.rar;.7z;.zip;";
                this.File.FileTypeExts = "*.exe;*.msi;*.rar;*.7z;*.zip;";
                this.File.FileOperationID = Guid.NewGuid().ToString();
                //btn_downFile.Visible = false;//新增时不能下载附件
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, ImageClickEventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 预存
        /// </summary>
        public void Save()
        {
            string id = Request.QueryString["id"];
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                Sys_SoftwareBLL bll = new Sys_SoftwareBLL();
                Sys_Software model = bll.Find(p => p.SoftwareID == id);
                model.Software_Name = Software_Title.Text;
                model.Software_Introduction = Software_Context.Text;
                var no = Software_No.Text;
                double result=0;
                if (double.TryParse(no, out result))
                    model.Software_No = double.Parse(no);
                else
                    model.Software_No = 1;
                bool bol = bll.Update(model, out msg);
                if (bol)
                {
                    new Sys_ModelFileBLL().AddList(Session["ww"] as List<Sys_ModelFile>, out msg);
                    MessageBox.ShowAndRedirect(this, "保存成功！", "SoftwareIndex.aspx");
                }
                else
                {
                    MessageBox.Show(this, msg);
                    Session.Remove("ww");
                }
            }
            else
            {
                Sys_SoftwareBLL bll = new Sys_SoftwareBLL();
                Sys_Software model = new Sys_Software();
                model.SoftwareID = this.File.FileOperationID;
                model.Software_Name = Software_Title.Text;
                model.Software_Introduction = Software_Context.Text;
                var no = Software_No.Text;
                double result = 0;
                if (double.TryParse(no, out result))
                    model.Software_No = double.Parse(no);
                else
                    model.Software_No = 1;
                model.Software_UploadUserID = CurrUserInfo().UserID;
                model.Software_UploadTime = DateTime.Now;
                model.Software_IsExists = 0;
                if (bll.Add(model, out msg))
                {
                    new Sys_ModelFileBLL().AddList(Session["ww"] as List<Sys_ModelFile>, out msg);
                    MessageBox.ShowAndRedirect(this, "保存成功！", "SoftwareIndex.aspx");
                }
                else
                {
                    MessageBox.Show(this, msg);
                    Session.Remove("ww");
                }
            }

        }
    }
}