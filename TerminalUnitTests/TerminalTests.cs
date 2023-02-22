using System;
using Terminal;
using Terminal.Models;
using Terminal.PricingStrategies;
using TerminalUnitTests.Builders;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.Builders.PricingStrategies;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests;

public class TerminalTests
{
    [Fact]
    public void Terminal_NoPricingStrategySet_ThrowsException()
    {
        var sut = () =>
        {
            var terminal = new PointOfSaleTerminal();
            terminal.ScanProduct("X");
        };

        Assert.Throws<NullReferenceException>(sut);
    }

    [Fact]
    public void ScanProduct_SingleProduct_ThrowsWhenCodeNotInPricingList()
    {
        var sut = () =>
        {
            var sut = new TerminalBuilder().WithSingleProduct("A", 1.25m)
                .Build();

            sut.ScanProduct("X");
        };

        Assert.Throws<ArgumentException>(sut);
    }

    [Theory]
    [MemberData(
        nameof(BulkPricingProviders.BulkProductCodesAndTotals),
        MemberType = typeof(BulkPricingProviders)
    )]
    public void CalculateTotal_WithBulkDiscount_CalculatesTotal(
        ProductPrice[] pricing,
        BulkProductPrice[] bulkPricing,
        string[] codes,
        decimal expected
    )
    {
        var sut = new TerminalBuilder().WithMultipleProducts(pricing)
            .WithPricingStrategy(
                new BulkPricingStrategy(
                    StandardPricingStrategyBuilder.Build(
                        withProductPricing: pricing
                    ),
                    bulkPricing
                )
            )
            .Build();

        foreach (var code in codes) sut.ScanProduct(code);

        Assert.Equal(expected, sut.CalculateTotal());
    }

    [Theory]
    [InlineData(new[] { "A", "B", "C", "D", "A", "B", "A" }, 14)]
    [InlineData(new[] { "C", "C", "C", "C", "C", "C", "C" }, 7)]
    [InlineData(new[] { "A", "B", "C", "D" }, 7.25)]
    [InlineData(new string[] { }, 0)]
    public void CalculateTotal_NoDiscount_CalculatesTotal(
        string[] codes,
        decimal expected
    )
    {
        var pricing = new[]
        {
            ProductPriceBuilder.Build(withCode: "A", withPrice: 1.25m),
            ProductPriceBuilder.Build(withCode: "B", withPrice: 4.25m),
            ProductPriceBuilder.Build(withCode: "C", withPrice: 1m),
            ProductPriceBuilder.Build(withCode: "D", withPrice: 0.75m)
        };
        var sut = new TerminalBuilder().WithMultipleProducts(pricing).Build();

        foreach (var code in codes) sut.ScanProduct(code);

        Assert.Equal(expected, sut.CalculateTotal());
    }
}