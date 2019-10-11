using System;
using System.Collections.Generic;
using System.Linq;
using Library.PriceCalculator.Contract;
using Library.PriceCalculator.Parsing;
using Library.PriceCalculator.Resources;

namespace Service.PriceCalculator
{
    class Program
    {
        static int Main(string[] args)
        {
            var retCode = 0;
            var basketParser = new BasketParser(new Inventory());
            ICollection<Item> items = basketParser.ParseBasket(args, out var failed);
            if (failed.Any()) {
                Console.WriteLine("Could not parse input:");
                retCode = 1;
                foreach (var error in failed) {
                    Console.WriteLine($"Item {error} not in inventory");
                }
            }
            Console.WriteLine("Parsed items:");
            foreach (Item item in items) {
                Console.WriteLine($"Item {item.Identifier} with price {item.Price}");
            }
            return retCode;
        }
    }
}
