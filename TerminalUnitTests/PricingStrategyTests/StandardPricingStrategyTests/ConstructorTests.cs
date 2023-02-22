using System;
using Terminal.Models;
using TerminalUnitTests.Builders;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;

namespace TerminalUnitTests.PricingTests.StandardPricingStrategyTests;

public class ConstructorTests
{
    [Fact]
    public void ThrowsWhenEmptyPricingProvided()
    {
        // Arrange
        var emptyPricing = Array.Empty<Product>();

        // Act
        var actual = () => StandardPricingStrategyBuilder.Build(
            withProductPricing: emptyPricing
        );

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void ThrowsWhenDuplicateProductCodePresentInPricing()
    {
        // Arrange
        var pricingWithDuplicates = new[]
        {
            ProductBuilder.Build(withCode: "A", withPrice: 1.25m),
            ProductBuilder.Build(withCode: "B", withPrice: 4.25m),
            ProductBuilder.Build(withCode: "B", withPrice: 1m)
        };

        // Act
        var actual = () => StandardPricingStrategyBuilder.Build(
            withProductPricing: pricingWithDuplicates
        );

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}