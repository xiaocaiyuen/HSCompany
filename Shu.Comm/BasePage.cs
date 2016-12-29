using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shu.Model;
using System.Configuration;
using System.Web;
using Shu.Utility.Filters;
using System.Web.Mvc;

namespace Shu.Comm
{
    [SSOAuth]
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            //HttpContext.Current
            SSOAuthAttribute.OnExecuting(HttpContext.Current);

            
            if (CurrUserInfo() == null)
            {
                //Session.Abandon();  //取消当前会话
                //Session.Clear();
                ////System.Web.HttpContext.Current.Response.Redirect(string.Format("/Login.aspx", new object[0]));
                ////MessageBox.ShowAndRedirects(Page, "登陆超时，请重新登陆！", "/UserLogin.aspx");


                //StringBuilder Builder = new StringBuilder();
                //Builder.Append("<script language='javascript' defer>");
                //Builder.AppendFormat("alert('{0}');", "登陆超时，请重新登陆！");
                //Builder.AppendFormat("top.location.href='{0}'", "/Login.aspx");
                //Builder.Append("</script>");
                //Response.Write(Builder.ToString());
                //Response.End();
            }
            base.OnLoad(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack && Request.UrlReferrer != null)
            {
                Session["_url_ref"] = Request.UrlReferrer.PathAndQuery;
            }
        }


        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            if (this.Session["UserInfo"] == null)
            {
                return false;
            }
            return true;
        }

        private static string SESSION_USER = "UserInfo";
        /// <summary>
        /// 获得当前用户信息
        /// </summary>
        /// <returns></returns>
        public SessionUserModel CurrUserInfo()
        {
            //if (this.Session["UserInfo"] != null)
            //{
            //    return this.Session["UserInfo"] as SessionUserModel;
            //}
            //else
            //{
            //    return new SessionUserModel();
            //}
            HttpContext rq = HttpContext.Current;
            return (SessionUserModel)rq.Session[SESSION_USER];
        }

        #region 公用方法
        /// <summary>
        /// 获得页面传递的参数,若该参数不存在，则返回""
        /// </summary>
        /// <param name="strkey"></param>
        /// <returns></returns>
        public string RequstStr(string strkey)
        {
            if (Request.QueryString[strkey] != null)
            {
                return Request.QueryString[strkey].ToString();
            }
            return "";
        }
        #endregion

        #region 肖亮自定义
        /// <summary>
        /// 页面操作返回结果
        /// </summary>
        private JsonResult _result;
        /// <summary>
        /// 页面操作返回结果 Json 字符串
        /// </summary>
        protected virtual JsonResult Result
        {
            get
            {
                if (_result == null)
                    _result = new JsonResult();
                return _result;
            }
            set
            {
                _result = value;
            }
        }
        #endregion
    }
}
