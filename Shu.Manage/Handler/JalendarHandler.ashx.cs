using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shu.Model;
using Shu.Comm;
using System.Text;
using Shu.BLL;
using System.Web.SessionState;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// DepartmentHandler 的摘要说明
    /// </summary>
    public class JalendarHandler : BasePage, IHttpHandler, IRequiresSessionState
    {
        public SessionUserModel CurrUserInfo = new SessionUserModel();
        Sys_JalendarBLL jalendarBLL = new Sys_JalendarBLL();
        Sys_HolidayBLL holidayBLL = new Sys_HolidayBLL();

        Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
        Sys_UserInfoBLL bllUser = new Sys_UserInfoBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            context.Response.Cache.SetNoStore();

            string method = context.Request.QueryString["method"];
            if (method == "loadHolidy")
            {
                context.Response.Write(LoadHolidy());
            }
            if (method == "load")
            {
                context.Response.Write(LoadData(context));
            }
            if (method == "add")
            {
                context.Response.Write(Add(context));
            }

            if (method == "delete")
            {
                context.Response.Write(Delete(context));
            }

            if (method == "show")
            {
                context.Response.Write(ShowInfo(context));
            }

            if (method == "modify")
            {
                context.Response.Write(Modify(context));
            }

        }


        #region 加载Holidy

        public string LoadHolidy()
        {
            List<Sys_Holiday> list = null;
            CurrUserInfo = new BasePage().CurrUserInfo();
            list = holidayBLL.GetAll().OrderBy(p => p.Holiday_UpdateTime).ToList();
            //节假日管理中未考虑 Holiday_IsDelete 字段值，因此此处也不考虑
            //list = holidayBLL.FindWhere(p=>p.Holiday_IsDelete ==false).OrderBy(p => p.Holiday_UpdateTime).ToList();

            StringBuilder strData = new StringBuilder();

            int index = 0;

            foreach (Sys_Holiday obj in list)
            {
                //事件开始时间
                string sStartTime = Convert.ToDateTime(obj.Holiday_StartDate).ToString("yyyy-MM-dd HH:mm");
                //时间结束时间
                string sEndTime = Convert.ToDateTime(obj.Holiday_EndDate).ToString("yyyy-MM-dd HH:mm");

                string title = obj.Holiday_Name;
                if (title.Length > 29)
                {
                    title = title.Substring(0, 23) + "......";
                }

                string day = Convert.ToDateTime(obj.Holiday_StartDate).ToString("yyyy-MM-dd");

                DateTime sDate = Convert.ToDateTime(Convert.ToDateTime(obj.Holiday_StartDate).ToString("yyyy-MM-dd"));
                DateTime eDate = Convert.ToDateTime(Convert.ToDateTime(obj.Holiday_EndDate).ToString("yyyy-MM-dd"));
                int interval = new TimeSpan(eDate.Ticks - sDate.Ticks).Days;

                if (interval > 0)
                {
                    for (int i = 0; i < interval; i++)
                    {
                        DateTime dateTemp = sDate.AddDays(i);
                        string dayTemp = Convert.ToDateTime(dateTemp).ToString("yyyy-MM-dd");
                        strData.Append("<div class=\"holidy\" data-date=\"" + dayTemp + "\" data-stime=\"" + sStartTime + "\" data-etime=\"" + sEndTime + "\"data-title=\"" + title + "\"data-sid=\"" + obj.HolidayId + "\"></div>");
                    }

                    string end = Convert.ToDateTime(obj.Holiday_EndDate).ToString("HH:mm");
                    if (end != "00:00")
                    {
                        DateTime dateTemp = sDate.AddDays(interval);
                        string dayTemp = Convert.ToDateTime(dateTemp).ToString("yyyy-MM-dd");
                        strData.Append("<div class=\"holidy\" data-date=\"" + dayTemp + "\" data-stime=\"" + sStartTime + "\" data-etime=\"" + sEndTime + "\"data-title=\"" + title + "\"data-sid=\"" + obj.HolidayId + "\"></div>");
                    }
                }
                else
                {
                    strData.Append("<div class=\"holidy\" data-date=\"" + day + "\" data-stime=\"" + sStartTime + "\" data-etime=\"" + sEndTime + "\"data-title=\"" + title + "\"data-sid=\"" + obj.HolidayId + "\"></div>");
                }

                index++;
            }

            return strData.ToString();
        }

        #endregion

        #region 加载
        /// <summary>
        /// 加载菜单树
        /// </summary>
        /// <returns></returns>
        public string LoadData(HttpContext context)
        {
            string time = context.Request.QueryString["jTime"];

            List<Sys_Jalendar> list = null;
            CurrUserInfo = new BasePage().CurrUserInfo();
            list = jalendarBLL.GetList(p => p.Jalendar_AddUserID == CurrUserInfo.UserID).OrderBy(p => p.Jalendar_AddTime).ToList();

            StringBuilder strData = new StringBuilder();

            int index = 0;

            foreach (Sys_Jalendar obj in list)
            {
                //事件开始时间
                string sStartTime = Convert.ToDateTime(obj.Jalendar_StartDate).ToString("yyyy-MM-dd HH:mm");
                //时间结束时间
                string sEndTime = Convert.ToDateTime(obj.Jalendar_EndDate).ToString("yyyy-MM-dd HH:mm");

                string title = obj.Jalendar_Event;
                if (title.Length > 39)
                {
                    title = title.Substring(0, 33) + "......";
                }
                string eventStatus = string.Empty;
                if (!string.IsNullOrEmpty(obj.Jalendar_EventStatus) && obj.Jalendar_EventStatus == "1")
                {
                    eventStatus = "已完成";
                }

                string day = Convert.ToDateTime(obj.Jalendar_StartDate).ToString("yyyy-MM-dd");

                DateTime sDate = Convert.ToDateTime(Convert.ToDateTime(obj.Jalendar_StartDate).ToString("yyyy-MM-dd"));
                DateTime eDate = Convert.ToDateTime(Convert.ToDateTime(obj.Jalendar_EndDate).ToString("yyyy-MM-dd"));
                int interval = new TimeSpan(eDate.Ticks - sDate.Ticks).Days;

                if (interval > 0)
                {
                    for (int i = 0; i < interval; i++)
                    {
                        DateTime dateTemp = sDate.AddDays(i);
                        string dayTemp = Convert.ToDateTime(dateTemp).ToString("yyyy-MM-dd");
                        strData.Append("<div class=\"added-event\" data-date=\"" + dayTemp + "\" data-stime=\"" + sStartTime + "\" data-etime=\"" + sEndTime + "\"data-title=\"" + title + "\"data-sid=\"" + obj.JalendarID + "\"data-eventStatus=\"" + eventStatus + "\"></div>");
                    }

                    string end = Convert.ToDateTime(obj.Jalendar_EndDate).ToString("HH:mm");

                    if (end != "00:00")
                    {
                        DateTime dateTemp = sDate.AddDays(interval);
                        string dayTemp = Convert.ToDateTime(dateTemp).ToString("yyyy-MM-dd");
                        strData.Append("<div class=\"added-event\" data-date=\"" + dayTemp + "\" data-stime=\"" + sStartTime + "\" data-etime=\"" + sEndTime + "\"data-title=\"" + title + "\"data-sid=\"" + obj.JalendarID + "\"data-eventStatus=\"" + eventStatus + "\"></div>");
                    }
                }
                else
                {
                    strData.Append("<div class=\"added-event\" data-date=\"" + day + "\" data-stime=\"" + sStartTime + "\" data-etime=\"" + sEndTime + "\"data-title=\"" + title + "\"data-sid=\"" + obj.JalendarID + "\"data-eventStatus=\"" + eventStatus + "\"></div>");
                }

                index++;
            }

            return strData.ToString();
        }

        #endregion

        #region 新增

        /// <summary>
        /// 新增
        /// </summary>
        public string Add(HttpContext context)
        {
            string jalendar_Event = context.Request.QueryString["jEvent"];
            string jalendar_StartDate = context.Request.QueryString["jStartDate"];
            string jalendar_EndDate = context.Request.QueryString["jEndDate"];
            string jalendar_Type = context.Request.QueryString["jType"];
            string jalendar_EventStatus = context.Request.QueryString["jEventStatus"];

            //日历备忘录
            Sys_Jalendar jalendar = new Sys_Jalendar();
            jalendar.JalendarID = Guid.NewGuid().ToString();
            jalendar.Jalendar_Event = jalendar_Event;
            jalendar.Jalendar_StartDate = Convert.ToDateTime(jalendar_StartDate);
            jalendar.Jalendar_EndDate = Convert.ToDateTime(jalendar_EndDate);
            jalendar.Jalendar_Type = jalendar_Type;
            jalendar.Jalendar_EventStatus = jalendar_EventStatus;
            jalendar.Jalendar_AddUserID = base.CurrUserInfo().UserID;
            jalendar.Jalendar_AddTime = DateTime.Now;
            bool ab1 = jalendarBLL.Add(jalendar);

            if (ab1)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        #endregion

        #region 删除

        public string Delete(HttpContext context)
        {
            string id = context.Request.QueryString["pcode"];

            bool rtn = jalendarBLL.Delete(p => p.JalendarID == id);

            if (rtn)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        #endregion

        #region 修改

        public string ShowInfo(HttpContext context)
        {
            string id = context.Request.QueryString["code"];
            Sys_Jalendar obj = jalendarBLL.Get(p => p.JalendarID == id);
            if (obj != null)
            {
                string StartDateSD = Convert.ToDateTime(obj.Jalendar_StartDate).ToString("yyyy-MM-dd");
                string EndDateSD = Convert.ToDateTime(obj.Jalendar_EndDate).ToString("yyyy-MM-dd");

                string json = "{\"JalendarID\":\"" + obj.JalendarID + "\",\"Jalendar_AddTime\":\"" + obj.Jalendar_AddTime + "\",\"Jalendar_AddUserID\":\"" + obj.Jalendar_AddUserID + "\",\"Jalendar_EndDate\":\"" + obj.Jalendar_EndDate + "\",\"Jalendar_Event\":\"" + obj.Jalendar_Event + "\",\"Jalendar_EventStatus\":\"" + obj.Jalendar_EventStatus + "\",\"Jalendar_StartDate\":\"" + obj.Jalendar_StartDate + "\",\"Jalendar_Type\":\"" + obj.Jalendar_Type + "\",\"Jalendar_StartDateSD\":\"" + StartDateSD + "\",\"Jalendar_EndDateSD\":\"" + EndDateSD + "\"}";

                //string json = JosnHandler.GetJson<Sys_Jalendar>(obj);

                return json;
            }
            else
            {
                return "0";
            }
        }

        public string Modify(HttpContext context)
        {
            string jalendarID = context.Request.QueryString["ID"];
            string jalendar_Event = context.Request.QueryString["jEvent"];
            string jalendar_StartDate = context.Request.QueryString["jStartDate"];
            string jalendar_EndDate = context.Request.QueryString["jEndDate"];
            string jalendar_Type = context.Request.QueryString["jType"];
            string jalendar_EventStatus = context.Request.QueryString["jEventStatus"];
            Sys_Jalendar obj = jalendarBLL.Get(p => p.JalendarID == jalendarID);
            if (obj == null)
            {
                return "0";
            }
            else
            {
                //日历备忘录
                Sys_Jalendar jalendar = new Sys_Jalendar();
                jalendar.JalendarID = jalendarID;
                jalendar.Jalendar_Event = jalendar_Event;
                jalendar.Jalendar_StartDate = Convert.ToDateTime(jalendar_StartDate);
                jalendar.Jalendar_EndDate = Convert.ToDateTime(jalendar_EndDate);
                jalendar.Jalendar_Type = jalendar_Type;
                jalendar.Jalendar_EventStatus = jalendar_EventStatus;
                jalendar.Jalendar_AddUserID = base.CurrUserInfo().UserID;
                jalendar.Jalendar_AddTime = DateTime.Now;
                bool ab1 = jalendarBLL.Update(jalendar);
                if (ab1)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}