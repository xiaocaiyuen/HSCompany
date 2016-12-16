using Shu.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shu.Manage.Popup
{
    public partial class IconList : System.Web.UI.Page
    {
        protected int pageindex = 1;
        protected int recordcount = 0;
        protected int pagesize;
        //protected string pagehtmlsmall;
        protected string pagehtml;
        public StringBuilder strImg = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetImg();
            }
        }

        public void GetImg()
        {
            pagesize = WebUtil.GetQuery("pagesize", 200);
            pageindex = WebUtil.GetQuery("page", 1);

            //IconSize IconSize = (IconSize)Enum.Parse(typeof(IconSize), WebUtil.GetQuery("IconSize", "16"));
            //IconType IconType = (IconType)Enum.Parse(typeof(IconType), WebUtil.GetQuery("IconType", "Button"));

            string IconSize = WebUtil.GetQuery("IconSize", "16");
            string IconType = WebUtil.GetQuery("IconType", "Button");
            string IconPath = "/Content/Icons/" + IconType + "/" + IconSize + "/";
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath(IconPath));

            //if (IconType == "Button")//如果是操作按钮需要合并easyui Icon
            //{
            //    dir += new DirectoryInfo(Server.MapPath(IconPath));
            //}

            int rowbegin = (pageindex - 1) * pagesize;
            int rowend = pageindex * pagesize;
            foreach (FileInfo fsi in dir.GetFileSystemInfos())
            {
                if (recordcount >= rowbegin && recordcount < rowend)
                {
                    strImg.Append("<div class=\"divicons\" iconName=\""+ fsi.Name.Remove(fsi.Name.LastIndexOf(".")) + "\" title='" + IconPath + fsi.Name + "'>");
                    strImg.Append("<img src=\"" + IconPath + "" + fsi.Name + "\" />");
                    strImg.Append("</div>");
                }
                recordcount++;
            }
            pagehtml = WebUtil.GetPaginationHtml(recordcount, pageindex, pagesize);
        }


        ///// <summary>
        ///// 图标大小
        ///// </summary>
        //public enum IconSize
        //{
        //    /// <summary>
        //    /// 16*16
        //    /// </summary>
        //    Icon16 = 16,
        //    /// <summary>
        //    /// 32*32
        //    /// </summary>
        //    Icon32 = 32,
        //    /// <summary>
        //    /// 64*64
        //    /// </summary>
        //    Icon64 = 64,
        //}

        ///// <summary>
        ///// 图标类型
        ///// </summary>
        //public enum IconType
        //{
        //    /// <summary>
        //    /// 按钮图标
        //    /// </summary>
        //    Button,
        //    /// <summary>
        //    /// 菜单图标
        //    /// </summary>
        //    Menu
        //}
    }
}