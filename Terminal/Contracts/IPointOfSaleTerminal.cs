namespace Terminal.Contracts;

public interface IPointOfSaleTerminal
{
    /// <summary>
    ///     Sets internal pricing strategy that will be used for calculating totals.
    /// </summary>
    /// <param name="pricingStrategy">Valid pricing strategy</param>
    void SetPricing(IPricingStrategy pricingStrategy);

    /// <summary>
    ///     Stores product code in local state.
    /// </summary>
    /// <param name="code">Product code</param>
    /// <exception cref="ArgumentException">If pricing is not found for product code</exception>
    void ScanProduct(string code);

    /// <summary>
    ///     Calculates total price for all previously scanned products.
    /// </summary>
    /// <returns>Total price</returns>
    decimal CalculateTotal();
}