using System.Collections.Generic;
using Terminal.Models;
using Terminal.PricingStrategies;
using TerminalUnitTests.Builders.Models;

namespace TerminalUnitTests.Builders.PricingStrategies;

public static class BulkDiscountPricingStrategyBuilder
{
    private static readonly BulkProductPrice[] DefaultBulkProductPricing =
    {
        BulkProductPriceBuilder.Build(
            withProductCode: "A",
            withBulkThreshold: 3,
            withBulkPrice: 3m
        ),
        BulkProductPriceBuilder.Build(
            withProductCode: "C",
            withBulkThreshold: 6,
            withBulkPrice: 5m
        ),
    };

    public static BulkDiscountPricingStrategy Build(
        IPricingStrategy? withPricingStrategy = null,
        IEnumerable<BulkProductPrice>? withBulkProductPricing = null
    ) =>
        new(
            withPricingStrategy ?? StandardPricingStrategyBuilder.Build(),
            withBulkProductPricing ?? DefaultBulkProductPricing
        );
}