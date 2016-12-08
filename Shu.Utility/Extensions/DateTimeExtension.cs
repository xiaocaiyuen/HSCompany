/*
Author      : 肖亮
Date        : 2011-4-20
Description : 对 DateTime 的扩展
Modify:
 *  2011-8-25   肖亮  修改方法    WeekOfYear(this DateTime date)
 *                   新增方法     GetfirstDayOfThisYear(this DateTime date)
 *                              GetFirstDayOfThisQuarter(this DateTime date)
 *                              GetFirstDayOfThisMonth(this DateTime date)
 *                              GetFirstDayOfThisWeek(this DateTime date)
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Utility.Extensions
{
    /// <summary>
    /// 对 DateTime 的扩展
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获得本年的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetfirstDayOfThisYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// 获得本季度的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfThisQuarter(this DateTime date)
        {
            return new DateTime(date.Year, ((date.Month - 1) / 3) * 3 + 1, 1);
        }

        /// <summary>
        /// 获得本月的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfThisMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 获得本周的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfThisWeek(this DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
                return date.Date;

            return date.Date.AddDays(-(int)date.DayOfWeek);
        }

        /// <summary>
        /// 获取时间 是一年中的第几个星期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime date)
        {
            var firstDay = new DateTime(date.Year, 1, 1);
            var days = (date - firstDay).Days + (int)firstDay.DayOfWeek;
            return days / 7 + 1;
        }
    }
}
