using System;
using System.Collections.Generic;
using Terminal.Models;
using TerminalUnitTests.Builders;
using Xunit;

namespace TerminalUnitTests;

public class StandardPricingStrategyTests
{
    [Fact]
    public void Validation_WithEmptyPricing_ThrowsException()
    {
        var sut = () => { new StandardPricingBuilder().Build(); };

        Assert.Throws<ArgumentException>(sut);
    }

    [Fact]
    public void Validation_DuplicateProductCodes_ThrowsException()
    {
        var sut = () =>
        {
            var pricing = new[]
            {
                new Product("A", 1.25m),
                new Product("B", 4.25m),
                new Product("B", 1m)
            };

            new StandardPricingBuilder().WithPricing(pricing).Build();
        };

        Assert.Throws<ArgumentException>(sut);
    }

    [Fact]
    public void HasPricing_WithValidArgument_ReturnsTrue()
    {
        var sut = new StandardPricingBuilder().WithPricing("Q", 1m).Build();

        Assert.True(sut.HasPricing("Q"));
    }

    [Fact]
    public void GetPrice_WithSingleProduct_ReturnsPriceForProductCode()
    {
        const decimal expected = 0.001m;
        const string code = "W";
        var sut = new StandardPricingBuilder()
            .WithPricing(new Product(code, expected))
            .Build();

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
        var pricing = new List<Product>
        {
            new("A", 1.25m), new("B", 4.25m), new("C", 1m), new("D", 0.75m)
        };
        var sut = new StandardPricingBuilder().WithPricing(pricing).Build();

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
        var pricing = new List<Product> { new("A", 1.25m), new("B", 4.25m) };
        var sut = new StandardPricingBuilder().WithPricing(pricing).Build();

        var actual = sut.CalculateTotal(code, quantity);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateTotal_InvalidQuantity_ThrowsException()
    {
        var sut = () =>
        {
            var strategy =
                new StandardPricingBuilder().WithValidPricing().Build();
            strategy.CalculateTotal("X", -1);
        };

        Assert.Throws<ArgumentException>(sut);
    }
}