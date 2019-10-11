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
            foreach (var inputItem in input) {
                var price = _inventory.GetPriceFor(inputItem);
                items.Add(new Item(inputItem) {Price = price});
            }
            return items;
        }
    }
}
