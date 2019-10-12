using System;
using System.Collections.Generic;
using System.Linq;
using Library.PriceCalculator.Contract;
using static Library.PriceCalculator.Resources.Identifiers;

namespace Library.PriceCalculator.Resources
{
    public class TwoBeansIsBreadHalfOffOffer : IOffer
    {
        public bool TryApplyDiscount(ICollection<Item> items, out Discount discount)
        {
            var beans = items.Where(i => i.Identifier == Beans).ToList();
            var bread = items.Where(i => i.Identifier == Bread).ToList();
            if (beans.Count >= 2 && bread.Count >= 1)
            {
                // assume all bread loaves have the same price
                var discountAmount = bread.First().Price * 0.5m;
                discount = new Discount("Bread 50% off with two beans", discountAmount, Math.Min(beans.Count / 2, bread.Count));
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
