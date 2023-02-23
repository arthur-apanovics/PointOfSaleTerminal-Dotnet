namespace Terminal.PricingStrategies;

public interface IDiscountablePricingStrategy : IPricingStrategy
{
    /// <summary>
    ///     Checks if a discounted pricing record exists for given product code.
    /// </summary>
    /// <param name="productCode">Product code</param>
    /// <returns>True if discounted price defined for product code, false otherwise</returns>
    public bool HasDiscountedPricingFor(string productCode);
}