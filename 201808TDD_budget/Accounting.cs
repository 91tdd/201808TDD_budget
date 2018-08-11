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
            var totalAmount = 0m;
            foreach (var budget in budgets)
            {
                totalAmount += budget.EffectiveAmount(period);
            }

            return totalAmount;
        }
    }
}