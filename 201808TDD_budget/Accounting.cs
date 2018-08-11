using System;
using System.Linq;

namespace _201808TDD_budget
{
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
                var overlappingDays = period.OverlappingDays(budget);
                decimal dailyAmount = budget.Amount / (decimal)budget.DaysInMonth;
                return dailyAmount * overlappingDays;
            }
            return 0;
        }
    }
}