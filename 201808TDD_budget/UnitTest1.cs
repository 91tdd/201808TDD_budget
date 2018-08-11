using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _201808TDD_budget
{
    [TestClass]
    public class AccountingTests
    {
        private Accounting _accounting;

        [TestInitialize]
        public void Setup()
        {
            _accounting = new Accounting();
        }

        [TestMethod]
        public void no_budgets()
        {
            AmountShouldBe(0, "20180301", "20180301");
        }

        private void AmountShouldBe(int expected, string start, string end)
        {
            var startDate = DateTime.ParseExact(start, "yyyyMMdd", null);
            var endDate = DateTime.ParseExact(end, "yyyyMMdd", null);
            Assert.AreEqual(expected, _accounting.TotalAmount(startDate, endDate));
        }
    }
}