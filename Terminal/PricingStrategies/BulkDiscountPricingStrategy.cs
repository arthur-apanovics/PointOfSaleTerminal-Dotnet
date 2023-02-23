using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Helpers;
using Terminal.Models;

namespace Terminal.PricingStrategies;

public class BulkDiscountPricingStrategy : IDiscountablePricingStrategy
{
    private readonly IPricingStrategy _pricingStrategy;
    private readonly BulkProductPrice[] _bulkProductPrices;

    public BulkDiscountPricingStrategy(
        IPricingStrategy pricingStrategy,
        IEnumerable<BulkProductPrice> bulkProductPricing
    )
    {
        _pricingStrategy = pricingStrategy;

        _bulkProductPrices = ValidateBulkPricingOrThrow(bulkProductPricing);
    }

    public bool HasPriceFor(string productCode)
    {
        return _pricingStrategy.HasPriceFor(productCode);
    }

    public decimal GetPriceFor(string productCode)
    {
        return _pricingStrategy.GetPriceFor(productCode);
    }

    public decimal CalculateTotalFor(string productCode, int productQuantity)
    {
        if (productQuantity < 0)
            throw new ArgumentException(
                "Cannot calculate total for negative quantities"
            );

        return CalculateTotalWithoutGuards(productCode, productQuantity);
    }

    public decimal CalculateTotalFor(IEnumerable<string> productCodes)
    {
        var codeList = productCodes.ToList();
        if (!codeList.Any())
            return 0;

        decimal result = 0;
        var codeToQuantity = codeList.GroupBy(c => c)
            .ToDictionary(x => x.Key, z => z.Count());

        foreach (var (code, quantity) in codeToQuantity)
            result += CalculateTotalFor(code, quantity);

        return result;
    }

    public bool HasDiscountedPricingFor(string productCode)
    {
        return _bulkProductPrices.Any(bpp => bpp.ProductCode == productCode);
    }

    private decimal CalculateTotalWithoutGuards(string code, int quantity)
    {
        var remaining = quantity;
        (var result, remaining) = TotalWithDiscount(code, remaining);
        result += GetPriceFor(code) * remaining;

        return result;
    }

    private (decimal result, int remainder) TotalWithDiscount(
        string code,
        int quantity
    )
    {
        var result = 0m;
        var remainder = quantity;

        if (!HasDiscountedPricingFor(code))
            return (result, remainder);

        // opting to use while loop instead of arithmetic for simplicity/readability
        var bulkPrice =
            _bulkProductPrices.First(bpp => bpp.ProductCode == code);
        while (remainder >= bulkPrice.BulkThreshold)
        {
            result += bulkPrice.BulkPrice;
            remainder -= bulkPrice.BulkThreshold;
        }

        return (result, remainder);
    }

    private static BulkProductPrice[] ValidateBulkPricingOrThrow(
        IEnumerable<BulkProductPrice> bulkPrices
    )
    {
        var pricingArray =
            bulkPrices as BulkProductPrice[] ?? bulkPrices.ToArray();

        if (pricingArray.HasDuplicates(b => b.ProductCode))
            throw new ArgumentException(
                "Bulk pricing list cannot contain duplicate products"
            );

        return pricingArray;
    }
}