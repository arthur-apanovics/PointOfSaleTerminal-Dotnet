using System;
using System.Collections.Generic;
using Terminal.Models;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;

namespace TerminalUnitTests.PricingStrategyTests.StandardPricingStrategyTests;

public class CalculateTotalForTests
{
    [Theory]
    [MemberData(nameof(ProductCodeSequenceProvider))]
    public void CalculatesExpectedTotalsForProductCodeSequences(
        ProductPrice[] pricing,
        string[] productCodes,
        decimal expectedTotal
    )
    {
        // Arrange
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: pricing
        );

        // Act
        var actualTotal = strategy.CalculateTotalFor(productCodes);

        // Assert
        actualTotal.Should().Be(expectedTotal);
    }

    [Theory]
    [MemberData(nameof(ProductCodeQuantityProvider))]
    public void CalculatesExpectedTotalsForSpecifiedProductQuantities(
        ProductPrice[] pricing,
        string productCode,
        int productQuantity,
        decimal expectedTotal
    )
    {
        // Arrange
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: pricing
        );

        // Act
        var actualTotal = strategy.CalculateTotalFor(productCode, productQuantity);

        // Assert
        actualTotal.Should().Be(expectedTotal);
    }

    [Fact]
    public void ThrowsWhenInvalidQuantityProvided()
    {
        // Arrange
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[] { ProductPriceBuilder.Build("X") }
        );

        // Act
        var actual = () => strategy.CalculateTotalFor(productCode: "X", productQuantity: -1);

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }

    public static IEnumerable<object[]> ProductCodeSequenceProvider()
    {
        var pricing = new[]
        {
            ProductPriceBuilder.Build(withCode: "A", withPrice: 1.25m),
            ProductPriceBuilder.Build(withCode: "B", withPrice: 4.25m),
            ProductPriceBuilder.Build(withCode: "C", withPrice: 1m),
            ProductPriceBuilder.Build(withCode: "D", withPrice: 0.75m)
        };

        var codesAndTotals =
            new (string[] ProductCodes, decimal ExpectedTotal)[]
            {
                (Array.Empty<string>(), 0),
                (new[] { "A", "A", "A", "A" }, 5),
                (new[] { "A", "B", "C", "D" }, 7.25m),
                (new[] { "A", "A", "A", "C", "C", "C", "D", "B", "B" },
                    16m),
            };

        foreach (var (codes, total) in codesAndTotals)
            yield return new object[] { pricing, codes, total };
    }

    public static IEnumerable<object[]> ProductCodeQuantityProvider()
    {
        var pricing = new[]
        {
            ProductPriceBuilder.Build(withCode: "A", withPrice: 1.25m),
            ProductPriceBuilder.Build(withCode: "B", withPrice: 4.25m),
        };

        var productQtyTotals =
            new (string ProductCode, int ProductQty, decimal ExpectedTotal)[]
            {
                ("A", 5, 6.25m),
                ("B", 3, 12.75m),
                ("B", 128, 544m),
                ("A", 0, 0m),
            };

        foreach (var (code, qty, total) in productQtyTotals)
            yield return new object[] { pricing, code, qty, total };
    }
}