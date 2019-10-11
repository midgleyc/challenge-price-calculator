using System;
using System.Collections.Generic;
using FluentAssertions;
using Library.PriceCalculator.Contract;
using Library.PriceCalculator.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.PriceCalculator.Tests
{
    [TestClass]
    public class BasketParserTests
    {
        [TestMethod]
        public void NothingPassedIsEmpty()
        {
            var basketParser = CreateBasketParser();
            var output = basketParser.ParseBasket(new string[] {});
            output.Should().BeEmpty();
        }

        [TestMethod]
        public void InventoryPassedItemIsPresent()
        {
            var basketParser = CreateBasketParser();
            var output = basketParser.ParseBasket(new string[] {"Present"});
            var present = output.Should().ContainSingle().Subject;
            present.Identifier.Should().Be("Present");
            present.Price.Should().Be(11.12m);
        }

        private IInventory CreateInventory() {
            return new TestInventory();
        }

        private BasketParser CreateBasketParser() {
            return new BasketParser(CreateInventory());
        }

        private class TestInventory : IInventory
        {
            private readonly Dictionary<string, decimal> priceMapping = new Dictionary<string, decimal> {
                {"Present", 11.12m},
            };

            public bool TryGetPriceFor(string itemName, out decimal price)
            {
                return priceMapping.TryGetValue(itemName, out price);
            }
        }
    }
}
