using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace _201808TDD_budget
{
    [TestClass]
    public class AccountingTests
    {
        private Accounting _accounting;
        private IBudgetRepo _budgetRepo = Substitute.For<IBudgetRepo>();

        [TestInitialize]
        public void Setup()
        {
            _accounting = new Accounting(_budgetRepo);
        }

        [TestMethod]
        public void no_budgets()
        {
            _budgetRepo.GetAll().Returns(new List<Budget> { });
            AmountShouldBe(0, "20180301", "20180301");
        }

        [TestMethod]
        public void period_inside_budget_month()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new Budget{YearMonth="201803", Amount=31}
            });

            AmountShouldBe(1, "20180301", "20180301");
        }

        private void AmountShouldBe(int expected, string start, string end)
        {
            var startDate = DateTime.ParseExact(start, "yyyyMMdd", null);
            var endDate = DateTime.ParseExact(end, "yyyyMMdd", null);
            Assert.AreEqual(expected, _accounting.TotalAmount(startDate, endDate));
        }
    }
}