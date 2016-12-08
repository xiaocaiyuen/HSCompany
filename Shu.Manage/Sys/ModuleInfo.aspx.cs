using Newtonsoft.Json.Converters;
using Shu.Utility;
using Shu.Comm;
using Shu.Model;
using Shu.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Shu.Utility.Extensions;

namespace Shu.Manage.Sys
{
    public partial class ModuleInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WebUtil.IsPost())
            {
                Save();
            }
            else
            {
                if (!IsPostBack)
                {
                    ShowBind();
                }

            }
        }

        /// <summary>
        /// 加载绑定页面数据
        /// </summary>
        private void ShowBind()
        {
            string id = Request.QueryString["id"];
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();//这里使用自定义日期格式，默认是ISO8601格式     
            timeConverter.DateTimeFormat = "yyyy-MM-dd";//设置时间格式
            if (!string.IsNullOrEmpty(id))
            {
                string method = WebUtil.Get("method");//复制表单数据（json格式）
                if (method == "get")
                {
                    string jsonPerson2 = string.Empty;
                    string jsonPerson6 = string.Empty;
                    Sys_Module ModuleInfo = new Sys_ModuleBLL().Get(p => p.Id == id);

                    string jsonPerson = JsonConvert.SerializeObject(ModuleInfo, timeConverter);

                    Response.Write(jsonPerson);
                    Response.End();
                }
                else
                {

                }
            }
            else
            {

            }
        }

        /// <summary>
        /// ajax 调用按钮方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[WebMethod]
        public void Save()
        {
            string id = WebUtil.Get("id");//唯一编号
            string ActionDefinitionID = WebUtil.Get("ActionDefinitionID");//按钮动作ID
            string ActionDefinitionName = WebUtil.Get("ActionDefinitionName");//按钮动作名称
            string content= WebUtil.GetForm("Content").HtmlEncode();//防止跨站脚本注入

            bool IsStartWF = WebUtil.Get("IsStartWF", false);//按钮动作名称
            bool IsExists = true;

            Sys_Module ModuleInfo = new Sys_Module();
            Sys_ModuleBLL ModuleBLL = new Sys_ModuleBLL();
            DateTime dt = DateTime.Now;
            if (id.IsNullOrEmpty())
            {
                IsExists = false;
                ModuleInfo = FormToModelHelper<Sys_Module>.ConvertToModel(HttpContext.Current);
                ModuleInfo.Id = Guid.NewGuid().ToString();
                ModuleInfo.AddUserId = CurrUserInfo().UserID;
                ModuleInfo.AddTime = dt;
                //ModuleInfo.EditUserId = CurrUserInfo().UserID;
                //ModuleInfo.EditTime = dt;
                //ModuleInfo.IsDelete = false;
                //ModuleInfo.IsShow = true;
            }
            else
            {
                ModuleInfo = ModuleBLL.Get(p => p.Id == id);
                //复制
                ModuleInfo = FormToModelHelper<Sys_Module>.ConvertToModel(HttpContext.Current, ModuleInfo);
            }


            #region 流程与数据交换方法
            //流程启动正常
            if (IsExists)
            {
                IsExists = new Sys_ModuleBLL().Update(ModuleInfo);
            }
            else
            {
                ModuleInfo = new Sys_ModuleBLL().AddEntity(ModuleInfo);
                IsExists = ModuleInfo.IsNotNull() ? true : false;
            }

            if (IsExists)
            {
                Result.SetData(ModuleInfo);
                Result.SetError(false).SetMsg(ActionDefinitionName + "成功").Output();
            }
            else
            {
                Result.SetError(true).SetMsg("保存出错，请重试").Output();
            }
            #endregion

        }
    }
}