using System;
using System.Collections.Generic;
using Terminal.Services;

namespace Terminal;

public interface IPointOfSaleTerminal
{
    void SetPricing(IPricingService pricingStrategy);

    void ScanProduct(string productCode);

    decimal CalculateTotal();
}

public class PointOfSaleTerminal : IPointOfSaleTerminal
{
    private readonly List<string> _scannedProductCodes = new();
    private IPricingService? _pricingService;

    public void SetPricing(IPricingService pricingService)
    {
        _pricingService = pricingService;
    }

    public void ScanProduct(string productCode)
    {
        ThrowIfPricingNotSet();
        ThrowIfNoPricingFoundFor(productCode);

        _scannedProductCodes.Add(productCode);
    }

    public decimal CalculateTotal()
    {
        ThrowIfPricingNotSet();

        return _pricingService!.CalculateTotalFor(_scannedProductCodes);
    }

    private void ThrowIfPricingNotSet()
    {
        // Note: Injecting the pricing service would be preferred to prevent
        // null references (not implemented to comply with top-level
        // interface example in exercise description)
        if (_pricingService is null)
            throw new NullReferenceException(
                $"{nameof(PointOfSaleTerminal)} has no pricing set. " +
                $"Use {nameof(SetPricing)} to set one."
            );
    }

    private void ThrowIfNoPricingFoundFor(string productCode)
    {
        if (!_pricingService!.TryGetPriceFor(productCode, out _))
            throw new ArgumentException(
                $"No Pricing found for product code \"{productCode}\""
            );
    }
}