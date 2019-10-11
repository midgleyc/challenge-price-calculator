using System;
using System.Collections.Generic;
using Library.PriceCalculator.Contract;

namespace Library.PriceCalculator.Resources
{
    public class Inventory : IInventory
    {
        private readonly Dictionary<string, decimal> priceMapping = new Dictionary<string, decimal> {
            {"Beans", 0.65m},
            {"Bread", 0.80m},
            {"Milk", 1.30m},
            {"Apple", 1.00m},
        };

        public bool TryGetPriceFor(string itemName, out decimal price)
        {
            return priceMapping.TryGetValue(itemName, out price);
        }
    }
}
