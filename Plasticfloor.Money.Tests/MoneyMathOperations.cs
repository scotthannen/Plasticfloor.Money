using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plasticfloor.Money.Tests
{
    [TestClass]
    public class MoneyMathOperations
    {
        [TestMethod]
        public void Addition()
        {
            Assert.AreEqual(Usd.Amount(5).Add(5), Usd.Amount(10));
            Assert.AreEqual(Usd.Amount(5) + Usd.Amount(5), Usd.Amount(10));
            Assert.AreEqual(Usd.Amount(5) + 5, Usd.Amount(10));
        }

        [TestMethod]
        public void Subtraction()
        {
            Assert.AreEqual(Usd.Amount(10).Subtract(5), Usd.Amount(5));
            Assert.AreEqual(Usd.Amount(10) - Usd.Amount(5), Usd.Amount(5));
            Assert.AreEqual(Usd.Amount(10) - 5, Usd.Amount(5));
        }

        [TestMethod]
        public void Multiplication()
        {
            Assert.AreEqual(Usd.Amount(10) *5, Usd.Amount(50));
            Assert.AreEqual(Usd.Amount(10) * 1.2345m, Usd.Amount(12.345m));
        }

        [TestMethod]
        public void Division()
        {
            Assert.AreEqual(Usd.Amount(50) / 5, Usd.Amount(10));
            Assert.AreEqual(Usd.Amount(12.345m) / 10, Usd.Amount(1.2345m));
        }
    }
}