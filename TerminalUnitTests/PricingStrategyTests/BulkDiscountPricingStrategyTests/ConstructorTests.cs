using System;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;

namespace TerminalUnitTests.PricingStrategyTests.BulkDiscountPricingStrategyTests;

public class ConstructorTests
{
    [Fact]
    public void ThrowsWhenDuplicateBulkProductCodePresentInPricing()
    {
        // Arrange
        var pricingWithDuplicates = new[]
        {
            BulkProductPriceBuilder.Build(withProductCode: "Foo"),
            BulkProductPriceBuilder.Build(withProductCode: "Bar"),
            BulkProductPriceBuilder.Build(withProductCode: "Bar"),
        };

        // Act
        var actual = () => BulkDiscountPricingStrategyBuilder.Build(
            withBulkProductPricing: pricingWithDuplicates
        );

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}