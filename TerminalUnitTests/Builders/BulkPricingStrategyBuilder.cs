using System.Collections.Generic;
using Terminal.Contracts;
using Terminal.Models;
using Terminal.Pricing;

namespace TerminalUnitTests.Builders;

public static class BulkPricingStrategyBuilder
{
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
        IPricingStrategy? withPricingStrategy = null,
        IEnumerable<BulkProduct>? withBulkProductPricing = null
    ) =>
        new(
            withPricingStrategy ?? StandardPricingStrategyBuilder.Build(),
            withBulkProductPricing ?? DefaultBulkProductPricing
        );
}