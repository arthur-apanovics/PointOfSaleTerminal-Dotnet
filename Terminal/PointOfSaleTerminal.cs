using System;
using System.Collections.Generic;
using Terminal.Contracts;

namespace Terminal;

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

        if (!_pricingStrategy!.HasPricing(code))
            throw new ArgumentException(
                $"Product with code '{code}' not found in pricing list"
            );

        _scannedProductCodes.Add(code);
    }

    public decimal CalculateTotal()
    {
        CheckPricingStrategySetOrThrow();

        return _pricingStrategy!.CalculateTotal(_scannedProductCodes);
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