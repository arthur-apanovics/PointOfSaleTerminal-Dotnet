using System;
using System.Collections.Generic;
using Terminal;
using Xunit;

namespace TerminalUnitTests
{
    public class TerminalTests
    {
        [Theory]
        [InlineData("A", 1.25)]
        [InlineData("B", 4.25)]
        public void SetPricing_SingleProduct_SetsPrice(string productCode, decimal productPrice)
        {
            var sut = new PointOfSaleTerminal();
            var pricing = new Dictionary<string, decimal>
            {
                {productCode, productPrice}
            };

            sut.SetPricing(pricing);
            var actual = sut.GetPrice(productCode);

            Assert.Equal(productPrice, actual);
        }

        [Theory]
        [InlineData("C", -4.25)]
        [InlineData("D", 0)]
        [InlineData("", 4.25)]
        public void SetPricing_SingleProduct_ThrowsOnInvalidValues(string productCode, decimal productPrice)
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    var sut = new PointOfSaleTerminal();
                    var pricing = new Dictionary<string, decimal>
                    {
                        {productCode, productPrice}
                    };

                    sut.SetPricing(pricing);
                }
            );
        }

        [Fact]
        public void SetPricing_MultipleProducts_SetsPricing()
        {
            var sut = new PointOfSaleTerminal();
            var pricing = new Dictionary<string, decimal>
            {
                {"A", 1.25m},
                {"B", 4.25m},
                {"C", 1m},
                {"D", 0.75m},
            };

            sut.SetPricing(pricing);

            foreach (var priceMap in pricing)
            {
                Assert.Equal(priceMap.Value, sut.GetPrice(priceMap.Key));
            }
        }
    }
}
