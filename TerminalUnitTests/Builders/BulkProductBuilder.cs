using Terminal.Models;

namespace TerminalUnitTests.Builders;

public static class BulkProductBuilder
{
    public static BulkProduct Build(
        string? withProductCode = null,
        BulkProductPrice? withBulkPrice = null
    ) =>
        new(withProductCode ?? "A", withBulkPrice ?? BulkPriceBuilder.Build());
}