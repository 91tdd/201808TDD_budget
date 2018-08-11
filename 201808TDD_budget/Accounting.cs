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

        public decimal Days()
        {
            return (End - Start).Days + 1;
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
                if (period.Start>budgets[0].LastDay)
                {
                    return 0;
                }
                return period.Days();
            }
            return 0;
        }
    }
}