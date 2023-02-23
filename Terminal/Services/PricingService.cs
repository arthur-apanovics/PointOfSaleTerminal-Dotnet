using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Helpers;
using Terminal.Models;

namespace Terminal.Services;

public interface IPricingService
{
    bool TryGetPriceFor(string productCode, out IProductPricing? productPrice);
    decimal CalculateTotalFor(IEnumerable<string> productCodes);
}

public class PricingService : IPricingService
{
    private readonly IProductPricing[] _pricing;

    public PricingService(IEnumerable<IProductPricing> pricing)
    {
        var pricingList = pricing.ToArray();
        ThrowIfDuplicateProductCodePresentIn(pricingList);

        _pricing = pricingList;
    }

    public bool TryGetPriceFor(
        string productCode,
        out IProductPricing? productPrice
    )
    {
        if (HasPriceFor(productCode))
        {
            productPrice = GetPricingFor(productCode);
            return true;
        }

        productPrice = null;
        return false;
    }

    public decimal CalculateTotalFor(IEnumerable<string> productCodes)
    {
        var codes = productCodes.ToArray();
        if (!codes.Any())
            return 0m;

        ThrowIfUnknownProductCodesInList(codes);

        return TallyUpTotalsFor(codes);
    }

    private decimal TallyUpTotalsFor(string[] codes)
    {
        var result = 0m;
        foreach (var (productCode, qty) in CountDistinctCodes(codes))
        {
            var productPricing = GetPricingFor(productCode);
            result += productPricing.GetTotalPriceFor(qty);
        }

        return result;
    }

    private static Dictionary<string, int> CountDistinctCodes(
        IEnumerable<string> productCodes
    ) =>
        productCodes.GroupBy(c => c).ToDictionary(c => c.Key, c => c.Count());

    private IProductPricing GetPricingFor(string productCode) =>
        _pricing.First(p => p.ProductCode == productCode);

    private bool HasPriceFor(string productCode) =>
        _pricing.Any(p => p.ProductCode == productCode);

    private void ThrowIfDuplicateProductCodePresentIn(
        IEnumerable<IProductPricing> pricing
    )
    {
        if (pricing.HasDuplicates(p => p.ProductCode))
            throw new ArgumentException(
                "Pricing list contains duplicate product codes"
            );
    }

    private void ThrowIfUnknownProductCodesInList(string[] productCodes)
    {
        if (!productCodes.Any(HasPriceFor))
            throw new ArgumentException(
                "One or more product codes do not have a corresponding pricing entry - " +
                $"{string.Join(',', productCodes.Where(c => !HasPriceFor(c)))}"
            );
    }
}