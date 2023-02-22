using System.Collections.Generic;
using Terminal.Services;

namespace TerminalUnitTests.Builders.Services;

public static class PricingServiceBuilder
{
    private static readonly Dictionary<string, decimal> DefaultPricing = new()
    {
        { "A", 1.25m }, { "B", 4.25m }, { "C", 1m }, { "D", 0.75m },
    };

    public static PricingService Build(
        Dictionary<string, decimal>? withPrices = null
    ) =>
        new(withPrices ?? DefaultPricing);

    public static PricingService BuildWithSinglePrice(
        string? withProductCode = null,
        decimal? withProductPrice = null
    ) =>
        Build(
            withPrices: new Dictionary<string, decimal>
            {
                { withProductCode ?? "Foo", withProductPrice ?? 3m }
            }
        );
}