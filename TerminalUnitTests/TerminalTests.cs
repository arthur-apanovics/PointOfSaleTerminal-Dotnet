using System;
using System.Collections.Generic;
using Terminal;
using Terminal.Pricing;
using Terminal.Models;
using TerminalUnitTests.Builders;
using Xunit;

namespace TerminalUnitTests
{
    public class TerminalTests
    {
        [Fact]
        public void Terminal_NoPricingStrategySet_ThrowsException()
        {
            Action sut = () =>
            {
                var terminal = new PointOfSaleTerminal();
                terminal.ScanProduct("X");
            };

            Assert.Throws<NullReferenceException>(sut);
        }

        [Fact]
        public void ScanProduct_SingleProduct_ThrowsWhenCodeNotInPricingList()
        {
            Action sut = () =>
            {
                var sut = new TerminalBuilder().WithSingleProduct("A", 1.25m).Build();

                sut.ScanProduct("X");
            };

            Assert.Throws<ArgumentException>(sut);
        }

        [Theory]
        [InlineData(new[] { "A", "B", "C", "D", "A", "B", "A", }, 13.25)]
        [InlineData(new[] { "C", "C", "C", "C", "C", "C", "C", }, 6)]
        [InlineData(new[] { "A", "B", "C", "D", }, 7.25)]
        [InlineData(new[] { "B", "B", "B", "B", "B", }, 21.25)]
        [InlineData(new[] { "B", "B", "B", "D", "D", "D", }, 15)]
        [InlineData(new[] { "D", }, 0.75)]
        [InlineData(new string[] { }, 0)]
        public void CalculateTotal_WithBulkDiscount_CalculatesTotal(string[] codes, decimal expected)
        {
            var pricing = new List<Product>
            {
                new("A", 1.25m),
                new("B", 4.25m),
                new("C", 1m),
                new("D", 0.75m),
            };
            var bulkPricing = new List<BulkProduct>
            {
                new("A", new BulkPrice(3, 3m)),
                new("C", new BulkPrice(6, 5m)),
            };
            var sut = new TerminalBuilder().WithMultipleProducts(pricing)
                .WithPricingStrategy(new BulkPricingStrategy(pricing, bulkPricing))
                .Build();

            foreach (var code in codes)
            {
                sut.ScanProduct(code);
            }

            Assert.Equal(expected, sut.CalculateTotal());
        }

        [Theory]
        [InlineData(new[] { "A", "B", "C", "D", "A", "B", "A", }, 14)]
        [InlineData(new[] { "C", "C", "C", "C", "C", "C", "C", }, 7)]
        [InlineData(new[] { "A", "B", "C", "D", }, 7.25)]
        [InlineData(new string[] { }, 0)]
        public void CalculateTotal_NoDiscount_CalculatesTotal(string[] codes, decimal expected)
        {
            var pricing = new List<Product>
            {
                new("A", 1.25m),
                new("B", 4.25m),
                new("C", 1m),
                new("D", 0.75m),
            };
            var sut = new TerminalBuilder().WithMultipleProducts(pricing).Build();

            foreach (var code in codes)
            {
                sut.ScanProduct(code);
            }

            Assert.Equal(expected, sut.CalculateTotal());
        }
    }
}
