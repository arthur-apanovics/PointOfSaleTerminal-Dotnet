using Terminal.Helpers;

namespace Terminal.Models;

public record SingleUnitPricing : IProductPricing
{
    public string ProductCode { get; }

    private decimal UnitPrice { get; }

    private SingleUnitPricing(string productCode, decimal unitPrice)
    {
        ProductCode = productCode;
        UnitPrice = unitPrice;
    }

    public decimal GetTotalPriceFor(int productQuantity)
    {
        return UnitPrice * productQuantity;
    }

    public static SingleUnitPricing Create(
        string productCode,
        decimal unitPrice
    )
    {
        ProductValidationHelper.ThrowIfProductCodeNotValid(productCode);
        ProductValidationHelper.ThrowIfProductPriceNotValid(unitPrice);

        return new SingleUnitPricing(productCode, unitPrice);
    }
}