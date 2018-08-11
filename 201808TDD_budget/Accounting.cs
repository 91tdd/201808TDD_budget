using System;
using System.Linq;

namespace _201808TDD_budget
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
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

            return (effectiveEnd - Start).Days + 1;
        }

        private bool HasNoOverlapping(Budget budget)
        {
            return Start > budget.LastDay || End < budget.FirstDay;
        }
    }

    public class Accounting
    {
        private readonly IBudgetRepo _budgetRepo;

        public Accounting(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public decimal TotalAmount(DateTime start, DateTime end)
        {
            var period = new Period(start, end);
            var budgets = _budgetRepo.GetAll();
            if (budgets.Any())
            {
                var budget = budgets[0];
                return period.OverlappingDays(budget);
            }
            return 0;
        }
    }
}