using Terminal.Models;

namespace TerminalUnitTests.Builders.Models;

public static class SingleAndBulkUnitPriceBuilder
{
    public static SingleAndBulkUnitPrice Build(
        string? withProductCode = null,
        decimal? withSingleUnitPrice = null,
        int? withBulkUnitSize = null,
        decimal? withBulkUnitPrice = null
    ) =>
        SingleAndBulkUnitPrice.Create(
            withProductCode ?? "FooBar",
            withSingleUnitPrice ?? 1m,
            withBulkUnitSize ?? 3,
            withBulkUnitPrice ?? 2m
        );
}