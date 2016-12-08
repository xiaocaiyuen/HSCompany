using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Shu.BLL;
using Shu.Model;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// DataDictHandler 的摘要说明
    /// </summary>
    public class DataDictHandler : IHttpHandler, IRequiresSessionState
    {

        HttpRequest Request;
        HttpResponse Response;
        HttpSessionState Session;
        HttpServerUtility Server;
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //不让浏览器缓存
                context.Response.Buffer = true;
                context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                context.Response.AddHeader("pragma", "no-cache");
                context.Response.AddHeader("cache-control", "");
                context.Response.CacheControl = "no-cache";
                context.Response.ContentType = "text/plain";
                Request = context.Request;
                Response = context.Response;
                Session = context.Session;
                Server = context.Server;
                string method = Request["Method"].ToString();
                //反射执行方法
                //反射的方法必须为公共方法即 Public
                MethodInfo methodInfo = this.GetType().GetMethod(method);
                methodInfo.Invoke(this, null);
            }
            catch (Exception ex)
            {

            }
        }

        public void GetDataDictDDL()
        {
            if (!string.IsNullOrEmpty(Request["Code"]))
            {
                string code = Request["Code"].ToString().Trim();
                Sys_DataDictBLL balDataDict = new Sys_DataDictBLL();
                List<Sys_DataDict> listDataDict = balDataDict.GetList(p => p.DataDict_IsDel == false && p.DataDict_ParentCode == code).OrderBy(p => p.DataDict_Sequence).ToList();
                string hashNull = "1";
                if (!string.IsNullOrEmpty(Request["HashNull"]))
                {
                    hashNull = Request["HashNull"];
                }
                if (hashNull.Equals("1"))
                {
                    listDataDict.Insert(0, new Sys_DataDict { DataDict_Name = "--请选择--", DataDict_Code = string.Empty });
                }
                Response.Write(new JavaScriptSerializer().Serialize(listDataDict));
            }
        }

        ///// <summary>
        ///// 上传资料分类
        ///// </summary>
        //public void GetDataType()
        //{
        //    //产品类型（个人|企业）
        //    string type = Request["Type"].ToString();
        //    Sys_UploadDataTypeBLL bll = new Sys_UploadDataTypeBLL();
        //    List<Sys_UploadDataType> listUploadDataType = bll.FindWhere(p => p.UploadDataType_IsDelete == false && p.UploadDataType_ProductType == type).OrderBy(p => p.UploadDataType_SortNo).ToList();
        //    foreach (Sys_UploadDataType uploadDataType in listUploadDataType)
        //    {
        //        uploadDataType.UploadDataType_Name = "【" + uploadDataType.UploadDataType_ProcessStage + "】" + uploadDataType.UploadDataType_Name;
        //    }
        //    listUploadDataType.Insert(0, new Sys_UploadDataType { UploadDataType_Name = "--请选择--", UploadDataType_ID = string.Empty });
        //    Response.Write(new JavaScriptSerializer().Serialize(listUploadDataType));
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