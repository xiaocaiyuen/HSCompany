using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shu.BLL;
using System.Text;
using Shu.Model;
using Shu.Comm;
using Newtonsoft.Json;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// UserInfoHandler 的摘要说明
    /// </summary>
    public class UserInfoHandler : IHttpHandler
    {
        SessionUserModel currentUser = null;
        Sys_UserInfoBLL bll = new Sys_UserInfoBLL();
        List<Sys_UserInfo> entityList = new List<Sys_UserInfo>();
        Sys_PostBLL pobll = new Sys_PostBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            StringBuilder sb = new StringBuilder();
            string parentId = string.Empty;
            if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
            {
                parentId = context.Request.QueryString["id"];
            }
            if (!string.IsNullOrEmpty(parentId))
            {
                Sys_UserInfo entity = bll.Get(p => p.UserInfoID == parentId);
                if (entity != null)
                {
                    string post = "";
                    string postN = "";
                    string name = entity.UserInfo_FullName;
                    string ment = entity.UserInfo_DepCode;
                    Sys_DepartmentBLL mentsbll = new Sys_DepartmentBLL();
                    Sys_Department ments = mentsbll.Get(p => p.Department_Code == ment);
                    post = entity.UserInfo_Post;
                    Sys_Post po = pobll.Get(p => p.PostID == post);
                    if (po!=null)
                    {
                        postN = po.Post_Name;
                    }
                    string mentss = ments.Department_Name;


                    var Data = new { name = name, mentss = mentss, post = post, postN = postN };
                    string jsonPerson = JsonConvert.SerializeObject(Data);
                    context.Response.Write(jsonPerson);

                }
            }
            else
            {
                context.Response.Write("{\"data\":\"error\"}");
            }

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}