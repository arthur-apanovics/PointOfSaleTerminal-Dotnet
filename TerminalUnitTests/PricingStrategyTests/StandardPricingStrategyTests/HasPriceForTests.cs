using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;

namespace TerminalUnitTests.PricingStrategyTests.StandardPricingStrategyTests;

public class HasPriceForTests
{
    [Fact]
    public void ReturnsTrueWhenProductPriceExists()
    {
        // Arrange
        const string productCode = "Q";
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[]
            {
                ProductPriceBuilder.Build(withCode: productCode)
            }
        );

        // Act
        var actual = strategy.HasPriceFor(productCode);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void ReturnsFalseWhenProductPriceDoesNotExist()
    {
        // Arrange
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[] { ProductPriceBuilder.Build("Foo") }
        );

        // Act
        var actual = strategy.HasPriceFor(productCode: "Bar");

        // Assert
        actual.Should().BeFalse();
    }
}