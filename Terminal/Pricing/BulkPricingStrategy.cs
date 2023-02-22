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

    private readonly Dictionary<string, BulkProductPrice>
        _productCodeToBulkPrice;

    public BulkPricingStrategy(
        IPricingStrategy pricingStrategy,
        IEnumerable<BulkProduct> bulkPricing
    )
    {
        _pricingStrategy = pricingStrategy;
        var bulkPriceList = bulkPricing.ToList();
        ValidatePricingOrThrow(bulkPriceList);
        _productCodeToBulkPrice = bulkPriceList.ToDictionary(
            b => b.ProductCode,
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
        return _productCodeToBulkPrice.ContainsKey(code);
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
        var bulkPrice = _productCodeToBulkPrice[code];
        while (remainder >= bulkPrice.BulkThreshold)
        {
            result += bulkPrice.BulkPrice;
            remainder -= bulkPrice.BulkThreshold;
        }

        return (result, remainder);
    }

    private static void ValidatePricingOrThrow(
        IEnumerable<BulkProduct> bulkPrices
    )
    {
        if (bulkPrices.HasDuplicates(b => b.ProductCode))
            throw new ArgumentException(
                "Bulk pricing list cannot contain duplicate products"
            );
    }
}