using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.ExtensionMethods
{
    public static class TimeZoneExtensionMethods
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;
        public static string ToPrettyDate(this DateTime date)
        {


            //var ts = new TimeSpan(DateTime.UtcNow.Ticks - date.Ticks);
            //double delta = Math.Abs(ts.TotalSeconds);

            //if (delta < 1 * MINUTE)
            //{
            //    return ts.Seconds == 1 ? "onesecondago".Localize() : ts.Seconds + "secondsAgo".Localize();
            //}
            //if (delta < 2 * MINUTE)
            //{
            //    return "aminuteago".Localize();
            //}
            //if (delta < 45 * MINUTE)
            //{
            //    return ts.Minutes + "minutesago".Localize();
            //}
            //if (delta < 90 * MINUTE)
            //{
            //    return "anhourago".Localize();
            //}
            //if (delta < 24 * HOUR)
            //{
            //    return ts.Hours + "hoursago".Localize();
            //}
            //if (delta < 48 * HOUR)
            //{
            //    return "yesterday".Localize();
            //}
            //if (delta < 30 * DAY)
            //{
            //    return ts.Days + "daysago".Localize();
            //}
            //if (delta < 12 * MONTH)
            //{
            //    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
            //    return months <= 1 ? "onemonthago".Localize() : months + "monthsago".Localize();
            //}
            //else
            //{
            //    int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            //    return years <= 1 ? "oneyearago".Localize() : years + "yearsago".Localize();
            //}
            return "";
        }
    }
}
