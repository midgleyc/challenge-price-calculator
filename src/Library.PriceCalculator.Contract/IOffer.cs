using System.Collections.Generic;

namespace Library.PriceCalculator.Contract
{
    public interface IOffer
    {
        bool TryApplyDiscount(IEnumerable<Item> items, out Discount discount);
    }
}