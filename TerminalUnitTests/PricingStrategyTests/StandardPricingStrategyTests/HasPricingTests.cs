using TerminalUnitTests.Builders;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;

namespace TerminalUnitTests.PricingTests.StandardPricingStrategyTests;

public class HasPricingTests
{
    [Fact]
    public void ReturnsTrueWhenProductPriceExists()
    {
        // Arrange
        const string productCode = "Q";
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[]
            {
                ProductBuilder.Build(withCode: productCode)
            }
        );

        // Act
        var actual = strategy.HasPricing(productCode);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void ReturnsFalseWhenProductPriceDoesNotExist()
    {
        // Arrange
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[] { ProductBuilder.Build("Foo") }
        );

        // Act
        var actual = strategy.HasPricing(code: "Bar");

        // Assert
        actual.Should().BeFalse();
    }
}