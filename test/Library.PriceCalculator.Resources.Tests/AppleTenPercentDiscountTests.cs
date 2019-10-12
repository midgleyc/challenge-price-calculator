using FluentAssertions;
using Library.PriceCalculator.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.PriceCalculator.Resources.Tests
{
    [TestClass]
    public class AppleTenPercentDiscountOfferTests
    {
        private const decimal applePrice = 2.00m;
        private const decimal mangoPrice = 1.00m;

        [TestMethod]
        public void ApplesAreTenPercentOff()
        {
            var offer = new AppleTenPercentDiscountOffer();
            var success = offer.TryApplyDiscount(new[] { Apple() }, out var discount);
            success.Should().BeTrue();
            discount.BaseAmount.Should().Be(applePrice * 0.1m);
            discount.TimesApplied.Should().Be(1);
        }

        [TestMethod]
        public void NonApplesAreTheSamePrice()
        {
            var offer = new AppleTenPercentDiscountOffer();
            var success = offer.TryApplyDiscount(new[] { Mango() }, out var discount);
            success.Should().BeFalse();
        }

        [TestMethod]
        public void MultipleApplesAreTenPercentOff()
        {
            var offer = new AppleTenPercentDiscountOffer();
            var success = offer.TryApplyDiscount(new[] { Apple(), Apple(), Apple() }, out var discount);
            success.Should().BeTrue();
            discount.BaseAmount.Should().Be(applePrice * 0.1m);
            discount.TimesApplied.Should().Be(3);
        }

        private Item Apple()
        {
            return new Item("Apple") { Price = applePrice };
        }

        private Item Mango()
        {
            return new Item("Mango") { Price = mangoPrice };
        }
    }
}
