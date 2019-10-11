using System.Collections.Generic;
using System.Linq;

namespace Library.PriceCalculator.Contract
{
    public class Price
    {
        public Price(decimal subTotal) : this(subTotal, Enumerable.Empty<Discount>())
        {
        }

        public Price(decimal subTotal, IEnumerable<Discount> discounts)
        {
            SubTotal = subTotal;
            Discounts = discounts;
        }

        public decimal SubTotal { get; }

        public decimal Total { get; }

        public IEnumerable<Discount> Discounts { get; }
    }
}
