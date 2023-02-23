using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;

namespace TerminalUnitTests.PricingStrategyTests.
    BulkDiscountPricingStrategyTests;

public class HasDiscountedPricingTests
{
    [Fact]
    public void ReturnsTrueWhenDiscountedProductPriceExists()
    {
        // Arrange
        const string productCode = "Q";
        var strategy = BulkDiscountPricingStrategyBuilder.Build(
            withBulkProductPricing: new[]
            {
                BulkProductPriceBuilder.Build(withProductCode: productCode)
            }
        );

        // Act
        var actual = strategy.HasDiscountedPricingFor(productCode);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void ReturnsFalseWhenDiscountedProductPriceDoesNotExist()
    {
        // Arrange
        var strategy = BulkDiscountPricingStrategyBuilder.Build(
            withBulkProductPricing: new[]
            {
                BulkProductPriceBuilder.Build(withProductCode: "Foo")
            }
        );

        // Act
        var actual = strategy.HasDiscountedPricingFor(productCode: "Bar");

        // Assert
        actual.Should().BeFalse();
    }
}