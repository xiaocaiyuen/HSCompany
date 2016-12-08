using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Shu.BLL;
using Shu.Model;
using Shu.Comm;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// ConfigSettingHandler 的摘要说明
    /// </summary>
    public class ConfigSettingHandler : BasePage, IHttpHandler, IRequiresSessionState
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

        /// <summary>
        /// 获取所有系统参数
        /// </summary>
        public void GetAllConfig()
        {
            Sys_SettingBLL bllSetting = new Sys_SettingBLL();
            List<Sys_Setting> listConfig = bllSetting.GetAll().OrderBy(t => t.Sort).ToList();
            Response.Write(new JavaScriptSerializer().Serialize(listConfig));
        }

        /// <summary>
        /// 保存设定项目
        /// </summary>
        public void SaveConfigItemList()
        {
            string msg = string.Empty;
            string json = string.Empty;
            if (!string.IsNullOrEmpty(Request["JSON"]))
            {
                json = Request["JSON"].ToString().Trim();
                List<Sys_Setting> listConfig = new JavaScriptSerializer().Deserialize<List<Sys_Setting>>(json);
                Sys_SettingBLL bllSetting = new Sys_SettingBLL();
                bool flag = bllSetting.Update(listConfig);
                if (flag)
                {
                    msg = "保存成功！";

                    //#region 设置系统全局变量值

                    ////系统是否自动发送短信
                    //Sys_Setting config = listConfig.Find(p => p.Setting_Key == "AutoSendMsg");
                    //SMSHelper.Instance.IsAutoSendSMS = config.Setting_Value == "0" ? false : true;
                    ////发送短信时间起
                    //config = listConfig.Find(p => p.Setting_Key == "SendMsgFromHour");
                    //SMSHelper.Instance.SendMsgFromHour = Convert.ToInt32(config.Setting_Value);
                    ////发送短信时间止
                    //config = listConfig.Find(p => p.Setting_Key == "SendMsgToHour");
                    //SMSHelper.Instance.SendMsgToHour = Convert.ToInt32(config.Setting_Value);
                    ////短信平台账户
                    //config = listConfig.Find(p => p.Setting_Key == "SMSUSERID");
                    //SMSHelper.Instance.SMSUserID = config.Setting_Value;
                    ////短信平台密码
                    //config = listConfig.Find(p => p.Setting_Key == "SMSUSERPWD");
                    //SMSHelper.Instance.SMSUserPwd = config.Setting_Value;

                    //#endregion
                }
                else
                {

                    msg = "保存失败！原因：" + msg;
                }
            }
            Response.Write(msg);
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