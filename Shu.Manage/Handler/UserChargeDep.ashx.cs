using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Shu.BLL;
using Shu.Model;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// UserChargeDep 的摘要说明
    /// </summary>
    public class UserChargeDep : IHttpHandler
    {
        public Sys_UserChargeDepBLL bllSys_UserChargeDep = new Sys_UserChargeDepBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string depCodeStr = context.Request.QueryString["depCodeStr"];
            string UserID = context.Request.QueryString["UserID"];
            if (!string.IsNullOrEmpty(depCodeStr) && !string.IsNullOrEmpty(UserID))
            {
                AddFgbm(UserID, depCodeStr);
            }
            context.Response.Write("{\"data\":\"OK\"}");
        }


        /// <summary>
        /// 添加分管部门
        /// </summary>
        public void AddFgbm(string strGuid, string depCodeStr)
        {
            bllSys_UserChargeDep.Delete(p => p.UserChargeDep_ExecutiveOfficerID == strGuid);//" UserChargeDep_ExecutiveOfficerID='" + strGuid + "'"
            if (depCodeStr != "")
            {
                string[] arrStr = depCodeStr.Split(',');
                for (int i = 0; i < arrStr.Length; i++)
                {
                    Sys_UserChargeDep modelFgbm = new Sys_UserChargeDep();
                    modelFgbm.UserChargeDepID = Guid.NewGuid().ToString();
                    modelFgbm.UserChargeDep_ExecutiveOfficerID = strGuid;
                    modelFgbm.UserChargeDep_ChargeCrewID = "";
                    modelFgbm.UserChargeDep_ChargeCrewDepCode = arrStr[i];
                    string errmsg = string.Empty;
                    bllSys_UserChargeDep.Add(modelFgbm);
                }
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