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
        public StringBuilder strImg = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetImg();
        }

        public void GetImg()
        {
            int PageSize = 200;
            int PageIndex = 1;
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("/Content/Icons/Button/16/"));
            int rowCount = 0;
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;
            foreach (FileInfo fsi in dir.GetFileSystemInfos())
            {
                if (rowCount >= rowbegin && rowCount < rowend)
                {
                    strImg.Append("<div class=\"divicons\" title='" + fsi.Name + "'>");
                    strImg.Append("<img src=\"/Content/Icons/Button/" + 16 + "/" + fsi.Name + "\" />");
                    strImg.Append("</div>");
                }
                rowCount++;
            }
            //this.PageControl1.RecordCount = Convert.ToInt32(rowCount);
        }
    }
}