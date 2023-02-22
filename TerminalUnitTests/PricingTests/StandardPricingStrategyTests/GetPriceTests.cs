using System;
using TerminalUnitTests.Builders;

namespace TerminalUnitTests.PricingTests.StandardPricingStrategyTests;

public class GetPriceTests
{
    [Fact]
    public void ReturnsExpectedProductPrice()
    {
        // Arrange
        const decimal expectedPrice = 0.001m;
        const string productCode = "W";

        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: ProductBuilder.Build(
                withCode: productCode,
                withPrice: expectedPrice
            )
        );

        // Act
        var actualPrice = strategy.GetPrice(productCode);

        // Assert
        actualPrice.Should().Be(expectedPrice);
    }

    [Fact]
    public void ThrowsWhenNoPriceExistsForProduct()
    {
        // Arrange
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: ProductBuilder.Build(withCode: "Foo")
        );

        // Act
        var actual = () => strategy.GetPrice("Bar");

        // Assert
        actual.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
}