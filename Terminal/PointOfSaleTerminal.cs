using System;
using System.Collections.Generic;
using Terminal.PricingStrategies;

namespace Terminal;

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

public class PointOfSaleTerminal : IPointOfSaleTerminal
{
    private readonly List<string> _scannedProductCodes = new();
    private IPricingStrategy? _pricingStrategy;

    public void SetPricing(IPricingStrategy pricingStrategy)
    {
        _pricingStrategy = pricingStrategy;
    }

    public void ScanProduct(string code)
    {
        CheckPricingStrategySetOrThrow();

        if (!_pricingStrategy!.HasPriceFor(code))
            throw new ArgumentException(
                $"Product with code '{code}' not found in pricing list"
            );

        _scannedProductCodes.Add(code);
    }

    public decimal CalculateTotal()
    {
        CheckPricingStrategySetOrThrow();

        return _pricingStrategy!.CalculateTotalFor(_scannedProductCodes);
    }

    // Note: A constructor with a pricing strategy argument would be
    // preferred to prevent null references (not implemented to comply
    // with top-level interface example in exercise description).
    private void CheckPricingStrategySetOrThrow()
    {
        if (_pricingStrategy is null)
            throw new NullReferenceException(
                $"{nameof(PointOfSaleTerminal)} has no pricing strategy set. Use {nameof(SetPricing)} to set one."
            );
    }
}