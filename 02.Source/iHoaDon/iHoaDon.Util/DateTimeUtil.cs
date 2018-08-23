using System;
using System.Globalization;
using System.Linq;

namespace iHoaDon.Util
{
    /// <summary>
    /// DateTime utilities
    /// </summary>
    public static class DateTimeUtil
    {
        private static readonly Calendar Cal = CultureInfo.InvariantCulture.Calendar;

        internal static int ParseWeekOfYear(string week, int yearNum, out DateTime start, out DateTime end)
        {
            int result;
            var weeksInYear = GetWeeksOfYear(yearNum);
            if (!Int32.TryParse(week, out result))
            {
                result = DateTime.Now.Year == yearNum ? GetIso8601WeekOfYear(DateTime.Now) : 1;
            }
            else if (result <= 0 || result > weeksInYear)
            {
                result = result <= 0 ? 1 : weeksInYear;
            }
            start = GetFirstMondayOfWeek(yearNum, result, CalendarWeekRule.FirstFourDayWeek);
            end = start.AddDays(7).AddTicks(-1);
            return result;
        }

        internal static int ParseMonthOfYear(string month, int yearNum, out DateTime start, out DateTime end)
        {
            int monthNum;
            if (!Int32.TryParse(month, out monthNum))
            {
                monthNum = yearNum == DateTime.Now.Year ? DateTime.Now.Month : 1;
            }
            else if (monthNum <= 0 || monthNum > 12)
            {
                monthNum = monthNum <= 0 ? 1 : 12;
            }
            GetStartAndEndOfMonth(yearNum, monthNum, out start, out end);
            return monthNum;
        }

        internal static int ParseQuarterOfYear(string quarter, int yearNum, out DateTime start, out DateTime end)
        {
            int quarterNum;
            var ranges = Enumerable.Range(0, 4)
                                            .Select(i => new
                                            {
                                                Left = new DateTime(yearNum, i * 3 + 1, 1),
                                                Right = (i == 3) ? new DateTime(yearNum + 1, 1, 1).AddDays(-1) : new DateTime(yearNum, (i + 1) * 3 + 1, 1, 23, 59, 59, 999).AddDays(-1)
                                            })
                                            .ToArray();
            if (!Int32.TryParse(quarter, out quarterNum))
            {
                quarterNum = yearNum == DateTime.Now.Year 
                                    ? (DateTime.Now.Month / 3) + 1 
                                    : 1;
            }
            else if (quarterNum < 1 || quarterNum > 4)
            {
                quarterNum = (quarterNum < 1) ? 1 : 4;
            }
            start = ranges[quarterNum - 1].Left;
            end = ranges[quarterNum - 1].Right;
            return quarterNum;
        }

        internal static void ParseDateTimePair(string left, string right, out DateTime leftDate, out DateTime rightDate)
        {
            if (DateTime.TryParse(left, out leftDate) && DateTime.TryParse(right, out rightDate))
            {
                if (leftDate > rightDate)
                {
                    Swap(ref leftDate, ref rightDate);
                }
            }
            else
            {
                if (DateTime.TryParse(left, out leftDate))
                {
                    rightDate = leftDate.AddMonths(1);
                }
                else if (DateTime.TryParse(right, out rightDate))
                {
                    leftDate = rightDate.AddMonths(-1);
                }
                else
                {
                    GetStartAndEndOfMonth(DateTime.Now.Year, DateTime.Now.Month, out leftDate, out rightDate);
                }
            }
        }

        private static void GetStartAndEndOfMonth(int year, int month, out DateTime start, out DateTime end)
        {
            var daysInMonth = Cal.GetDaysInMonth(year, month);
            end = new DateTime(year, month, daysInMonth);
            start = new DateTime(year, month, 1);
        }

        private static DateTime GetFirstMondayOfWeek(int year, int weekNum, CalendarWeekRule rule)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            var firstMonday = jan1.AddDays(daysOffset);
            var firstWeek = Cal.GetWeekOfYear(jan1, rule, DayOfWeek.Monday);
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstMonday.AddDays(weekNum * 7);
            return result;
        }

        /// <summary>
        /// Gets the number of weeks in a year.
        /// </summary>
        /// <param name="yearNum">The year num.</param>
        /// <returns></returns>
        public static int GetWeeksOfYear(int yearNum)
        {
            var firstDayOfYear = new DateTime(yearNum, 1, 1);
            var lastDayOfYear = firstDayOfYear.AddDays(Cal.GetDaysInYear(yearNum) - 1);
            var result = GetIso8601WeekOfYear(lastDayOfYear);
            return result == 1 ? 52: result;
        }

        /// <summary>
        /// Gets the ISO8601 week of the year.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = Cal.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return Cal.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private static void Swap<T>(ref T left, ref T right) where T : struct
        {
            var temp = left;
            left = right;
            right = temp;
        }
    }
}
