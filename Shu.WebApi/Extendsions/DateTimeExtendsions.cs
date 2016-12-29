using System;

namespace Shu.WebApi.Extendsions
{
    public static class DateTimeExtendsions
    {
        public static int ToUnixTimeStamp(this DateTime dateTime)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(dateTime - startTime).TotalSeconds;
        }

        public static DateTime ToDateTime(this long unixTimeStamp)
        {
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var toNow = new TimeSpan(unixTimeStamp);
            return dtStart.Add(toNow);
        }

        public static DateTime ToDateTime(this string unixTimeStamp)
        {
            return ToDateTime(long.Parse(unixTimeStamp));
        }
    }
}