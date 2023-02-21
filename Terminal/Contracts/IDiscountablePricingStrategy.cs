namespace Terminal.Contracts;

public interface IDiscountablePricingStrategy : IPricingStrategy
{
    /// <summary>
    ///     Checks if a discounted pricing record exists for given product code.
    /// </summary>
    /// <param name="code">Product code</param>
    /// <returns>True if discounted price defined for product code, false otherwise</returns>
    public bool HasDiscountedPricing(string code);
}