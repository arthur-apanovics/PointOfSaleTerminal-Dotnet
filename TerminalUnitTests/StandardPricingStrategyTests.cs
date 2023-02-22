using System;
using TerminalUnitTests.Builders;

namespace TerminalUnitTests;

public class StandardPricingStrategyTests
{
    [Fact]
    public void HasPricing_WithValidArgument_ReturnsTrue()
    {
        var sut = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[]
            {
                ProductBuilder.Build(withCode: "Q", withPrice: 1m)
            }
        );

        Assert.True(sut.HasPricing("Q"));
    }

    [Fact]
    public void GetPrice_WithSingleProduct_ReturnsPriceForProductCode()
    {
        const decimal expected = 0.001m;
        const string code = "W";
        var sut = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[]
            {
                ProductBuilder.Build(withCode: code, withPrice: expected)
            }
        );

        Assert.Equal(expected, sut.GetPrice(code));
    }

    [Theory]
    [InlineData("A,A,A,A", 5)]
    [InlineData("A,B,C,D", 7.25)]
    [InlineData("A,A,A,C,C,C,D,B,B", 16)]
    [InlineData("", 0)]
    public void CalculateTotal_MixedCodes_CalculatesTotal(
        string sequence,
        decimal expected
    )
    {
        var sut = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[]
            {
                ProductBuilder.Build(withCode: "A", withPrice: 1.25m),
                ProductBuilder.Build(withCode: "B", withPrice: 4.25m),
                ProductBuilder.Build(withCode: "C", withPrice: 1m),
                ProductBuilder.Build(withCode: "D", withPrice: 0.75m)
            }
        );

        var codes = sequence.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var actual = sut.CalculateTotal(codes);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("A", 5, 6.25)]
    [InlineData("B", 3, 12.75)]
    [InlineData("B", 128, 544)]
    [InlineData("A", 0, 0)]
    public void CalculateTotal_CodeQuantities_CalculatesTotal(
        string code,
        int quantity,
        decimal expected
    )
    {
        var sut = StandardPricingStrategyBuilder.Build(
            withProductPricing: new[]
            {
                ProductBuilder.Build(withCode: "A", withPrice: 1.25m),
                ProductBuilder.Build(withCode: "B", withPrice: 4.25m),
            }
        );

        var actual = sut.CalculateTotal(code, quantity);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateTotal_InvalidQuantity_ThrowsException()
    {
        var sut = () =>
        {
            var strategy = StandardPricingStrategyBuilder.Build();
            strategy.CalculateTotal("X", -1);
        };

        Assert.Throws<ArgumentException>(sut);
    }
}