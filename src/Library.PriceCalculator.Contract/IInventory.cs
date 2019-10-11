namespace Library.PriceCalculator.Contract
{
    public interface IInventory
    {
        decimal GetPriceFor(string itemName);
    }
}
