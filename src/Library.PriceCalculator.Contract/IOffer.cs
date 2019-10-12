using System.Collections.Generic;

namespace Library.PriceCalculator.Contract
{
    public interface IOffer
    {
        Discount CheckDiscount(IEnumerable<Item> items);
    }
}