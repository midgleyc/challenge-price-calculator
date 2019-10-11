using Library.PriceCalculator.Contract;
using Library.PriceCalculator.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Library.PriceCalculator.Tests
{
    [TestClass]
    public class BasketParserTests
    {
        [TestMethod]
        public void NothingPassedIsEmpty()
        {
            var basketParser = new BasketParser(CreateInventory());
            var output = basketParser.ParseBasket(new string[] {});
            Assert.IsFalse(output.Any());
        }

        private IInventory CreateInventory() {
            return new TestInventory();
        }

        private class TestInventory : IInventory
        {
            public decimal GetPriceFor(string itemName)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
