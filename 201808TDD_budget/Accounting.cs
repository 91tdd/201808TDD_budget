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

            return _budgetRepo.GetAll()
                .Sum(b => b.EffectiveAmount(period));
        }
    }
}