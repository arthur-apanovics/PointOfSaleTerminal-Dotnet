using System.Collections.Generic;
using Terminal.Models;
using Terminal.Pricing;

namespace TerminalUnitTests.Builders;

public static class StandardPricingStrategyBuilder
{
    private static readonly Product[] DefaultPricing =
    {
        ProductBuilder.Build(withCode: "A", withPrice: 1.25m),
        ProductBuilder.Build(withCode: "B", withPrice: 4.25m),
        ProductBuilder.Build(withCode: "C", withPrice: 1m),
        ProductBuilder.Build(withCode: "D", withPrice: 0.75m)
    };

    public static StandardPricingStrategy Build(
        IEnumerable<Product>? withProductPricing = null
    ) =>
        new(withProductPricing ?? DefaultPricing);
}