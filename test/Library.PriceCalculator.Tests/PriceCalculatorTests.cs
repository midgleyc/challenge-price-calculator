using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Library.PriceCalculator.Calculation;
using Library.PriceCalculator.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.PriceCalculator.Tests
{
    [TestClass]
    public class PriceCalculatorTests
    {
        [TestMethod]
        public void NothingPassedHasNoCost()
        {
            var priceCalculator = CreatePriceCalculatorWithNoOffers();
            var output = priceCalculator.CalculatePrice(new Item[] { });
            output.Total.Should().Be(0.0m);
        }

        [TestMethod]
        public void OneThingPassedHasThatCost()
        {
            var priceCalculator = CreatePriceCalculatorWithNoOffers();
            var output = priceCalculator.CalculatePrice(new Item[] { ItemWithPrice(1.23m) });
            output.SubTotal.Should().Be(1.23m);
        }

        [TestMethod]
        public void ManyThingsPassedSumsCost()
        {
            var priceCalculator = CreatePriceCalculatorWithNoOffers();
            var output = priceCalculator.CalculatePrice(new Item[] { ItemWithPrice(1.23m), ItemWithPrice(2.34m), ItemWithPrice(3.45m, "Other")});
            output.SubTotal.Should().Be(7.02m);
        }

        private IOffers CreateOffers()
        {
            return new TestOffers();
        }

        private Pricing CreatePriceCalculatorWithNoOffers()
        {
            return new Pricing(CreateOffers());
        }

        private static Item ItemWithPrice(decimal price, string id = "Test")
        {
            return new Item("Test") { Price = price };
        }

        private class TestOffers : IOffers
        {
        }
    }
}
