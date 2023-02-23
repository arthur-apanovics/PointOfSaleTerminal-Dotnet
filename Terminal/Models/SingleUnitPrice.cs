namespace Terminal.Models;

public record SingleUnitPrice : IProductPrice
{
    public string ProductCode { get; }

    private decimal UnitPrice { get; }

    private SingleUnitPrice(string productCode, decimal unitPrice)
    {
        ProductCode = productCode;
        UnitPrice = unitPrice;
    }

    public decimal CalculateTotalFor(int productQuantity)
    {
        return UnitPrice * productQuantity;
    }

    public static SingleUnitPrice
        Create(string productCode, decimal unitPrice) =>
        new(productCode, unitPrice);
}