using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using Shu.BLL;
using Shu.Model;

namespace Shu.Manage.Sys
{
    public partial class NoticeShow : BasePage
    {
        DownFile downfile = new DownFile();
        Sys_NoticeBLL bllNotice = new Sys_NoticeBLL();
        Sys_ModelFileBLL bllFiles = new Sys_ModelFileBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Show();
            }
        }

        public void Show()
        {
            string id = Request.QueryString["id"];
            Sys_Notice model = bllNotice.Get(p => p.NoticeID == id);
            if (model != null)
            {
                Notice_Title.Text = model.Notice_Title;
                Notice_Context.Text = model.Notice_Content;
                string hid_id = Request.QueryString["id"].ToString();

                List<Sys_ModelFile> newsfiles = bllFiles.GetList(p => p.File_OperationID == hid_id).ToList();
                if (newsfiles.Count > 0)
                {
                    foreach (Sys_ModelFile file in newsfiles)
                    {
                        this.lblFj.Text += "<a href=\"/Files/Download.aspx?path=" + file.File_Path + "&filename=" + file.File_Name + "\"  style=\"cursor:pointer\">" + (new ListItem(file.File_Name, file.FileID).Text) + "</br>";
                    }
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "该数据不存在！", "NoticeList.aspx");
            }
        }
    }
}