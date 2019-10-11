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
            var items = new List<Item>();
            var failed = new List<Exception>();
            foreach (var inputItem in input) {
                if (_inventory.TryGetPriceFor(inputItem, out var price)) {
                    items.Add(new Item(inputItem) {Price = price});
                } else {
                    failed.Add(new InvalidOperationException($"Item {inputItem} not in inventory"));
                }
            }
            // for now, just suppress the errors: return as much as possible
            return items;
        }
    }
}
