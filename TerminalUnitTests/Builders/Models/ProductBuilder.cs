using Terminal.Models;

namespace TerminalUnitTests.Builders.Models;

public static class ProductBuilder
{
    public static Product Build(
        string? withCode = null,
        decimal? withPrice = null
    ) =>
        Product.Create(withCode ?? "A", withPrice ?? 1.25m);
}