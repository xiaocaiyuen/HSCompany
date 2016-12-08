using Shu.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Shu.Manage
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        //void Application_Error(object sender, EventArgs e)
        //{
        //    // 在出现未处理的错误时运行的代码
        //    // 在出现未处理的错误时运行的代码
        //    //在出现未处理的错误时运行的代码
        //    Exception erroy = Server.GetLastError();
        //    string err = "出错页面是：" + Request.Url.ToString() + "</br>";
        //    err += "异常信息：" + erroy.Message + "</br>";
        //    err += "Source:" + erroy.Source + "</br>";
        //    err += "StackTrace:" + erroy.StackTrace + "</br>";

        //    CMMLog.Debug(string.Format("Application_Error执行"));
        //    CMMLog.Error(erroy.Message + "---" + erroy.StackTrace);

        //    //清除前一个异常
        //    Server.ClearError();
        //    //此处理用Session["ProError"]出错。所以用 Application["ProError"]
        //    Application["erroy"] = err;
        //    //此处不是page中，不能用Response.Redirect("../frmSysError.aspx");
        //    System.Web.HttpContext.Current.Response.Redirect("/ApplicationErroy.aspx");

        //}

        //void Session_Start(object sender, EventArgs e)
        //{
        //    // 在新会话启动时运行的代码
        //    Session.Timeout = 60;

        //}

        //void Session_End(object sender, EventArgs e)
        //{
        //    // 在会话结束时运行的代码。 
        //    // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        //    // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        //    // 或 SQLServer，则不会引发该事件。

        //}
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string session_param_name = "ASPSESSID";
        //        string session_cookie_name = "ASP.NET_SessionId";

        //        if (HttpContext.Current.Request.Form[session_param_name] != null)
        //        {
        //            UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
        //        }
        //        else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
        //        {
        //            UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    try
        //    {
        //        string auth_param_name = "AUTHID";
        //        string auth_cookie_name = FormsAuthentication.FormsCookieName;

        //        if (HttpContext.Current.Request.Form[auth_param_name] != null)
        //        {
        //            UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
        //        }
        //        else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
        //        {
        //            UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
        //        }

        //    }
        //    catch
        //    {
        //    }
        //}


        //private void UpdateCookie(string cookie_name, string cookie_value)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
        //    if (null == cookie)
        //    {
        //        cookie = new HttpCookie(cookie_name);
        //    }
        //    cookie.Value = cookie_value;
        //    HttpContext.Current.Request.Cookies.Set(cookie);
        //}
    }
}