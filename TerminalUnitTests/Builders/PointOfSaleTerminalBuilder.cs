using Terminal;
using Terminal.Services;

namespace TerminalUnitTests.Builders;

public static class PointOfSaleTerminalBuilder
{
    public static PointOfSaleTerminal Build(
        IPricingService? withPricingService = null
    )
    {
        var pos = new PointOfSaleTerminal();

        if (withPricingService is not null)
            pos.SetPricing(withPricingService);

        return pos;
    }
}