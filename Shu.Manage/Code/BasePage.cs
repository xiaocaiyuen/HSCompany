using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shu.Model;
using System.Configuration;
using System.Web;
using Shu.Utility.Filters;
using System.Web.Mvc;
using Shu.BLL;

namespace Shu.Manage
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            //SSO 单点登录验证
            SSOAuthAttribute.OnExecuting(HttpContext.Current);
            if (CurrUserInfo() == null)
            {
                UserSesson();//重新获取sesson
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
            if (this.Session["UserInfo"] == null)
            {
                SSOAuthAttribute.OnExecuting(HttpContext.Current);
                UserSesson();
            }
            HttpContext rq = HttpContext.Current;
            return (SessionUserModel)rq.Session[SESSION_USER];

        }

        private void UserSesson()
        {
            string userName = HttpContext.Current.Request.Cookies["SessionUserName"].Value;
            View_Sys_UserInfo userInfo = new View_Sys_UserInfoBLL().Get(p => p.UserInfo_LoginUserName == userName);
            SessionUserModel model = new SessionUserModel();
            model.UserID = userInfo.UserInfoID;
            model.UserName = userInfo.UserInfo_FullName;
            model.LoginUserName = userInfo.UserInfo_LoginUserName;
            model.DepartmentName = userInfo.Department_Name;
            model.DepartmentCode = userInfo.UserInfo_DepCode;
            model.PostID = userInfo.UserInfo_Post;
            model.PostName = userInfo.UserInfo_PostName;
            model.RoleID = userInfo.UserInfo_RoleID;
            model.RoleName = userInfo.UserInfo_RoleName;
            model.UserType = userInfo.UserInfo_Type;
            model.DepType = userInfo.Department_Type;
            Session["UserInfo"] = model;
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
        private Shu.Comm.JsonResult _result;
        /// <summary>
        /// 页面操作返回结果 Json 字符串
        /// </summary>
        protected virtual Shu.Comm.JsonResult Result
        {
            get
            {
                if (_result == null)
                    _result = new Shu.Comm.JsonResult();
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
