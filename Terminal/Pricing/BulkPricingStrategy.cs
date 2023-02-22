using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Common;
using Terminal.Contracts;
using Terminal.Models;

namespace Terminal.Pricing;

public class BulkPricingStrategy : IDiscountablePricingStrategy
{
    private readonly IPricingStrategy _pricingStrategy;
    private readonly Dictionary<string, BulkPrice> _codeToBulkPriceMap;

    public BulkPricingStrategy(
        IPricingStrategy pricingStrategy,
        IEnumerable<BulkProduct> bulkPricing
    )
    {
        _pricingStrategy = pricingStrategy;
        var bulkPriceList = bulkPricing.ToList();
        ValidatePricingOrThrow(bulkPriceList);
        _codeToBulkPriceMap = bulkPriceList.ToDictionary(
            b => b.Code,
            b => b.BulkPrice
        );
    }

    public bool HasPricing(string code)
    {
        return _pricingStrategy.HasPricing(code);
    }

    public decimal GetPrice(string code)
    {
        return _pricingStrategy.GetPrice(code);
    }

    public decimal CalculateTotal(string code, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException(
                "Cannot calculate total for negative quantities"
            );

        return CalculateTotalWithoutGuards(code, quantity);
    }

    public decimal CalculateTotal(IEnumerable<string> codes)
    {
        var codeList = codes.ToList();
        if (!codeList.Any())
            return 0;

        decimal result = 0;
        var codeToQuantity = codeList.GroupBy(c => c)
            .ToDictionary(x => x.Key, z => z.Count());

        foreach (var (code, quantity) in codeToQuantity)
            result += CalculateTotal(code, quantity);

        return result;
    }

    public bool HasDiscountedPricing(string code)
    {
        return _codeToBulkPriceMap.ContainsKey(code);
    }

    private decimal CalculateTotalWithoutGuards(string code, int quantity)
    {
        var remaining = quantity;
        (var result, remaining) = TotalWithDiscount(code, remaining);
        result += GetPrice(code) * remaining;

        return result;
    }

    private (decimal result, int remainder) TotalWithDiscount(
        string code,
        int quantity
    )
    {
        var result = 0m;
        var remainder = quantity;

        if (!HasDiscountedPricing(code))
            return (result, remainder);

        // opting to use while loop instead of arithmetic for simplicity/readability
        var bulkPrice = _codeToBulkPriceMap[code];
        while (remainder >= bulkPrice.Threshold)
        {
            result += bulkPrice.Price;
            remainder -= bulkPrice.Threshold;
        }

        return (result, remainder);
    }

    private static void ValidatePricingOrThrow(
        IEnumerable<BulkProduct> bulkPrices
    )
    {
        if (bulkPrices.HasDuplicates(b => b.Code))
            throw new ArgumentException(
                "Bulk pricing list cannot contain duplicate products"
            );
    }
}