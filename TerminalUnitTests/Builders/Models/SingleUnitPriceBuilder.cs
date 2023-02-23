using Terminal.Models;

namespace TerminalUnitTests.Builders.Models;

public static class SingleUnitPriceBuilder
{
    public static SingleUnitPrice Build(
        string? withProductCode = null,
        decimal? withUnitPrice = null
    ) =>
        SingleUnitPrice.Create(
            withProductCode ?? "Foo",
            withUnitPrice ?? 0.01m
        );
}