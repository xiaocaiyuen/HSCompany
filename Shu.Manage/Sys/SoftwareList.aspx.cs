using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.BLL;
using Shu.Model;

namespace Shu.Manage.Sys
{
    public partial class SoftwareList : System.Web.UI.Page
    {
        Sys_SoftwareBLL bll = new Sys_SoftwareBLL();
        Sys_ModelFileBLL smbll = new Sys_ModelFileBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
            }
        }

        public string InitTable()
        {
            List<Sys_Software> lst = bll.FindWhere(s => s.Software_IsExists == 0).OrderBy(s => s.Software_No).ToList();
            StringBuilder str = new StringBuilder();
            str.AppendLine("<table style=\"width:80%; margin-left:9%;margin-top:20px;\" class=\"tab\" border=\"0\" cellpadding=\"10\" cellspacing=\"1\">");
            str.AppendLine("<thead style=\"background-color: #ddd;\">");
            str.AppendLine("<tr style=\"height:30px;\"> <th style=\"width:80px;\">软件名称</th><th style=\"width:268px;\">软件介绍</th><th style=\"width:30px;\">下载连接</th></tr>");
            str.AppendLine("</thead>");
            str.AppendLine("<tbody style=\"background-color: rgba(245, 245, 245, 0.89); text-align:center; font-size:13px;font-family:'Microsoft YaHei';\">");
            foreach (var item in lst)
            {
                Sys_ModelFile file = smbll.Find(f => f.File_OperationID == item.SoftwareID);
                //string json = file.File_Path + "&" + file.File_Name;
                str.AppendLine("<tr><td>" + item.Software_Name + "</td><td style=\"text-align:left;\">" + item.Software_Introduction + "</td><td><a href='/Files/Download.aspx?path=" + file.File_Path + "&filename=" + file.File_Name + "'>下载</a></td></tr>");
            }
            str.AppendLine("</tbody>");
            str.AppendLine("</table>");
            return str.ToString();
        }
    }
}