namespace Terminal.Models;

public interface IProductPricing
{
    public string ProductCode { get; }
    decimal GetTotalPriceFor(int productQuantity);
}

