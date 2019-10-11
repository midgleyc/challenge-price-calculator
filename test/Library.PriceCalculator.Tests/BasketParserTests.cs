using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public void InventoryNotPassedItemIsAbsent()
        {
            var basketParser = CreateBasketParser();
            var output = basketParser.ParseBasket(new string[] {"Absent"});
            output.Should().BeEmpty();
        }

        [TestMethod]
        public void ParseCanContainDuplicates()
        {
            var basketParser = CreateBasketParser();
            var output = basketParser.ParseBasket(new string[] {"Present", "Present", "Present"});
            output.Count.Should().Be(3);
            var present = output.Last();
            present.Identifier.Should().Be("Present");
            present.Price.Should().Be(11.12m);
        }

        [TestMethod]
        public void ParseContainsAsMuchAsPossible()
        {
            var basketParser = CreateBasketParser();
            var output = basketParser.ParseBasket(new string[] {"Present", "Absent", "Present2", "Absent2"});
            output.Count.Should().Be(2);
            var present = output.Should().Contain(i => i.Identifier == "Present").Subject;
            present.Price.Should().Be(11.12m);
            var present2 = output.Should().Contain(i => i.Identifier == "Present2").Subject;
            present2.Price.Should().Be(23.25m);
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
                {"Present2", 23.25m},
                {"Present3", 37.40m},
            };

            public bool TryGetPriceFor(string itemName, out decimal price)
            {
                return priceMapping.TryGetValue(itemName, out price);
            }
        }
    }
}
