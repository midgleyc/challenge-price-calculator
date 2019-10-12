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
            var priceCalculator = CreatePriceCalculator();
            var output = priceCalculator.CalculatePrice(new Item[] {});
            output.Total.Should().Be(0.0m);
        }

        private IOffers CreateOffers() {
            return new TestOffers();
        }

        private Pricing CreatePriceCalculator() {
            return new Pricing(CreateOffers());
        }

        private class TestOffers : IOffers
        {
        }
    }
}
