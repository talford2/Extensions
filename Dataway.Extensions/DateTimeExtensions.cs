using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataway.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static DateTime GetNextWeekDay(this DateTime dateTime, DayOfWeek weekDay)
        {
            var cur = dateTime.Date.AddDays(1);
            while (cur.DayOfWeek != weekDay)
            {
                cur = cur.AddDays(1);
            }
            return cur;
        }

        public static DateTime GetPreviousWeekDay(this DateTime dateTime, DayOfWeek weekDay)
        {
            var cur = dateTime.Date.AddDays(-1);
            while (cur.DayOfWeek != weekDay)
            {
                cur = cur.AddDays(-1);
            }
            return cur;
        }
    }
}
