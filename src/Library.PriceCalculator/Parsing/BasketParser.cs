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

        public ICollection<Item> ParseBasket(IEnumerable<string> input, out List<string> failed)
        {
            var items = new List<Item>();
            failed = new List<string>();
            foreach (var inputItem in input)
            {
                if (_inventory.TryGetPriceFor(inputItem, out var price))
                {
                    items.Add(new Item(inputItem) { Price = price });
                }
                else
                {
                    failed.Add(inputItem);
                }
            }
            // return as much as possible
            return items;
        }
    }
}
