using TerminalUnitTests.Builders;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.Services;

namespace TerminalUnitTests.PointOfSaleTerminalTests;

public class ScanProductTests
{
    [Fact]
    public void DoesNotThrowWhenPricePresentForProductCode()
    {
        // Arrange
        const string productCode = "YumYum";
        var terminal = PointOfSaleTerminalBuilder.Build(
            withPricingService: PricingServiceBuilder.Build(
                withPrice: SingleUnitPriceBuilder.Build(
                    withProductCode: productCode
                )
            )
        );

        // Act
        var actual = () => terminal.ScanProduct(productCode);

        // Assert
        actual.Should().NotThrow();
    }
}