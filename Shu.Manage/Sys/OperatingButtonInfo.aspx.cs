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
    public partial class OperatingButtonInfo : BasePage
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
                    Sys_OperatingButton ModuleInfo = new Sys_OperatingButtonBLL().Get(p => p.Id == id);

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
            string id = WebUtil.GetQuery("id");//唯一编号
            string IdButton = WebUtil.GetQuery("active");
            string ActionDefinitionID = WebUtil.Get("ActionDefinitionID");//按钮动作ID
            string ActionDefinitionName = WebUtil.Get("ActionDefinitionName");//按钮动作名称
            string content= WebUtil.GetForm("Content").HtmlEncode();//防止跨站脚本注入

            bool IsStartWF = WebUtil.Get("IsStartWF", false);//按钮动作名称
            bool IsExists = true;

            Sys_OperatingButton OperatingButtonInfo = new Sys_OperatingButton();
            Sys_OperatingButtonBLL OperatingButtonBLL = new Sys_OperatingButtonBLL();
            DateTime dt = DateTime.Now;
            if (id.IsNullOrEmpty())
            {
                IsExists = false;
                OperatingButtonInfo = FormToModelHelper<Sys_OperatingButton>.ConvertToModel(HttpContext.Current);
                OperatingButtonInfo.Id = Guid.NewGuid().ToString();
                OperatingButtonInfo.AddUserId = CurrUserInfo().UserID;
                OperatingButtonInfo.AddTime = dt;
                //ModuleInfo.EditUserId = CurrUserInfo().UserID;
                //ModuleInfo.EditTime = dt;
                //ModuleInfo.IsDelete = false;
                //ModuleInfo.IsShow = true;
            }
            else
            {
                OperatingButtonInfo = OperatingButtonBLL.Get(p => p.Id == id);
                //复制
                OperatingButtonInfo = FormToModelHelper<Sys_OperatingButton>.ConvertToModel(HttpContext.Current, OperatingButtonInfo);
            }


            #region 流程与数据交换方法
            //流程启动正常
            if (IsExists)
            {
                IsExists = new Sys_OperatingButtonBLL().Update(OperatingButtonInfo);
            }
            else
            {
                OperatingButtonInfo = new Sys_OperatingButtonBLL().AddEntity(OperatingButtonInfo);
                IsExists = OperatingButtonInfo.IsNotNull() ? true : false;
            }

            if (IsExists)
            {
                Result.SetData(OperatingButtonInfo);
                Result.SetError(false).SetType("3").SetIdButton(IdButton).SetMsg(ActionDefinitionName + "成功").Output();
            }
            else
            {
                Result.SetError(true).SetMsg("保存出错，请重试").Output();
            }
            #endregion

        }
    }
}