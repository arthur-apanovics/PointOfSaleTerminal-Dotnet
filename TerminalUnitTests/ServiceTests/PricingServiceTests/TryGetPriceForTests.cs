using TerminalUnitTests.Builders.Services;

namespace TerminalUnitTests.ServiceTests.PricingServiceTests;

public class TryGetPriceForTests
{
    [Fact]
    public void ReturnsTrueAndExpectedPriceWhenProductPriceExists()
    {
        // Arrange
        const string productCode = "XYZ";
        const decimal expectedPrice = 10.45m;
        var service = PricingServiceBuilder.BuildWithSinglePrice(
            withProductCode: productCode,
            withProductPrice: expectedPrice
        );

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
        var service =
            PricingServiceBuilder.BuildWithSinglePrice(withProductCode: "Foo");

        // Act
        var hasPrice = service.TryGetPriceFor("Baz", out var actualPrice);

        // Assert
        hasPrice.Should().BeFalse();
        actualPrice.Should().BeNull();
    }
}