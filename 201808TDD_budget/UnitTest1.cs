using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _201808TDD_budget
{
    [TestClass]
    public class AccountingTests
    {
        [TestMethod]
        public void no_budgets()
        {
            var accounting = new Accounting();
            var start = new DateTime(2018, 3, 1);
            var end = new DateTime(2018, 3, 1);
            var totalAmount = accounting.TotalAmount(start, end);
            Assert.AreEqual(0, totalAmount);
        }
    }
}