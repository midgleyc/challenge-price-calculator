using System;
using System.Collections.Generic;
using Library.PriceCalculator.Contract;

namespace Library.PriceCalculator.Parsing
{
    public class BasketParser
    {
        private IInventory _inventory;

        public BasketParser(IInventory inventory)
        {
            _inventory = inventory;
        }

        public ICollection<Item> ParseBasket(IEnumerable<string> input) {
            return new List<Item>();
        }
    }
}
