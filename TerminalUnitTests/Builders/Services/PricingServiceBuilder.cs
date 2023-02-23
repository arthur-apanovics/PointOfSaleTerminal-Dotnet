using System.Collections.Generic;
using Terminal.Models;
using Terminal.Services;
using TerminalUnitTests.Builders.Models;

namespace TerminalUnitTests.Builders.Services;

public static class PricingServiceBuilder
{
    private static readonly IProductPricing[] DefaultPricing =
    {
        SingleAndBulkUnitPriceBuilder.Build(
            withProductCode: "A",
            withSingleUnitPrice: 1.25m,
            withBulkUnitSize: 3,
            withBulkUnitPrice: 3m
        ),
        SingleUnitPriceBuilder.Build(
            withProductCode: "B",
            withUnitPrice: 4.25m
        ),
        SingleAndBulkUnitPriceBuilder.Build(
            withProductCode: "C",
            withSingleUnitPrice: 1m,
            withBulkUnitSize: 6,
            withBulkUnitPrice: 5m
        ),
        SingleUnitPriceBuilder.Build(
            withProductCode: "D",
            withUnitPrice: 0.75m
        ),
    };

    public static PricingService Build(
        IEnumerable<IProductPricing>? withPricing = null
    ) =>
        new(withPricing ?? DefaultPricing);

    public static PricingService Build(IProductPricing withPricing) =>
        new(new[] { withPricing });
}