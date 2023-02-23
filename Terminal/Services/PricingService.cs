using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Helpers;
using Terminal.Models;

namespace Terminal.Services;

public interface IPricingService
{
    bool TryGetPriceFor(string productCode, out IProductPrice? productPrice);
    decimal CalculateTotalFor(IEnumerable<string> productCodes);
}

public class PricingService : IPricingService
{
    private readonly List<IProductPrice> _pricing;

    public PricingService(IEnumerable<IProductPrice> pricing)
    {
        var pricingList = pricing.ToList();
        ThrowIfDuplicateProductCodePresentIn(pricingList);

        _pricing = pricingList;
    }

    public bool TryGetPriceFor(
        string productCode,
        out IProductPrice? productPrice
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

        var codeToQty = CountDistinctCodes(codes);
        var total = 0m;
        foreach (var (productCode, qty) in codeToQty)
        {
            var productPricing = GetPricingFor(productCode);
            total += productPricing.CalculateTotalFor(qty);
        }

        return total;
    }

    private static Dictionary<string, int> CountDistinctCodes(
        IEnumerable<string> productCodes
    ) =>
        productCodes.GroupBy(c => c).ToDictionary(c => c.Key, c => c.Count());

    private IProductPrice GetPricingFor(string productCode) =>
        _pricing.First(p => p.ProductCode == productCode);

    private bool HasPriceFor(string productCode) =>
        _pricing.Any(p => p.ProductCode == productCode);

    private void ThrowIfDuplicateProductCodePresentIn(
        IEnumerable<IProductPrice> pricing
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