using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Helpers;
using Terminal.Models;

namespace Terminal.PricingStrategies;

public class StandardPricingStrategy : IPricingStrategy
{
    private readonly Dictionary<string, decimal> _codeToPriceMap;

    public StandardPricingStrategy(IEnumerable<ProductPrice> pricing)
    {
        var productList = pricing.ToList();
        ValidatePricingOrThrow(productList);
        _codeToPriceMap = productList.ToDictionary(p => p.Code, p => p.Price);
    }

    public bool HasPriceFor(string productCode)
    {
        return _codeToPriceMap.ContainsKey(productCode);
    }

    public decimal GetPriceFor(string productCode)
    {
        if (!HasPriceFor(productCode))
            throw new ArgumentOutOfRangeException(
                $"No price found for product with code '{productCode}'"
            );

        return _codeToPriceMap[productCode];
    }

    public decimal CalculateTotalFor(string productCode, int productQuantity)
    {
        return productQuantity switch
        {
            0 => 0,
            < 0 => throw new ArgumentException(
                "Cannot calculate total for negative quantities"
            ),
            _ => CalculateTotalWithoutGuards(productCode, productQuantity)
        };
    }

    public decimal CalculateTotalFor(IEnumerable<string> productCodes)
    {
        var codeList = productCodes.ToList();
        if (!codeList.Any())
            return 0;

        decimal result = 0;
        var codeToQuantity = GroupByCode(codeList);

        foreach (var (code, quantity) in codeToQuantity)
            result += CalculateTotalFor(code, quantity);

        return result;
    }

    private decimal CalculateTotalWithoutGuards(
        string code,
        int quantity
    )
    {
        return GetPriceFor(code) * quantity;
    }

    private static Dictionary<string, int> GroupByCode(
        IEnumerable<string> codes
    )
    {
        return codes.GroupBy(c => c).ToDictionary(x => x.Key, z => z.Count());
    }

    private static void ValidatePricingOrThrow(
        IReadOnlyCollection<ProductPrice> pricing
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