using System;
using System.Collections.Generic;
using Library.PriceCalculator.Contract;
using Library.PriceCalculator.Parsing;
using Library.PriceCalculator.Resources;

namespace Service.PriceCalculator
{
    class Program
    {
        static int Main(string[] args)
        {
            var basketParser = new BasketParser(new Inventory());
            ICollection<Item> items;
            try {
                items = basketParser.ParseBasket(args);
            } catch (InvalidOperationException e) {
                Console.WriteLine("Could not parse input.");
                Console.WriteLine(e.Message);
                return 1;
            }
            Console.WriteLine("Parsed items:");
            foreach (Item item in items) {
                Console.WriteLine($"Item {item.Identifier} with price {item.Price}");
            }
            return 0;
        }
    }
}
