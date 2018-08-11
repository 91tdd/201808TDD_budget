using System;

namespace _201808TDD_budget
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new ArgumentException();
            }
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public decimal OverlappingDays(Budget budget)
        {
            if (HasNoOverlapping(budget))
            {
                return 0;
            }

            var effectiveEnd = budget.LastDay < End
                ? budget.LastDay
                : End;

            var effectiveStart = budget.FirstDay > Start
                ? budget.FirstDay
                : Start;

            return (effectiveEnd - effectiveStart).Days + 1;
        }

        private bool HasNoOverlapping(Budget budget)
        {
            return Start > budget.LastDay || End < budget.FirstDay;
        }
    }
}