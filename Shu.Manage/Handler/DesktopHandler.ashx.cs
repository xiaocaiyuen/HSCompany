using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using Shu.Model;
using Shu.BLL;
using Shu.Comm;
using System.Web.SessionState;
using Newtonsoft.Json;
using Shu.Utility.Extensions;
using Shu.Utility;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// DesktopHandler 的摘要说明
    /// </summary>
    public class DesktopHandler : BasePage, IHttpHandler, IRequiresSessionState
    {
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            string method = context.Request.QueryString["method"];
            switch (method)
            {
                case "list":
                    BindList(context);
                    break;
                case "info":
                    BindInfo(context);
                    break;
                case "type":
                    DesktopType(context);
                    break;
                case "infolist":
                    BindinfoList(context);
                    break;
                case "listinfo":
                    BindListinfo(context);
                    break;
                case "MySysNotice":
                    MySysNotice(context);
                    break;
                case "MySysMessage":
                    MySysMessage(context);
                    break;
            }
        }

        /// <summary>
        /// 单个列表数据替换
        /// </summary>
        /// <param name="context"></param>
        private void BindListinfo(HttpContext context)
        {

        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <param name="context"></param>
        private void BindinfoList(HttpContext context)
        {
            Sys_DesktopBLL dll = new Sys_DesktopBLL();
            string UserID = CurrUserInfo().UserID;
            List<Sys_Desktop> list_desktop = dll.GetList(p => p.Desktop_Type == "2" && p.Desktop_UserInfoID == UserID).OrderBy(p => p.Desktop_Sort).ToList();
            if (list_desktop.Count > 0)
            {
                //有数据
                StringBuilder strbd = new StringBuilder();
                foreach (var info in list_desktop)
                {
                    strbd.Append(new DesktopList().BindList(info.Desktop_TabType, info.DesktopID, info.Desktop_Name));
                }
                context.Response.Write(strbd);
            }
            else
            {
                //没有数据默认设置三个
                DefaultData(DesktopList.defaultval);

                StringBuilder strbd = new StringBuilder();
                list_desktop = dll.GetList(p => p.Desktop_Type == "2" && p.Desktop_UserInfoID == UserID).OrderBy(p => p.Desktop_Sort).ToList();
                foreach (var info in list_desktop)
                {
                    strbd.Append(new DesktopList().BindList(info.Desktop_TabType, info.DesktopID, info.Desktop_Name));
                }
                context.Response.Write(strbd);
            }
        }

        protected void MySysMessage(HttpContext context)
        {
            try
            {
                StringBuilder strbd = new StringBuilder();
                strbd.Append(new DesktopList().BindList("5", "", ""));
                context.Response.Write(strbd);
            }
            catch { }
        }

        protected void MySysNotice(HttpContext context)
        {
            try
            {
                StringBuilder strbd = new StringBuilder();
                strbd.Append(new DesktopList().BindList("4", "", ""));
                context.Response.Write(strbd);
            }
            catch { }
        }

        /// <summary>
        /// 默认设置三个
        /// </summary>
        private void DefaultData(string dataval)
        {
            StringBuilder strbd = new StringBuilder();
            string[] strval = dataval.Split(',');
            List<combotree> combo = JsonConvert.DeserializeObject<List<combotree>>(DesktopList.type);
            Sys_DesktopBLL bll = new Sys_DesktopBLL();
            List<Sys_Desktop> list_info = new List<Sys_Desktop>();
            int sort = 0;
            string msg = string.Empty;
            foreach (var type in strval)
            {
                combotree info = combo.Where(p => p.id == type).First();
                list_info.Add(new Sys_Desktop { Desktop_AddTime = DateTime.Now, Desktop_Name = info.text, DesktopID = Guid.NewGuid().ToString(), Desktop_Type = "2", Desktop_TabType = info.id, Desktop_UserInfoID = CurrUserInfo().UserID, Desktop_Sort = sort });
                sort++;
            }
            bll.Add(list_info);
        }

        /// <summary>
        /// 获取列表属性数据
        /// </summary>
        /// <param name="context"></param>
        private void DesktopType(HttpContext context)
        {
            context.Response.Write(DesktopList.type);
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="context"></param>
        private void BindInfo(HttpContext context)
        {
            View_Sys_DeskTtop info = new View_Sys_DeskTtop();
            View_Sys_DeskTtopBLL dll = new View_Sys_DeskTtopBLL();
            string userid = CurrUserInfo().UserID;
            string DesktopID = context.Request.QueryString["DesktopID"];
            info = dll.Get(p => p.DesktopID == DesktopID);//("DesktopID = '" + DesktopID + "'");
            StringBuilder strBd = new StringBuilder();
            strBd.Append("<a class=\"quickLink\" href=\"javascript:showTabs('" + info.Desktop_Name + "','" + info.Menu_Url + "','');\">");
            strBd.Append("<img src=\"" + info.Desktop_Icon + "\" /></a>");
            strBd.Append("<p><a class=\"blueLink\" href=\"" + info.Menu_Url + "\">" + info.Desktop_Name.IfNullReturnEmpty().SubstringByByte(12, "…") + "</a></p>");
            strBd.Append("<div class=\"editeMenu\" style=\"position: absolute; display: none; z-index: 2013; bottom: 0px;width: 80px; height: 110px; filter: alpha(opacity=40); background: #ccc; opacity: 0.40;text-align: center; margin-right: auto; margin-left: auto;\"></div>");
            strBd.Append("<div class=\"editeMenu\" style=\"position: absolute; display: none; z-index: 2013; bottom: 40%;left: 10px;\">");
            strBd.Append("<a href=\"javascript:void(0);\" onclick=\"editeMenu('" + info.DesktopID + "')\"><img src=\"../Images/buttons/Bianji.gif\" /></a>");
            strBd.Append("</div>");
            strBd.Append("<div class=\"del\" onclick=\"del('" + info.DesktopID + "')\" style=\"position: absolute; display: none;z-index: 2014; bottom: 92px; left: 62px; cursor: pointer;\"><img src=\"../Scripts/UI/themes/icons/no.png\" /></div>");
            context.Response.Write(strBd);
        }


        /// <summary>
        /// 快捷菜单数据源选项
        /// </summary>
        private void BindList(HttpContext context)
        {
            List<View_Sys_DeskTtop> list = new List<View_Sys_DeskTtop>();
            View_Sys_DeskTtopBLL dll = new View_Sys_DeskTtopBLL();
            string userid = CurrUserInfo().UserID;
            list = dll.GetList(p => p.Desktop_Type == "1" && p.Desktop_UserInfoID == userid).OrderBy(p => p.Desktop_Sort).ToList();
            StringBuilder strBd = new StringBuilder();

            foreach (var info in list)
            {
                strBd.Append("<li id=\"" + info.DesktopID + "\" style=\"position:relative;\"><a class=\"quickLink\" href=\"javascript:showTabs('" + info.Desktop_Name + "','" + info.Menu_Url + "','');\">");
                strBd.Append("<img src=\"" + info.Desktop_Icon + "\" /></a>");
                strBd.Append("<p><a class=\"blueLink\" href=\"javascript:showTabs('" + info.Desktop_Name + "','" + info.Menu_Url + "','');\">" + info.Desktop_Name.IfNullReturnEmpty().SubstringByByte(12, "…") + "</a></p>");
                strBd.Append("<div class=\"editeMenu\" style=\"position: absolute; display: none; z-index: 2013; bottom: 0px;width: 80px; height: 110px; filter: alpha(opacity=40); background: #ccc; opacity: 0.40;text-align: center; margin-right: auto; margin-left: auto;\"></div>");
                strBd.Append("<div class=\"editeMenu\" style=\"position: absolute; display: none; z-index: 2013; bottom: 40%;left: 10px;\">");
                strBd.Append("<a href=\"javascript:void(0);\" onclick=\"editeMenu('" + info.DesktopID + "')\"><img src=\"../Images/buttons/Bianji.gif\" /></a>");
                strBd.Append("</div>");
                strBd.Append("<div class=\"del\" onclick=\"del('" + info.DesktopID + "')\" style=\"position: absolute; display: none;z-index: 2014; bottom: 92px; left: 62px; cursor: pointer;\"><img src=\"../Scripts/UI/themes/icons/no.png\" /></div>");
                strBd.Append("</li>");
            }
            context.Response.Write(strBd);
        }

        public new bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}