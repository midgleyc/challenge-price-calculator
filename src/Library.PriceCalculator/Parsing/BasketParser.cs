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
            return ParseBasket(input, out var _);
        }

        public ICollection<Item> ParseBasket(IEnumerable<string> input, out List<Exception> failed) {
            var items = new List<Item>();
            failed = new List<Exception>();
            foreach (var inputItem in input) {
                if (_inventory.TryGetPriceFor(inputItem, out var price)) {
                    items.Add(new Item(inputItem) {Price = price});
                } else {
                    // TODO: maybe something better than exception? only using as strings, really.
                    failed.Add(new InvalidOperationException($"Item {inputItem} not in inventory"));
                }
            }
            // for now, just suppress the errors: return as much as possible
            return items;
        }
    }
}
