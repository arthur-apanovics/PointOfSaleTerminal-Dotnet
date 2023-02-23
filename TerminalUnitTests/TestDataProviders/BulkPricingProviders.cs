using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Models;
using TerminalUnitTests.Builders.Models;

namespace TerminalUnitTests.TestDataProviders;

public static class BulkPricingProviders
{
    public static IEnumerable<object[]> ProductCodeAndTotalsProvider()
    {
        var scenarios = new ProductCodesAndTotalScenario[]
        {
            new(
                Pricing: TestPricing,
                ProductCodes: new[] { "A", "B", "C", "D", "A", "B", "A" },
                ExpectedTotal: 13.25m
            ),
            new(
                Pricing: TestPricing,
                ProductCodes: new[] { "C", "C", "C", "C", "C", "C", "C" },
                ExpectedTotal: 6m
            ),
            new(
                Pricing: TestPricing,
                ProductCodes: new[] { "A", "B", "C", "D" },
                ExpectedTotal: 7.25m
            ),
            new(
                Pricing: TestPricing,
                ProductCodes: new[] { "B", "B", "B", "B", "B" },
                ExpectedTotal: 21.25m
            ),
            new(
                Pricing: TestPricing,
                ProductCodes: new[] { "B", "B", "B", "D", "D", "D" },
                ExpectedTotal: 15m
            ),
            new(
                Pricing: TestPricing,
                ProductCodes: new[] { "D" },
                ExpectedTotal: 0.75m
            ),
            new(
                Pricing: TestPricing,
                ProductCodes: Array.Empty<string>(),
                ExpectedTotal: 0
            ),
        };

        foreach (var scenario in scenarios)
            yield return new object[] { scenario };
    }

    private static readonly IProductPrice[] TestPricing =
    {
        SingleAndBulkUnitPrice.Create(
            productCode: "A",
            singleUnitPrice: 1.25m,
            bulkUnitSize: 3,
            bulkUnitPrice: 3m
        ),
        SingleUnitPriceBuilder.Build(
            withProductCode: "B",
            withUnitPrice: 4.25m
        ),
        SingleAndBulkUnitPrice.Create(
            productCode: "C",
            singleUnitPrice: 1m,
            bulkUnitSize: 6,
            bulkUnitPrice: 5m
        ),
        SingleUnitPriceBuilder.Build(
            withProductCode: "D",
            withUnitPrice: 0.75m
        ),
    };

    public record ProductCodesAndTotalScenario(
        IProductPrice[] Pricing,
        string[] ProductCodes,
        decimal ExpectedTotal
    )
    {
        public override string ToString() =>
            (!ProductCodes.Any() ? "none" : string.Join(',', ProductCodes)) +
            $" - expecting {ExpectedTotal}";
    }
}