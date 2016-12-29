using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shu.Utility.Filters
{
    public class SSOAuthAttribute : ActionFilterAttribute
    {
        public const string SessionKey = "SessionKey";
        public const string SessionUserName = "SessionUserName";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookieSessionkey = "";
            var cookieSessionUserName = "";

            //SessionKey by QueryString
            if (filterContext.HttpContext.Request.QueryString[SessionKey] != null)
            {
                cookieSessionkey = filterContext.HttpContext.Request.QueryString[SessionKey];
                filterContext.HttpContext.Response.Cookies.Add(new HttpCookie(SessionKey, cookieSessionkey));
            }

            //SessionUserName by QueryString
            if (filterContext.HttpContext.Request.QueryString[SessionUserName] != null)
            {
                cookieSessionUserName = filterContext.HttpContext.Request.QueryString[SessionUserName];
                filterContext.HttpContext.Response.Cookies.Add(new HttpCookie(SessionUserName, cookieSessionUserName));
            }

            //从Cookie读取SessionKey
            if (filterContext.HttpContext.Request.Cookies[SessionKey] != null)
            {
                cookieSessionkey = filterContext.HttpContext.Request.Cookies[SessionKey].Value;
            }

            //从Cookie读取SessionUserName
            if (filterContext.HttpContext.Request.Cookies[SessionUserName] != null)
            {
                cookieSessionUserName = filterContext.HttpContext.Request.Cookies[SessionUserName].Value;
            }

            if (string.IsNullOrEmpty(cookieSessionkey) || string.IsNullOrEmpty(cookieSessionUserName))
            {
                //直接登录
                filterContext.Result = SsoLoginResult(cookieSessionUserName);
            }
            else
            {
                //验证
                if (CheckLogin(cookieSessionkey, filterContext.HttpContext.Request.RawUrl) == false)
                {
                    //会话丢失，跳转到登录页面
                    filterContext.Result = SsoLoginResult(cookieSessionUserName);
                }
            }

            base.OnActionExecuting(filterContext);
        }

        public static void OnExecuting(HttpContext filterContext)
        {
            var cookieSessionkey = "";
            var cookieSessionUserName = "";

            //SessionKey by QueryString
            if (filterContext.Request.QueryString[SessionKey] != null)
            {
                cookieSessionkey = filterContext.Request.QueryString[SessionKey];
                filterContext.Response.Cookies.Add(new HttpCookie(SessionKey, cookieSessionkey));
            }

            //SessionUserName by QueryString
            if (filterContext.Request.QueryString[SessionUserName] != null)
            {
                cookieSessionUserName = filterContext.Request.QueryString[SessionUserName];
                filterContext.Response.Cookies.Add(new HttpCookie(SessionUserName, cookieSessionUserName));
            }

            //从Cookie读取SessionKey
            if (filterContext.Request.Cookies[SessionKey] != null)
            {
                cookieSessionkey = filterContext.Request.Cookies[SessionKey].Value;
            }

            //从Cookie读取SessionUserName
            if (filterContext.Request.Cookies[SessionUserName] != null)
            {
                cookieSessionUserName = filterContext.Request.Cookies[SessionUserName].Value;
            }

            if (string.IsNullOrEmpty(cookieSessionkey) || string.IsNullOrEmpty(cookieSessionUserName))
            {
                //直接登录
                LoginResult(cookieSessionUserName);
            }
            else
            {
                //验证
                if (CheckLogin(cookieSessionkey, filterContext.Request.RawUrl) == false)
                {
                    //会话丢失，跳转到登录页面
                    LoginResult(cookieSessionUserName);
                }
            }
        }

        public static bool CheckLogin(string sessionKey, string remark = "")
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["SSOPassport"])
            };

            var requestUri = string.Format("api/Passport?sessionKey={0}&remark={1}", sessionKey, remark);

            try
            {
                var resp = httpClient.GetAsync(requestUri).Result;

                resp.EnsureSuccessStatusCode();

                return resp.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static SessionCacheItem UserLogin(string sessionKey, string remark = "")
        //{
        //    var httpClient = new HttpClient
        //    {
        //        BaseAddress = new Uri(ConfigurationManager.AppSettings["SSOPassport"])
        //    };

        //    var requestUri = string.Format("api/Passport?sessionKey={0}&remark={1}", sessionKey, remark);

        //    try
        //    {
        //        var resp = httpClient.GetAsync(requestUri).Result;

        //        resp.EnsureSuccessStatusCode();

        //        return new SessionCacheItem
        //        {
        //            StatusCode = resp.StatusCode.ToString(),
        //            ReasonPhrase = response.ReasonPhrase,
        //            Version = response.Version.ToString(),
        //            Content = response.Content.ReadAsStringAsync().Result
        //        };

        //        //return resp.Content.ReadAsAsync<bool>().Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private static ActionResult SsoLoginResult(string username)
        {
            return new RedirectResult(string.Format("{0}/passport?appkey={1}&username={2}",
                    ConfigurationManager.AppSettings["SSOPassport"],
                    ConfigurationManager.AppSettings["SSOAppKey"],
                    username));
        }

        private static void LoginResult(string username)
        {
            HttpContext.Current.Response.Redirect(string.Format("{0}/passport?appkey={1}&username={2}",
                    ConfigurationManager.AppSettings["SSOPassport"],
                    ConfigurationManager.AppSettings["SSOAppKey"],
                    username));
        }
    }
}
