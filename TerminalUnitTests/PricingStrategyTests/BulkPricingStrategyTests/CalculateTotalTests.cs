using System;
using Terminal.Models;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.PricingStrategyTests.BulkPricingStrategyTests;

public class CalculateTotalTests
{
    [Theory]
    [MemberData(
        nameof(BulkPricingProviders.BulkProductCodesAndTotals),
        MemberType = typeof(BulkPricingProviders)
    )]
    public void CalculatesExpectedTotalsForProductCodeSequences(
        Product[] pricing,
        BulkProduct[] bulkPricing,
        string[] productCodes,
        decimal expectedTotal
    )
    {
        // Arrange
        var strategy = BulkPricingStrategyBuilder.Build(
            withPricingStrategy: StandardPricingStrategyBuilder.Build(
                withProductPricing: pricing
            ),
            withBulkProductPricing: bulkPricing
        );

        // Act
        var actualTotal = strategy.CalculateTotal(productCodes);

        // Assert
        actualTotal.Should().Be(expectedTotal);
    }

    [Theory]
    [MemberData(
        nameof(BulkPricingProviders.BulkProductQuantityAndTotals),
        MemberType = typeof(BulkPricingProviders)
    )]
    public void CalculatesExpectedTotalsForSpecifiedProductQuantities(
        Product[] pricing,
        BulkProduct[] bulkPricing,
        string productCode,
        int productQuantity,
        decimal expectedTotal
    )
    {
        // Arrange
        var strategy = BulkPricingStrategyBuilder.Build(
            withPricingStrategy: StandardPricingStrategyBuilder.Build(
                withProductPricing: pricing
            ),
            withBulkProductPricing: bulkPricing
        );

        // Act
        var actualTotal = strategy.CalculateTotal(productCode, productQuantity);

        // Assert
        actualTotal.Should().Be(expectedTotal);
    }

    [Fact]
    public void ThrowsWhenInvalidQuantityProvided()
    {
        // Arrange
        const string productCode = "X";
        var strategy = BulkPricingStrategyBuilder.Build(
            withPricingStrategy: StandardPricingStrategyBuilder.Build(
                withProductPricing: new[] { ProductBuilder.Build(productCode) }
            )
        );

        // Act
        var actual = () =>
            strategy.CalculateTotal(code: productCode, quantity: -1);

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}