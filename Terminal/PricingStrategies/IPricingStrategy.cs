using System.Collections.Generic;

namespace Terminal.PricingStrategies;

public interface IPricingStrategy
{
    /// <summary>
    ///     Checks if a pricing record exists for given product code.
    /// </summary>
    /// <param name="productCode">Product code</param>
    /// <returns>True if price defined for product code, false otherwise</returns>
    public bool HasPriceFor(string productCode);

    /// <summary>
    ///     Retrieves price for given product code.
    /// </summary>
    /// <param name="productCode">Product code</param>
    /// <returns>Price</returns>
    public decimal GetPriceFor(string productCode);

    /// <summary>
    ///     Calculates total price for a single product code based on quantity.
    /// </summary>
    /// <param name="productCode">Product code</param>
    /// <param name="productQuantity">How many instances to calculate for</param>
    /// <returns>Total price</returns>
    public decimal CalculateTotalFor(string productCode, int productQuantity);

    /// <summary>
    ///     Calculates total price for a mixed set of codes.
    /// </summary>
    /// <param name="productCodes">Product codes in any order and quantity</param>
    /// <returns>Total price</returns>
    public decimal CalculateTotalFor(IEnumerable<string> productCodes);
}