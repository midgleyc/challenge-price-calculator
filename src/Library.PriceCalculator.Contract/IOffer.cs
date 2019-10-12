using System.Collections.Generic;

namespace Library.PriceCalculator.Contract
{
    public interface IOffer
    {
        bool TryApplyDiscount(ICollection<Item> items, out Discount discount);
    }
}