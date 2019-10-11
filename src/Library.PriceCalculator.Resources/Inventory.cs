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

        public decimal GetPriceFor(string itemName)
        {
            if (priceMapping.TryGetValue(itemName, out var price)) {
                return price;
            } else {
                throw new InvalidOperationException($"Item {itemName} not in inventory");
            }
        }
    }
}
