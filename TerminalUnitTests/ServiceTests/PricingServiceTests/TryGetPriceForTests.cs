using Terminal.Models;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.Services;

namespace TerminalUnitTests.ServiceTests.PricingServiceTests;

public class TryGetPriceForTests
{
    [Fact]
    public void ReturnsTrueAndExpectedPriceWhenProductPriceExists()
    {
        // Arrange
        const string productCode = "XYZ";
        var expectedPrice =
            SingleUnitPriceBuilder.Build(withProductCode: productCode);
        var service = PricingServiceBuilder.Build(withPricing: expectedPrice);

        // Act
        var hasPrice = service.TryGetPriceFor(productCode, out var actualPrice);

        // Assert
        hasPrice.Should().BeTrue();
        actualPrice.Should().Be(expectedPrice);
    }

    [Fact]
    public void ReturnsFalseAndNullWhenProductPriceDoesNotExist()
    {
        // Arrange
        var service = PricingServiceBuilder.Build(
            withPricing: SingleUnitPriceBuilder.Build(withProductCode: "Foo")
        );

        // Act
        var hasPrice = service.TryGetPriceFor("Baz", out var actualPrice);

        // Assert
        hasPrice.Should().BeFalse();
        actualPrice.Should().BeNull();
    }
}