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
            var budgets = _budgetRepo.GetAll();
            if (budgets.Any())
            {
                return (end - start).Days + 1;
            }
            return 0;
        }
    }
}