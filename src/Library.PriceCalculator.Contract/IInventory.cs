namespace Library.PriceCalculator.Contract
{
    public interface IInventory
    {
        bool TryGetPriceFor(string itemName, out decimal price);
    }
}
