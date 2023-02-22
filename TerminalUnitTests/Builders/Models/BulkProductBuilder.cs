using Terminal.Models;

namespace TerminalUnitTests.Builders.Models;

public static class BulkProductBuilder
{
    public static BulkProduct Build(
        string? withProductCode = null,
        BulkProductPrice? withBulkPrice = null
    ) =>
        BulkProduct.Create(
            withProductCode ?? "A",
            withBulkPrice ?? BulkPriceBuilder.Build()
        );
}