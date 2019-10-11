using System;
using System.Collections.Generic;
using Library.PriceCalculator.Contract;

namespace Library.PriceCalculator.Calculation
{
    public class PriceCalculator
    {
        private IOffers _offers;

        public PriceCalculator(IOffers offers)
        {
            _offers = offers;
        }

        public Price CalculatePrice(IEnumerable<Item> items) {
            return new Price(0.0m);
        }
    }
}
