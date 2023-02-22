using Terminal.Models;

namespace TerminalUnitTests.Builders.Models;

public static class ProductPriceBuilder
{
    public static ProductPrice Build(
        string? withCode = null,
        decimal? withPrice = null
    ) =>
        ProductPrice.Create(withCode ?? "A", withPrice ?? 1.25m);
}