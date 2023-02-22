using System;
using System.Collections.Generic;
using Terminal.Models;
using TerminalUnitTests.Builders;

namespace TerminalUnitTests.PricingTests.StandardPricingStrategyTests;

public class CalculateTotalTests
{
    [Theory]
    [MemberData(nameof(ProductCodeSequenceProvider))]
    public void CalculatesExpectedTotalsForProductCodeSequences(
        Product[] pricing,
        string[] productCodes,
        decimal expectedTotal
    )
    {
        // Arrange
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: pricing
        );

        // Act
        var actualTotal = strategy.CalculateTotal(productCodes);

        // Assert
        actualTotal.Should().Be(expectedTotal);
    }

    public static IEnumerable<object[]> ProductCodeSequenceProvider()
    {
        var pricing = new[]
        {
            ProductBuilder.Build(withCode: "A", withPrice: 1.25m),
            ProductBuilder.Build(withCode: "B", withPrice: 4.25m),
            ProductBuilder.Build(withCode: "C", withPrice: 1m),
            ProductBuilder.Build(withCode: "D", withPrice: 0.75m)
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
            ProductBuilder.Build(withCode: "A", withPrice: 1.25m),
            ProductBuilder.Build(withCode: "B", withPrice: 4.25m),
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

    [Theory]
    [MemberData(nameof(ProductCodeQuantityProvider))]
    public void CalculateTotal_CodeQuantities_CalculatesTotal(
        Product[] pricing,
        string productCode,
        int productQuantity,
        decimal expectedTotal
    )
    {
        // Arrange
        var sut = StandardPricingStrategyBuilder.Build(
            withProductPricing: pricing
        );

        // Act
        var actualTotal = sut.CalculateTotal(productCode, productQuantity);

        // Assert
        actualTotal.Should().Be(expectedTotal);
    }

    [Fact]
    public void ThrowsWhenInvalidQuantityProvided()
    {
        // Arrange
        var strategy = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[] { ProductBuilder.Build("X") }
        );

        // Act
        var actual = () => strategy.CalculateTotal(code: "X", quantity: -1);

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}