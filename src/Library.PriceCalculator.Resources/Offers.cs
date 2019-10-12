using System.Collections.Generic;
using Library.PriceCalculator.Contract;

namespace Library.PriceCalculator.Resources
{
    public class Offers : IOffers
    {
        private readonly List<IOffer> offers = new List<IOffer> { };

        public ICollection<IOffer> GetOffers()
        {
            return offers;
        }
    }
}
