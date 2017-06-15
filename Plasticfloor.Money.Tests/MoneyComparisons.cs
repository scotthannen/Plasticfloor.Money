using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plasticfloor.Money.Tests
{
    [TestClass]
    public class MoneyComparisons
    {
        [TestMethod]
        [ExpectedException(typeof(CurrencyMismatchException))]
        public void ComparisonThrowsExceptionForCurrencyMismatch()
        {
            var m1 = new Money(5, Currency.USD);
            var m2 = new Money(4, Currency.CAD);
            var comparison = m1.CompareTo(m2);
        }

        [TestMethod]
        public void MoneyComparesToMoneyCorrectly()
        {
            var m1 = new Money(5, Currency.USD);
            var m2 = new Money(4, Currency.USD);
            Assert.IsTrue(m1.CompareTo(m2) == m1.Amount.CompareTo(m2.Amount));
        }

        [TestMethod]
        public void MoneyToMoneyGreaterThan()
        {
            Assert.IsTrue(Usd.Amount(5) > Usd.Amount(4));
            Assert.IsFalse(Usd.Amount(4) > Usd.Amount(5));
            Assert.IsFalse(Usd.Amount(4) > Usd.Amount(4));
        }

        [TestMethod]
        public void MoneyToMoneyLessThan()
        {
            Assert.IsTrue(Usd.Amount(3) < Usd.Amount(4));
            Assert.IsFalse(Usd.Amount(6) < Usd.Amount(5));
            Assert.IsFalse(Usd.Amount(4) < Usd.Amount(4));
        }

        [TestMethod]
        public void MoneyToMoneyGreaterThanOrEqual()
        {
            Assert.IsTrue(Usd.Amount(5) >= Usd.Amount(4));
            Assert.IsFalse(Usd.Amount(4) >= Usd.Amount(5));
            Assert.IsTrue(Usd.Amount(4) >= Usd.Amount(4));
        }

        [TestMethod]
        public void MoneyToMoneyLessThanOrEqual()
        {
            Assert.IsTrue(Usd.Amount(3) <= Usd.Amount(4));
            Assert.IsFalse(Usd.Amount(6) <= Usd.Amount(5));
            Assert.IsTrue(Usd.Amount(4) <= Usd.Amount(4));
        }

        [TestMethod]
        public void MoneyToDecimalGreaterThan()
        {
            Assert.IsTrue(Usd.Amount(5) > 4);
            Assert.IsFalse(Usd.Amount(4) > 5);
            Assert.IsFalse(Usd.Amount(4) > 4);
        }

        [TestMethod]
        public void MoneyToDecimalLessThan()
        {
            Assert.IsTrue(Usd.Amount(3) < 4);
            Assert.IsFalse(Usd.Amount(6) < 5);
            Assert.IsFalse(Usd.Amount(4) < 4);
        }

        [TestMethod]
        public void MoneyToDecimalIsGreaterThanOrEqual()
        {
            Assert.IsTrue(Usd.Amount(5) >= 4);
            Assert.IsFalse(Usd.Amount(4) >= 5);
            Assert.IsTrue(Usd.Amount(4) >= 4);
        }

        [TestMethod]
        public void MoneyToDecimalIsLessThanOrEqual()
        {
            Assert.IsTrue(Usd.Amount(3) <= 4);
            Assert.IsFalse(Usd.Amount(6) <= 5);
            Assert.IsTrue(Usd.Amount(4) <= 4);
        }

        [TestMethod]
        public void Equality()
        {
            Assert.IsTrue(Usd.Amount(3) == Usd.Amount(3));
            Assert.IsFalse(Usd.Amount(3) == Usd.Amount(3.01m));
        }

        [TestMethod]
        public void Inequality()
        {
            Assert.IsTrue(Usd.Amount(3) != Usd.Amount(3.01m));
            Assert.IsFalse(Usd.Amount(3) != Usd.Amount(3));
        }
    }
}
