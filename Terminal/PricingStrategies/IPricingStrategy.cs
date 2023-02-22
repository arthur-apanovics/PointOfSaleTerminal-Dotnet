using System.Collections.Generic;

namespace Terminal.PricingStrategies;

public interface IPricingStrategy
{
    /// <summary>
    ///     Checks if a pricing record exists for given product code.
    /// </summary>
    /// <param name="code">Product code</param>
    /// <returns>True if price defined for product code, false otherwise</returns>
    public bool HasPricing(string code);

    /// <summary>
    ///     Retrieves price for given product code.
    /// </summary>
    /// <param name="code">Product code</param>
    /// <returns>Price</returns>
    public decimal GetPrice(string code);

    /// <summary>
    ///     Calculates total price for a single product code based on quantity.
    /// </summary>
    /// <param name="code">Product code</param>
    /// <param name="quantity">How many instances to calculate for</param>
    /// <returns>Total price</returns>
    public decimal CalculateTotal(string code, int quantity);

    /// <summary>
    ///     Calculates total price for a mixed set of codes.
    /// </summary>
    /// <param name="codes">Product codes in any order and quantity</param>
    /// <returns>Total price</returns>
    public decimal CalculateTotal(IEnumerable<string> codes);
}