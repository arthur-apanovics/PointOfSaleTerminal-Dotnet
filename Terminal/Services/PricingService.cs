using System.Collections.Generic;

namespace Terminal.Services;

public class PricingService
{
    private readonly Dictionary<string, decimal> _pricing;

    public PricingService(Dictionary<string, decimal> pricing)
    {
        _pricing = pricing;
    }

    public bool TryGetPriceFor(string productCode, out decimal? productPrice)
    {
        if (_pricing.ContainsKey(productCode))
        {
            productPrice = _pricing[productCode];
            return true;
        }

        productPrice = null;
        return false;
    }
}