using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using Shu.BLL;
using Shu.Model;
using CKEditor.NET;
using Shu.Utility.Extensions;

namespace Shu.Manage.Sys
{
    public partial class NoticeAdd : BasePage
    {
        Sys_NoticeBLL snBll = new Sys_NoticeBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Notice_Context.config.uiColor = "#BFEE62";
                //Notice_Context.config.language = "zh";
                Notice_Context.config.enterMode = EnterMode.BR;
                if (Request.QueryString["id"] != null)
                {
                    hid_Id.Value = Request.QueryString["id"];
                }
                BindDDL();
                BindShow();
            }
        }


        public void BindDDL()
        {
            Sys_DataDictBLL balDataDict = new Sys_DataDictBLL();
            List<Sys_DataDict> listDataDict = balDataDict.GetList(p => p.DataDict_ParentCode == "26").OrderBy(p => p.DataDict_Sequence).ToList();
            this.ddlNotice_Type.DataSource = listDataDict;
            this.ddlNotice_Type.DataTextField = "DataDict_Name";
            this.ddlNotice_Type.DataValueField = "DataDict_Code";
            this.ddlNotice_Type.DataBind();
        }


        /// <summary>
        /// 
        /// </summary>
        public void BindShow()
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))  //修改时加载
            {

                Sys_NoticeBLL bll = new Sys_NoticeBLL();
                //Sys_NoticeFileBLL bllAttach = new Sys_NoticeFileBLL();

                Sys_Notice model = bll.Get(p => p.NoticeID == id);
                if (model == null)
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "NoticeList.aspx");

                }
                else
                {
                    ddlNotice_Type.SelectedValue = model.Notice_Type;
                    Notice_Title.Text = model.Notice_Title;
                    Notice_Context.Text = model.Notice_Content;
                    ShowScope();
                    //ddlIsShow.SelectedValue = model.Notice_IsShow.ToString();
                    Session.Remove("ww");
                    this.File.FileSizeLimit = "3000";
                    this.File.FilesNname = "File";
                    this.File.FileSessionID = "ww";
                    this.File.FileType = "通知公告";
                    this.File.FileTypeDesc = "支持格式：.jpg;.gif;.png;.bmp;.jpeg;.doc;.docx;.xls;.xlsx;.ppt;.pptx;.pdf;.flv;.zip;.rar;.txt;";
                    this.File.FileTypeExts = "*.jpg;*.gif;*.png;*.bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.pdf;*.flv;*.zip;*.rar;*.txt";
                    this.File.FileOperationID = model.NoticeID;
                }
            }
            else
            {
                Session.Remove("ww");
                this.File.FilesNname = "File";
                this.File.FileSessionID = "ww";
                this.File.FileSizeLimit = "3000";
                this.File.FileType = "通知公告";
                this.File.FileTypeDesc = "支持格式：.jpg;.gif;.png;.bmp;.jpeg;.doc;.docx;.xls;.xlsx;.ppt;.pptx;.pdf;.flv;.zip;.rar;.txt;";
                this.File.FileTypeExts = "*.jpg;*.gif;*.png;*.bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.pdf;*.flv;*.zip;*.rar;*.txt";
                this.File.FileOperationID = Guid.NewGuid().ToString();
                //btn_downFile.Visible = false;//新增时不能下载附件
            }
        }

        public void ShowScope()
        {
            Sys_DepartmentBLL depbll = new Sys_DepartmentBLL();
            Sys_Notice sn = snBll.Get(p => p.NoticeID == hid_Id.Value);
            string val = sn.Notice_Scope;
            if (val != null)
            {
                hid_DepId.Value = val;
                string str = "";
                string[] spl = val.Split(',');

                foreach (string item in spl)
                {
                    Sys_Department depmode = depbll.Get(p => p.Department_Code == item);
                    if (depmode != null)
                    {
                        str += depmode.Department_Name + ",";
                    }
                }
                if (!str.IsNullOrEmpty())
                {
                    str = str.Substring(0, str.Length - 1);
                }
                
                UCDepartmentTreeText1.Text = str;
            }
        }
        /// <summary>
        /// 预存
        /// </summary>
        public void Save()
        {
            //System.Threading.Thread.Sleep(5000);
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Sys_NoticeBLL bll = new Sys_NoticeBLL();
                Sys_Notice model = bll.Get(p => p.NoticeID == id);
                model.Notice_Type = ddlNotice_Type.SelectedValue;
                model.Notice_Title = Notice_Title.Text;
                model.Notice_Content = Notice_Context.Text;
                model.IsDelete = false;
                model.Sort = 1;
                string cole = hid_DepId.Value.ToString();
                model.Notice_Scope = cole;
                //model.Notice_IsShow = Convert.ToBoolean(ddlIsShow.SelectedValue);
                bool bol = bll.Update(model);
                if (bol)
                {
                    new Sys_ModelFileBLL().Add(Session["ww"] as List<Sys_ModelFile>);
                    MessageBox.ShowAndRedirect(this, "保存成功！", "NoticeList.aspx");

                }
                else
                {
                    MessageBox.Show(this, "保存出错");
                    Session.Remove("ww");
                }
            }
            else
            {

                Sys_NoticeBLL bll = new Sys_NoticeBLL();
                Sys_Notice model = new Sys_Notice();
                model.NoticeID = this.File.FileOperationID;
                model.Notice_Type = ddlNotice_Type.SelectedValue;
                //model.Notice_IsShow = Convert.ToBoolean(ddlIsShow.SelectedValue);
                model.Notice_Title = Notice_Title.Text;
                model.Notice_Content = Notice_Context.Text;
                model.AddUserId = CurrUserInfo().UserID;
                model.AddTime = DateTime.Now;
                model.IsDelete = false;
                model.Sort = 1;
                string cole = hid_DepId.Value.ToString();
                model.Notice_Scope = cole;
                // btn_downFile.Visible = false;//新增时不能下载附件
                // List<Sys_NoticeFile> files = GetFileInfo(model.NoticeID);
                //if (files != null)
                //{
                //    model.Sys_NoticeFile = files;
                //}
                if (bll.Add(model))
                {
                    new Sys_ModelFileBLL().Add(Session["ww"] as List<Sys_ModelFile>);
                    MessageBox.ShowAndRedirect(this, "保存成功！", "NoticeList.aspx");

                }
                else
                {
                    MessageBox.Show(this, "保存出错");
                    Session.Remove("ww");
                }
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
    }
}