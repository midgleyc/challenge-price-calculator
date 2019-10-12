using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Library.PriceCalculator.Calculation;
using Library.PriceCalculator.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.PriceCalculator.Tests
{
    [TestClass]
    public class PricingTests
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
            var output = priceCalculator.CalculatePrice(new Item[] { ItemWithPrice(1.23m), ItemWithPrice(2.34m), ItemWithPrice(3.45m, "Other") });
            output.SubTotal.Should().Be(7.02m);
        }

        [TestMethod]
        public void AlwaysOfferAlwaysApplied()
        {
            var priceCalculator = CreatePriceCalculator(new[] { new AlwaysOffer() });
            var output = priceCalculator.CalculatePrice(new Item[] { ItemWithPrice(1.23m), ItemWithPrice(2.34m) });
            output.Discounts.Should().ContainSingle().Subject.Identifier.Should().Be("Always");
        }

        [TestMethod]
        public void NeverOfferNeverApplied()
        {
            var priceCalculator = CreatePriceCalculator(new[] { new NeverOffer() });
            var output = priceCalculator.CalculatePrice(new Item[] { ItemWithPrice(1.23m), ItemWithPrice(2.34m) });
            output.Discounts.Should().BeEmpty();
        }

        [TestMethod]
        public void TestOfferAppliedForTestItems()
        {
            var priceCalculator = CreatePriceCalculator(new[] { new TestOffer() });
            var output = priceCalculator.CalculatePrice(new Item[] { ItemWithPrice(1.23m, "Test"), ItemWithPrice(2.34m, "Test2"), ItemWithPrice(1.23m, "Test") });
            output.Discounts.Should().ContainSingle().Subject.TimesApplied.Should().Be(2);
        }

        private IOffers CreateEmptyOffers()
        {
            return new TestOffers();
        }

        private Pricing CreatePriceCalculatorWithNoOffers()
        {
            return new Pricing(CreateEmptyOffers());
        }

        private Pricing CreatePriceCalculator(IEnumerable<IOffer> offers)
        {
            return new Pricing(CreateOffers(offers));
        }

        private IOffers CreateOffers(IEnumerable<IOffer> offers)
        {
            return new TestOffers(offers);
        }

        private static Item ItemWithPrice(decimal price, string id = "Test")
        {
            return new Item(id) { Price = price };
        }

        private class TestOffers : IOffers
        {
            private List<IOffer> _offers;

            public TestOffers() : this(Enumerable.Empty<IOffer>())
            {
            }

            public TestOffers(IEnumerable<IOffer> offers)
            {
                _offers = offers.ToList();
            }

            public ICollection<IOffer> GetOffers()
            {
                return _offers;
            }
        }

        private class AlwaysOffer : IOffer
        {
            public bool TryApplyDiscount(IEnumerable<Item> items, out Discount discount)
            {
                discount = new Discount("Always", 0.10m, 1);
                return true;
            }
        }

        private class NeverOffer : IOffer
        {
            public bool TryApplyDiscount(IEnumerable<Item> items, out Discount discount)
            {
                discount = null;
                return false;
            }
        }

        private class TestOffer : IOffer
        {
            public bool TryApplyDiscount(IEnumerable<Item> items, out Discount discount)
            {
                items = items.ToList();
                var testItems = items.Where(i => i.Identifier == "Test").ToList();
                if (testItems.Any())
                {
                    discount = new Discount("Test discount", 0.11m, testItems.Count);
                    return true;
                }
                else
                {
                    discount = null;
                    return false;
                }
            }
        }
    }
}
