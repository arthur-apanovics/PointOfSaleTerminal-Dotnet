using Terminal.Models;

namespace TerminalUnitTests.Builders;

public static class BulkPriceBuilder
{
    public static BulkPrice Build(
        int? withThreshold = null,
        decimal? withPrice = null
    ) =>
        new(withThreshold ?? 3, withPrice ?? 3m);
}