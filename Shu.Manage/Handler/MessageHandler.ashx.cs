using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Shu.BLL;
using Shu.Comm;
using Shu.Model;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// MessageHandler 的摘要说明
    /// </summary>
    public class MessageHandler : IHttpHandler, IRequiresSessionState
    {
        private StringBuilder sb = new StringBuilder();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request.QueryString["method"];
            switch(method)
            {
                //case "self":
                //  GetUserLoginInfo(context);
                //  break;
                case "delete":
                  DeleteMessage(context);
                  break;
            }
        }

        public void DeleteMessage(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Sys_MessageBLL MessageBLL = new Sys_MessageBLL();
                try
                {
                    Sys_Message Message = MessageBLL.Get(p => p.MessageID == id);
                    if (Message != null)
                    {
                        Message.Message_IsShow = false;
                        string msg = "";
                        MessageBLL.Update(Message);
                    }
                    context.Response.Write("true");
                }
                catch { context.Response.Write("false"); }
            }
        }


        //public void GetUserLoginInfo(HttpContext context)
        //{
        //    SessionUserModel cUser = null;
        //    try
        //    {
        //        cUser = context.Session["UserInfo"] as SessionUserModel;
        //    }
        //    catch { sb.Append(""); }

        //    if (cUser != null)
        //    {
        //        sb.Clear();
        //        View_Sys_MessageBLL MessageBLL = new View_Sys_MessageBLL();
        //        List<View_Sys_Message> MessageList = MessageBLL.FindWhere(p => p.Message_IsShow == true && p.Message_ReceiveUserID == cUser.UserID);
        //        foreach (View_Sys_Message Message in MessageList)
        //        {
        //            sb.Append("<div id='" + Message.MessageID + "'>" + Message.Message_Content + " [" + Convert.ToDateTime(Message.Message_SendTime).ToString("yyyy-MM-dd") + "] " + "<a href=\"javascript:void(0);\" onclick=deleteMessage('" + Message.MessageID + "') style=\"cursor:hand\"><img width=12 height=12 alt='不在提醒' src=\"/Images/Desktop/delMessage.png\" /></a><br /><hr style=\"height:1px;border:none;border-top:1px dashed #0066CC;\" /><br /></div>");
        //        }
        //        context.Response.Write(sb);
        //    }
        //}
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}