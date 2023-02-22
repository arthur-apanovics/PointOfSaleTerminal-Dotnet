using System.Collections.Generic;
using Terminal.Models;
using Terminal.Pricing;

namespace TerminalUnitTests.Builders;

public static class BulkPricingStrategyBuilder
{
    private static readonly Product[] DefaultProductPricing =
    {
        ProductBuilder.Build(withCode: "A", withPrice: 1.25m),
        ProductBuilder.Build(withCode: "B", withPrice: 4.25m),
        ProductBuilder.Build(withCode: "C", withPrice: 1m),
        ProductBuilder.Build(withCode: "D", withPrice: 0.75m)
    };

    private static readonly BulkProduct[] DefaultBulkProductPricing =
    {
        BulkProductBuilder.Build(
            withProductCode: "A",
            BulkPriceBuilder.Build(withThreshold: 3, withPrice: 3m)
        ),
        BulkProductBuilder.Build(
            withProductCode: "C",
            BulkPriceBuilder.Build(withThreshold: 6, withPrice: 5m)
        ),
    };

    public static BulkPricingStrategy Build(
        IEnumerable<Product>? withProductPricing = null,
        IEnumerable<BulkProduct>? withBulkProductPricing = null
    ) =>
        new(
            withProductPricing ?? DefaultProductPricing,
            withBulkProductPricing ?? DefaultBulkProductPricing
        );
}