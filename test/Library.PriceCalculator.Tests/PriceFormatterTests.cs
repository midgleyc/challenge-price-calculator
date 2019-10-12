using FluentAssertions;
using Library.PriceCalculator.Contract;
using Library.PriceCalculator.Formatter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.PriceCalculator.Tests
{
    [TestClass]
    public class PriceFormatterTests
    {
        [TestMethod]
        public void NoDiscountsAboveOnePound()
        {
            var formatter = new PriceFormatter();
            var output = formatter.Format(new Price(2.00m));
            output.Should().BeEquivalentTo(new[] {
                "Subtotal: £2.00",
                "(No offers available)",
                "Total price: £2.00"
            });
        }

        [TestMethod]
        public void NoDiscountsBelowOnePound()
        {
            var formatter = new PriceFormatter();
            var output = formatter.Format(new Price(0.66m));
            output.Should().BeEquivalentTo(new[] {
                "Subtotal: 66p",
                "(No offers available)",
                "Total price: 66p"
            });
        }

        [TestMethod]
        public void OneDiscountBelowOnePound()
        {
            var formatter = new PriceFormatter();
            var output = formatter.Format(new Price(1.00m, new[] { new Discount("Test discount", 0.01m, 1) }));
            output.Should().BeEquivalentTo(new[] {
                "Subtotal: £1.00",
                "Test discount: -1p",
                "Total price: 99p"
            });
        }

        [TestMethod]
        public void OneDiscountAboveOnePound()
        {
            var formatter = new PriceFormatter();
            var output = formatter.Format(new Price(10.00m, new[] { new Discount("Test discount", 1.00m, 1) }));
            output.Should().BeEquivalentTo(new[] {
                "Subtotal: £10.00",
                "Test discount: -£1.00",
                "Total price: £9.00"
            });
        }

        [TestMethod]
        public void ManyDiscounts()
        {
            var formatter = new PriceFormatter();
            var output = formatter.Format(new Price(10.00m, new[] { new Discount("Test discount1", 1.00m, 2), new Discount("Test discount2", 0.50m, 1) }));
            output.Should().BeEquivalentTo(new[] {
                "Subtotal: £10.00",
                "Test discount1: -£1.00",
                "Test discount1: -£1.00",
                "Test discount2: -50p",
                "Total price: £7.50"
            });
        }
    }
}
