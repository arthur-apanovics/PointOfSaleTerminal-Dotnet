using Terminal.Models;

namespace TerminalUnitTests.Builders.Models;

public static class SingleUnitPriceBuilder
{
    public static SingleUnitPricing Build(
        string? withProductCode = null,
        decimal? withUnitPrice = null
    ) =>
        SingleUnitPricing.Create(
            withProductCode ?? "Foo",
            withUnitPrice ?? 0.01m
        );
}