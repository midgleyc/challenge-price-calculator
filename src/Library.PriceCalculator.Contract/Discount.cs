namespace Library.PriceCalculator.Contract
{
    public class Discount
    {
        public Discount(string identifier, decimal baseAmount, int timesApplied)
        {
            Identifier = identifier;
            BaseAmount = baseAmount;
            TimesApplied = timesApplied;
        }

        public string Identifier { get; }

        public decimal BaseAmount { get; }

        public int TimesApplied { get; }
    }
}
