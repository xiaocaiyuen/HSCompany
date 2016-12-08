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
    public partial class Configsetting : BasePage
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
            // 附件信息
            Session.Remove("ww");
            this.File.FileSizeLimit = "3000";
            this.File.FilesNname = "File";
            this.File.FileSessionID = "ww";
            this.File.FileFBL = "1920*1080";
            this.File.FileType = "登陆背景图片";
            this.File.FileOperationID = "denglubeijing";

            // 附件信息
            Session.Remove("ww2");
            this.File2.FileSizeLimit = "3000";
            this.File2.FilesNname = "File2";
            this.File2.FileSessionID = "ww2";
            this.File2.FileFBL = "417*50";
            this.File2.FileType = "主框架LOGO背景图片";
            this.File2.FileOperationID = "zhukuangjiatupian";
            //this.File1.FileOperationID = Guid.NewGuid().ToString(); ;


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["ww"] != null)
            {
                new Sys_ModelFileBLL().Add(Session["ww"] as List<Sys_ModelFile>);
                Session.Remove("ww");
            }
            if (Session["ww2"] != null)
            {
                new Sys_ModelFileBLL().Add(Session["ww2"] as List<Sys_ModelFile>);
                Session.Remove("ww2");
            }
            
        }
    }
}