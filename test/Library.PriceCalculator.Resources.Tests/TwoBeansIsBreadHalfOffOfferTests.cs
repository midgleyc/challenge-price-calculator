using FluentAssertions;
using Library.PriceCalculator.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.PriceCalculator.Resources.Tests
{
    [TestClass]
    public class TwoBeansIsBreadHalfOffOfferTests
    {
        private const decimal breadPrice = 2.00m;
        private const decimal beansPrice = 1.00m;

        [TestMethod]
        public void BreadIsHalfOffWithTwoBeans()
        {
            var offer = new TwoBeansIsBreadHalfOffOffer();
            var success = offer.TryApplyDiscount(new[] { Bread(), Beans(), Beans() }, out var discount);
            success.Should().BeTrue();
            discount.BaseAmount.Should().Be(breadPrice * 0.5m);
            discount.TimesApplied.Should().Be(1);
        }

        [TestMethod]
        public void BreadIsNotHalfOffWithOneBeans()
        {
            var offer = new TwoBeansIsBreadHalfOffOffer();
            var success = offer.TryApplyDiscount(new[] { Bread(), Beans() }, out var discount);
            success.Should().BeFalse();
        }

        [TestMethod]
        public void MultipleBreadsCanAllBeHalfOff()
        {
            var offer = new TwoBeansIsBreadHalfOffOffer();
            var success = offer.TryApplyDiscount(new[] { Bread(), Beans(), Beans(), Beans(), Beans(), Bread() }, out var discount);
            success.Should().BeTrue();
            discount.BaseAmount.Should().Be(breadPrice * 0.5m);
            discount.TimesApplied.Should().Be(2);
        }

        [TestMethod]
        public void OnlyAppliesOnceEvenWithLotsOfBread()
        {
            var offer = new TwoBeansIsBreadHalfOffOffer();
            var success = offer.TryApplyDiscount(new[] { Bread(), Bread(), Bread(), Beans(), Beans(), Bread() }, out var discount);
            success.Should().BeTrue();
            discount.BaseAmount.Should().Be(breadPrice * 0.5m);
            discount.TimesApplied.Should().Be(1);
        }

        private Item Bread()
        {
            return new Item("Bread") { Price = breadPrice };
        }

        private Item Beans()
        {
            return new Item("Beans") { Price = beansPrice };
        }
    }
}
