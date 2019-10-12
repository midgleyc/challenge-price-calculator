using System.Linq;
using System.Collections.Generic;
using Library.PriceCalculator.Contract;
using System;

namespace Library.PriceCalculator.Calculation
{
    public class Pricing
    {
        private IOffers _offers;

        public Pricing(IOffers offers)
        {
            _offers = offers;
        }

        public Price CalculatePrice(IEnumerable<Item> items)
        {
            return CalculatePrice(items.ToList());
        }

        private Price CalculatePrice(List<Item> items)
        {
            var subTotal = items.Sum(i => i.Price);
            var discounts = ComputeDiscounts(items);
            return new Price(subTotal, discounts);
        }

        private IEnumerable<Discount> ComputeDiscounts(List<Item> items)
        {
            var discounts = new List<Discount>();
            foreach (var offer in _offers.GetOffers())
            {
                if (offer.TryApplyDiscount(items, out var discount))
                {
                    discounts.Add(discount);
                }
            }
            return discounts;
        }
    }
}
