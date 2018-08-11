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

        public decimal OverlappingDays(Period otherPeriod)
        {
            if (HasNoOverlap(otherPeriod))
            {
                return 0;
            }

            var effectiveEnd = otherPeriod.End < End
                ? otherPeriod.End
                : End;

            var effectiveStart = otherPeriod.Start > Start
                ? otherPeriod.Start
                : Start;

            return (effectiveEnd - effectiveStart).Days + 1;
        }

        private bool HasNoOverlap(Period otherPeriod)
        {
            return Start > otherPeriod.End || End < otherPeriod.Start;
        }
    }
}