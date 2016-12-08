using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Shu.Comm
{
   public class DateTimeUtil
    {
       public static string StartYear = ConfigurationManager.AppSettings["minYear"].ToString();

       /// <summary>
       /// 获得中文日期
       /// </summary>
       /// <returns></returns>
       public static string getCurrDataToChinese()
       {
            DateTime time=DateTime.Now;
            return string.Format("{0:D}", time);
       }

       /// <summary>获得星期几</summary>
       /// <returns>星期几，1代表星期一；7代表星期日</returns>
       public static string getWeekDay()
       {
           string week = "";
           string dt = DateTime.Today.DayOfWeek.ToString();
           //根据取得的星期英文单词返回汉字 
           switch (dt)
           {
               case "Monday":
                   week = "星期一";
                   break;
               case "Tuesday":
                   week = "星期二";
                   break;
               case "Wednesday":
                   week = "星期三";
                   break;
               case "Thursday":
                   week = "星期四";
                   break;
               case "Friday":
                   week = "星期五";
                   break;
               case "Saturday":
                   week = "星期六";
                   break;
               case "Sunday":
                   week = "星期日";
                   break;
           }
           return week;
       }



       public static DateTime? ToDateTime(string time)
       {
           DateTime? date = null;

           if (!time.Equals(""))
           {
               try
               {
                   date = DateTime.Parse(time);
               }
               catch
               {
                   return null;
               }
           }

           return date;
       }

       public static string ToDateTime(DateTime? time)
       {
           string  date = "";

           if (!time.Equals(""))
           {
               try
               {
                   date = DateTime.Parse(time.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
               }
               catch
               {
                   return null;
               }
           }

           return date;
       }

       /// <summary>
       /// 获得一天最大时间
       /// </summary>
       /// <param name="time"></param>
       /// <returns></returns>
       public static string GetMaxDateToString(string time)
       {
          

           if (!time.Equals(""))
           {
               if (time.Contains(" "))
               {
                   string[] times = time.Split(' ');

                   return times[0] + " " + "23:59:59";
               }

               else if (time.Contains("年")&& time.Contains("月")&&time.Contains("日"))
               {
                   try
                   {
                       string date = Convert.ToDateTime(time) + "";

                       return date.Split(' ')[0] + " " + "23:59:59";
                   }
                   catch
                   {
                       return time;
                   }
               }
           }

           return time;
       }


       /// <summary>
       /// 设置年份下拉框
       /// </summary>
       /// <param name="ddl"></param>
       public static void SetYearDropDownList(DropDownList ddl)
       {

           int minYear = int.Parse(StartYear);

           int maxYear = DateTime.Now.Year;

           //ddl.Items.Clear();

           for (int i = minYear; i <= maxYear; i++)
           {
               ListItem item = new ListItem("" + i.ToString() + "年", i.ToString());

               if (i == maxYear)
               {
                   item.Selected = true;
               }

               ddl.Items.Add(item);
           }
       }

       /// <summary>
       /// 设置年份下拉框
       /// </summary>
       /// <param name="ddl"></param>
       public static List<ListItem> SetYearDropDownList()
       {
           List<ListItem> ddl = new List<ListItem>();
           ddl.Add(new ListItem("※※请选择※※", "0"));
           int minYear = int.Parse(StartYear);

           int maxYear = DateTime.Now.Year;

           //ddl.Items.Clear();

           for (int i = minYear; i <= maxYear; i++)
           {
               ListItem item = new ListItem("" + i.ToString() + "年", i.ToString());

               if (i == maxYear)
               {
                   item.Selected = true;
               }
               ddl.Add(item);
           }
           return ddl;
       }


       /// <summary>
       /// 设置年份下拉框
       /// </summary>
       /// <param name="ddl"></param>
       public static void SetYearDropDownListNoSelected(DropDownList ddl)
       {

           int minYear = int.Parse(StartYear);

           int maxYear = DateTime.Now.Year;

           ddl.Items.Clear();

           for (int i = minYear; i <= maxYear; i++)
           {
               ListItem item = new ListItem("" + i.ToString() + "年", i.ToString());
               ddl.Items.Add(item);
           }
       }


       /// <summary>
       /// 设置月份下拉框
       /// </summary>
       /// <param name="ddl"></param>
       public static void SetMonthDropDownList(DropDownList ddl)
       {
           int currMonth = DateTime.Now.Month;

           ddl.Items.Clear();

           ddl.Items.Insert(0, new ListItem("请选择月份", "0"));

           for (int i = 1; i <= 12; i++)
           {
               ListItem item = new ListItem("" + i.ToString() + "月份", i.ToString());

               //if (currMonth == i)
               //{
               //    item.Selected = true;
               //}
               ddl.Items.Add(item);
           }
       }



       /// <summary>
       /// 设置季度
       /// </summary>
       /// <param name="ddl"></param>
       public static void SetQuarterDropDownListNoSelected(DropDownList ddl)
       {
           ddl.Items.Add(new ListItem(Constant.QuarterName_One, Constant.Quarter_Number1));
           ddl.Items.Add(new ListItem(Constant.QuarterName_Two, Constant.Quarter_Number2));
           ddl.Items.Add(new ListItem(Constant.QuarterName_Three, Constant.Quarter_Number3));
           ddl.Items.Add(new ListItem(Constant.QuarterName_Four, Constant.Quarter_Number4));
       }


       /// <summary>
       /// 设置月份下拉框
       /// </summary>
       /// <param name="ddl"></param>
       public static void SetQuarterDropDownList(DropDownList ddl)
       {
           int currMonth = DateTime.Now.Month;
           int currQuarter = 0;
           if ((currMonth == 1) || (currMonth == 2) || (currMonth == 3))
           {
               currQuarter = 1;
           }
           if ((currMonth == 4) || (currMonth == 5) || (currMonth == 6))
           {
               currQuarter = 2;
           }
           if ((currMonth == 7) || (currMonth == 8) || (currMonth == 9))
           {
               currQuarter = 3;
           }
           if ((currMonth == 10) || (currMonth == 12) || (currMonth == 11))
           {
               currQuarter = 4;
           }
           ddl.Items.Clear();

           ddl.Items.Insert(0, new ListItem("请选择季度", "0"));
           ddl.Items.Insert(1, new ListItem("第1季度", "1"));
           ddl.Items.Insert(2, new ListItem("第2季度", "2"));
           ddl.Items.Insert(3, new ListItem("第3季度", "3"));
           ddl.Items.Insert(4, new ListItem("第4季度", "4"));

           for (int i = 0; i < ddl.Items.Count; i++)
           {
               ListItem itm = ddl.Items[i];

               int value = int.Parse(itm.Value);

               if (currQuarter == value)
               {
                   ddl.Items[i].Selected = true;
               }

           }
       }

       /// <summary>
       /// 设置月份下拉框
       /// </summary>
       /// <param name="ddl"></param>
       public static void SetMonthDropDownListNoSelected(DropDownList ddl)
       {
           int currMonth = DateTime.Now.Month;

           ddl.Items.Clear();

           ddl.Items.Insert(0, new ListItem("请选择月份", "0"));

           for (int i = 1; i <= 12; i++)
           {
               ListItem item = new ListItem("" + i.ToString() + "月份", i.ToString());
               ddl.Items.Add(item);
           }
       }

       /// <summary>
       /// 10位数int时间戳单位换算为datetime 
       /// </summary>
       /// <param name="time"></param>
       /// <returns></returns>
       public static DateTime IntToDatetime(long time)
       {
           Int64 begtime = Convert.ToInt64(time) * 10000000;//100毫微秒为单位,time为需要转化的int日期
           DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
           long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
           long time_tricks = tricks_1970 + begtime;//日志日期刻度
           DateTime dt = new DateTime(time_tricks);//转化为DateTime
           return dt;
       }
    }
}
