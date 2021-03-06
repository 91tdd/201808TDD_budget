﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;

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
            GivenBudgets();
            AmountShouldBe(0, "20180301", "20180301");
        }

        [TestMethod]
        public void period_inside_budget_month()
        {
            GivenBudgets(new Budget { YearMonth = "201803", Amount = 31 });
            AmountShouldBe(1, "20180301", "20180301");
        }

        [TestMethod]
        public void period_no_overlap_budget_LastDay()
        {
            GivenBudgets(new Budget { YearMonth = "201803", Amount = 31 });
            AmountShouldBe(0, "20180401", "20180401");
        }

        [TestMethod]
        public void period_no_overlap_budget_FirstDay()
        {
            GivenBudgets(new Budget { YearMonth = "201803", Amount = 31 });
            AmountShouldBe(0, "20180201", "20180201");
        }

        [TestMethod]
        public void period_overlap_budget_LastDay()
        {
            GivenBudgets(new Budget { YearMonth = "201803", Amount = 31 });
            AmountShouldBe(expected: 1, start: "20180331", end: "20180401");
        }

        [TestMethod]
        public void period_overlap_budget_FirstDay()
        {
            GivenBudgets(new Budget { YearMonth = "201803", Amount = 31 });
            AmountShouldBe(expected: 1, start: "20180228", end: "20180301");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void invalid_date()
        {
            GivenBudgets(new Budget { YearMonth = "201803", Amount = 31 });
            _accounting.TotalAmount(DateTime.ParseExact("20180228", "yyyyMMdd", null), DateTime.ParseExact("20170301", "yyyyMMdd", null));
        }

        [TestMethod]
        public void daily_amount()
        {
            GivenBudgets(new Budget { YearMonth = "201803", Amount = 310 });
            AmountShouldBe(expected: 20, start: "20180301", end: "20180302");
        }

        [TestMethod]
        public void multiple_budgets()
        {
            GivenBudgets(
                new Budget { YearMonth = "201803", Amount = 310 },
                new Budget { YearMonth = "201804", Amount = 30 });

            AmountShouldBe(expected: 11, start: "20180331", end: "20180401");
        }

        private void GivenBudgets(params Budget[] budgets)
        {
            _budgetRepo.GetAll().Returns(budgets.ToList());
        }

        private void AmountShouldBe(int expected, string start, string end)
        {
            var startDate = DateTime.ParseExact(start, "yyyyMMdd", null);
            var endDate = DateTime.ParseExact(end, "yyyyMMdd", null);
            Assert.AreEqual(expected, _accounting.TotalAmount(startDate, endDate));
        }
    }
}