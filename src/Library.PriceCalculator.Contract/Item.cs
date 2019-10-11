namespace Library.PriceCalculator.Contract
{
    public class Item
    {
        public Item(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }

        public decimal Price { get; set; }
    }
}
