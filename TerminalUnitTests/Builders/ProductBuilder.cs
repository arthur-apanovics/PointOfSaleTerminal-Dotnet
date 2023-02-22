using Terminal.Models;

namespace TerminalUnitTests.Builders;

public static class ProductBuilder
{
    public static Product Build(
        string? withCode = null,
        decimal? withPrice = null
    ) =>
        new(withCode ?? "A", withPrice ?? 1.25m);
}