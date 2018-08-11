using System;

namespace _201808TDD_budget
{
    public class Budget
    {
        public string YearMonth { get; set; }
        public int Amount { get; set; }

        public DateTime LastDay
        {
            get
            {
                var firstDay = FirstDay();
                var daysInMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
                return DateTime.ParseExact(YearMonth + daysInMonth, "yyyyMMdd", null);
            }
        }

        private DateTime FirstDay()
        {
            var firstDay = DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);
            return firstDay;
        }
    }
}