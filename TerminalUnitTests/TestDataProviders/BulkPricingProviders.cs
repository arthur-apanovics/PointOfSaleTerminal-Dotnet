using System;
using System.Collections.Generic;
using Terminal.Models;
using TerminalUnitTests.Builders;
using TerminalUnitTests.Builders.Models;

namespace TerminalUnitTests.TestDataProviders;

public static class BulkPricingProviders
{
    private static readonly Product[] StandardPricing =
    {
        ProductBuilder.Build(withCode: "A", withPrice: 1.25m),
        ProductBuilder.Build(withCode: "B", withPrice: 4.25m),
        ProductBuilder.Build(withCode: "C", withPrice: 1m),
        ProductBuilder.Build(withCode: "D", withPrice: 0.75m)
    };

    private static readonly BulkProduct[] BulkPricing =
    {
        BulkProduct.Create(
            productCode: "A",
            BulkProductPrice.Create(bulkThreshold: 3, bulkPrice: 3m)
        ),
        BulkProduct.Create(
            productCode: "C",
            BulkProductPrice.Create(bulkThreshold: 6, bulkPrice: 5m)
        )
    };

    public static IEnumerable<object[]> BulkProductCodesAndTotals()
    {
        var productCodesAndTotalPrices =
            new (string[] ProductCodes, decimal ExpectedTotal)[]
            {
                (new[] { "A", "B", "C", "D", "A", "B", "A" }, 13.25m),
                (new[] { "C", "C", "C", "C", "C", "C", "C" }, 6m),
                (new[] { "A", "B", "C", "D" }, 7.25m),
                (new[] { "B", "B", "B", "B", "B" }, 21.25m),
                (new[] { "B", "B", "B", "D", "D", "D" }, 15m),
                (new[] { "D" }, 0.75m),
                (Array.Empty<string>(), 0)
            };

        foreach (var (codes, discountedTotal) in productCodesAndTotalPrices)
            yield return new object[]
            {
                StandardPricing, BulkPricing, codes, discountedTotal
            };
    }

    public static IEnumerable<object[]> BulkProductQuantityAndTotals()
    {
        var productQtyAndTotals =
            new (string ProductCode, int ProductQuantity, decimal ExpectedTotal)
                []
                {
                    ("A", 0, 0m),
                    ("A", 1, 1.25m),
                    ("A", 9, 9m),
                    ("A", 10, 10.25m),
                    ("C", 7, 6m),
                };

        foreach (var (code, qty, total) in productQtyAndTotals)
        {
            yield return new object[]
            {
                StandardPricing, BulkPricing, code, qty, total
            };
        }
    }
}