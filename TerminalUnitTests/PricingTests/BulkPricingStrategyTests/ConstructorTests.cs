using System;
using TerminalUnitTests.Builders;

namespace TerminalUnitTests.PricingTests.BulkPricingStrategyTests;

public class ConstructorTests
{
    [Fact]
    public void ThrowsWhenDuplicateBulkProductCodePresentInPricing()
    {
        // Arrange
        var pricingWithDuplicates =
            new[]
            {
                BulkProductBuilder.Build(withProductCode: "Foo"),
                BulkProductBuilder.Build(withProductCode: "Bar"),
                BulkProductBuilder.Build(withProductCode: "Bar"),
            };

        // Act
        var actual = () => BulkPricingStrategyBuilder.Build(
            withBulkProductPricing: pricingWithDuplicates
        );

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}