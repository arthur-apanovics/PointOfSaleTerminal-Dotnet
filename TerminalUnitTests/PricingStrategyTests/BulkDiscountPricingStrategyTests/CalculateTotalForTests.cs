using System;
using Terminal.Models;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.PricingStrategyTests.
    BulkDiscountPricingStrategyTests;

public class CalculateTotalForTests
{
    [Theory]
    [MemberData(
        nameof(BulkPricingProviders.BulkProductCodesAndTotals),
        MemberType = typeof(BulkPricingProviders)
    )]
    public void CalculatesExpectedTotalsForProductCodeSequences(
        ProductPrice[] pricing,
        BulkProductPrice[] bulkPricing,
        string[] productCodes,
        decimal expectedTotal
    )
    {
        // Arrange
        var strategy = BulkDiscountPricingStrategyBuilder.Build(
            withPricingStrategy: StandardPricingStrategyBuilder.Build(
                withProductPricing: pricing
            ),
            withBulkProductPricing: bulkPricing
        );

        // Act
        var actualTotal = strategy.CalculateTotalFor(productCodes);

        // Assert
        actualTotal.Should().Be(expectedTotal);
    }

    [Theory]
    [MemberData(
        nameof(BulkPricingProviders.BulkProductQuantityAndTotals),
        MemberType = typeof(BulkPricingProviders)
    )]
    public void CalculatesExpectedTotalsForSpecifiedProductQuantities(
        ProductPrice[] pricing,
        BulkProductPrice[] bulkPricing,
        string productCode,
        int productQuantity,
        decimal expectedTotal
    )
    {
        // Arrange
        var strategy = BulkDiscountPricingStrategyBuilder.Build(
            withPricingStrategy: StandardPricingStrategyBuilder.Build(
                withProductPricing: pricing
            ),
            withBulkProductPricing: bulkPricing
        );

        // Act
        var actualTotal =
            strategy.CalculateTotalFor(productCode, productQuantity);

        // Assert
        actualTotal.Should().Be(expectedTotal);
    }

    [Fact]
    public void ThrowsWhenInvalidQuantityProvided()
    {
        // Arrange
        const string productCode = "X";
        var strategy = BulkDiscountPricingStrategyBuilder.Build(
            withPricingStrategy: StandardPricingStrategyBuilder.Build(
                withProductPricing: new[]
                {
                    ProductPriceBuilder.Build(productCode)
                }
            )
        );

        // Act
        var actual = () => strategy.CalculateTotalFor(
            productCode: productCode,
            productQuantity: -1
        );

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}