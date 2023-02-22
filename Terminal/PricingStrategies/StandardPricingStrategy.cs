using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Helpers;
using Terminal.Models;

namespace Terminal.PricingStrategies;

public class StandardPricingStrategy : IPricingStrategy
{
    private readonly Dictionary<string, decimal> _codeToPriceMap;

    public StandardPricingStrategy(IEnumerable<Product> pricing)
    {
        var productList = pricing.ToList();
        ValidatePricingOrThrow(productList);
        _codeToPriceMap = productList.ToDictionary(p => p.Code, p => p.Price);
    }

    public bool HasPricing(string code)
    {
        return _codeToPriceMap.ContainsKey(code);
    }

    public decimal GetPrice(string code)
    {
        if (!HasPricing(code))
            throw new ArgumentOutOfRangeException(
                $"No price found for product with code '{code}'"
            );

        return _codeToPriceMap[code];
    }

    public decimal CalculateTotal(string code, int quantity)
    {
        return quantity switch
        {
            0 => 0,
            < 0 => throw new ArgumentException(
                "Cannot calculate total for negative quantities"
            ),
            _ => CalculateTotalWithoutGuards(code, quantity)
        };
    }

    public decimal CalculateTotal(IEnumerable<string> codes)
    {
        var codeList = codes.ToList();
        if (!codeList.Any())
            return 0;

        decimal result = 0;
        var codeToQuantity = GroupByCode(codeList);

        foreach (var (code, quantity) in codeToQuantity)
            result += CalculateTotal(code, quantity);

        return result;
    }

    private decimal CalculateTotalWithoutGuards(
        string code,
        int quantity
    )
    {
        return GetPrice(code) * quantity;
    }

    private static Dictionary<string, int> GroupByCode(
        IEnumerable<string> codes
    )
    {
        return codes.GroupBy(c => c).ToDictionary(x => x.Key, z => z.Count());
    }

    private static void ValidatePricingOrThrow(
        IReadOnlyCollection<Product> pricing
    )
    {
        if (!pricing.Any())
            throw new ArgumentException("Pricing cannot be empty");

        if (pricing.HasDuplicates(x => x.Code))
            throw new ArgumentException(
                "Pricing cannot contain duplicate product codes"
            );
    }
}