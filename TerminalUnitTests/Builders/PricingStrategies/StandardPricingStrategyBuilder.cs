using System.Collections.Generic;
using Terminal.Models;
using Terminal.PricingStrategies;
using TerminalUnitTests.Builders.Models;

namespace TerminalUnitTests.Builders.PricingStrategies;

public static class StandardPricingStrategyBuilder
{
    private static readonly ProductPrice[] DefaultPricing =
    {
        ProductPriceBuilder.Build(withCode: "A", withPrice: 1.25m),
        ProductPriceBuilder.Build(withCode: "B", withPrice: 4.25m),
        ProductPriceBuilder.Build(withCode: "C", withPrice: 1m),
        ProductPriceBuilder.Build(withCode: "D", withPrice: 0.75m)
    };

    public static StandardPricingStrategy Build(
        IEnumerable<ProductPrice>? withProductPricing = null
    ) =>
        new(withProductPricing ?? DefaultPricing);
}