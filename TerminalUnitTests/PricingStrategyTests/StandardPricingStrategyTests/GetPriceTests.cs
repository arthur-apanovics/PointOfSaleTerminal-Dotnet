using System;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;

namespace TerminalUnitTests.PricingStrategyTests.StandardPricingStrategyTests;

public class GetPriceTests
{
    [Fact]
    public void ReturnsExpectedProductPrice()
    {
        // Arrange
        const decimal expectedPrice = 0.001m;
        const string productCode = "W";

        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[]
            {
                ProductPriceBuilder.Build(
                    withCode: productCode,
                    withPrice: expectedPrice
                )
            }
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
            withProductPricing: new[] { ProductPriceBuilder.Build(withCode: "Foo") }
        );

        // Act
        var actual = () => strategy.GetPrice("Bar");

        // Assert
        actual.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
}