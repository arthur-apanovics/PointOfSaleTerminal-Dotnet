using Terminal.Models;

namespace TerminalUnitTests.Builders.Models;

public static class BulkProductPriceBuilder
{
    public static BulkProductPrice Build(
        string? withProductCode = null,
        int? withBulkThreshold = null,
        decimal? withBulkPrice = null
    ) =>
        BulkProductPrice.Create(
            withProductCode ?? "Foo",
            withBulkThreshold ?? 3,
            withBulkPrice ?? 3m
        );
}