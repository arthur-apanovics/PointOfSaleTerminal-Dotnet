using Terminal.Models;

namespace TerminalUnitTests.Builders.Models;

public static class BulkPriceBuilder
{
    public static BulkProductPrice Build(
        int? withThreshold = null,
        decimal? withPrice = null
    ) =>
        BulkProductPrice.Create(withThreshold ?? 3, withPrice ?? 3m);
}