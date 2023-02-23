using TerminalUnitTests.Builders;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.Services;

namespace TerminalUnitTests.PointOfSaleTerminalTests;

public class SetPricingTests
{
    [Fact]
    public void DoesNotThrowAfterPricingIsSet()
    {
        // Arrange
        const string productCode = "ZYX";
        var terminal = PointOfSaleTerminalBuilder.Build();
        var pricingService = PricingServiceBuilder.Build(
            withPrice: SingleUnitPriceBuilder.Build(
                withProductCode: productCode
            )
        );

        // Act
        terminal.SetPricing(pricingService);
        var actual = () => { terminal.ScanProduct(productCode); };

        // Assert
        actual.Should().NotThrow();
    }
}