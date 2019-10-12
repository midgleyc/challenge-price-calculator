using System;
using System.Collections.Generic;
using System.Linq;
using Library.PriceCalculator.Calculation;
using Library.PriceCalculator.Contract;
using Library.PriceCalculator.Formatter;
using Library.PriceCalculator.Parsing;
using Library.PriceCalculator.Resources;

namespace Service.PriceCalculator
{
    class Program
    {
        static int Main(string[] args)
        {
            var basketParser = new BasketParser(new Inventory());
            ICollection<Item> items = basketParser.ParseBasket(args, out var failed);
            if (failed.Any())
            {
                Console.WriteLine("Could not parse input:");
                foreach (var error in failed)
                {
                    Console.WriteLine($"Item {error} not in inventory");
                }
                return 1;
            }
            var pricing = new Pricing(new Offers());
            var price = pricing.CalculatePrice(items);
            var lines = new PriceFormatter().Format(price);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
            return 0;
        }
    }
}
