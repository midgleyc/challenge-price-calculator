using System.Linq;
using System.Collections.Generic;
using Library.PriceCalculator.Contract;
using System;

namespace Library.PriceCalculator.Formatter
{
    public class PriceFormatter
    {
        public ICollection<string> Format(Price price)
        {
            var lines = new List<string>();
            lines.Add($"Subtotal: {FormatDecimalAsUkCurrency(price.SubTotal)}");
            if (!price.Discounts.Any())
            {
                lines.Add("(No offers available)");
            }
            else
            {
                foreach (var discount in price.Discounts)
                {
                    for (var i = 0; i < discount.TimesApplied; i++)
                    {
                        lines.Add($"{discount.Identifier}: {FormatDecimalAsUkCurrency(-discount.BaseAmount)}");
                    }
                }
            }
            lines.Add($"Total price: {FormatDecimalAsUkCurrency(price.Total)}");
            return lines;
        }

        private string FormatDecimalAsUkCurrency(decimal amt)
        {
            if (amt >= 1m)
            {
                return string.Format("£{0:0.00}", amt);
            }
            else if (amt <= -1m)
            {
                return string.Format("-£{0:0.00}", -amt);
            }
            else
            {
                return string.Format("{0:0.}p", amt * 100m);
            }
        }
    }
}
