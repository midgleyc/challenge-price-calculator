using System.Collections.Generic;
using System.Linq;
using Library.PriceCalculator.Contract;

namespace Library.PriceCalculator.Resources
{
    public class AppleTenPercentDiscountOffer : IOffer
    {
        public bool TryApplyDiscount(ICollection<Item> items, out Discount discount)
        {
            var apples = items.Where(i => i.Identifier == "Apple").ToList();
            if (apples.Any())
            {
                // assume all apples have the same price
                var discountAmount = apples.First().Price * 0.1m;
                discount = new Discount("Apples 10% off", discountAmount, apples.Count);
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
