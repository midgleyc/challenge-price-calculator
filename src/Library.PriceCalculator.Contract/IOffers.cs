using System.Collections.Generic;

namespace Library.PriceCalculator.Contract
{
    public interface IOffers
    {
        ICollection<IOffer> GetOffers();
    }
}
