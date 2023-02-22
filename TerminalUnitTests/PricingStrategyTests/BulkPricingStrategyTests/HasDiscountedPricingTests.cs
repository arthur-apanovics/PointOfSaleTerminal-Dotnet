using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;

namespace TerminalUnitTests.PricingStrategyTests.BulkPricingStrategyTests;

public class HasDiscountedPricingTests
{
    [Fact]
    public void ReturnsTrueWhenDiscountedProductPriceExists()
    {
        // Arrange
        const string productCode = "Q";
        var strategy = BulkPricingStrategyBuilder.Build(
            withBulkProductPricing: new[]
            {
                BulkProductBuilder.Build(
                    withProductCode: productCode,
                    BulkPriceBuilder.Build()
                )
            }
        );

        // Act
        var actual = strategy.HasDiscountedPricing(productCode);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void ReturnsFalseWhenDiscountedProductPriceDoesNotExist()
    {
        // Arrange
        var strategy = BulkPricingStrategyBuilder.Build(
            withBulkProductPricing: new[]
            {
                BulkProductBuilder.Build(withProductCode: "Foo")
            }
        );

        // Act
        var actual = strategy.HasDiscountedPricing(code: "Bar");

        // Assert
        actual.Should().BeFalse();
    }
}