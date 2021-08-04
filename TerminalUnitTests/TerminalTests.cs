using System;
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
            var sut = new TerminalBuilder().WithSingleProduct(productCode, productPrice).Build();

            Assert.Equal(productPrice, sut.GetPrice(productCode));
        }

        [Theory]
        [InlineData("C", -4.25)]
        [InlineData("D", 0)]
        [InlineData("", 4.25)]
        public void SetPricing_SingleProduct_ThrowsOnInvalidValues(string productCode, decimal productPrice)
        {
            Assert.Throws<ArgumentException>(
                () => { new TerminalBuilder().WithSingleProduct(productCode, productPrice).Build(); }
            );
        }

        [Fact]
        public void SetPricing_MultipleProducts_SetsPricing()
        {
            var products = new[]
            {
                new Product("A", 1.25m),
                new Product("B", 4.25m),
                new Product("C", 1m),
                new Product("D", 0.75m),
            };
            var sut = new TerminalBuilder().WithMultipleProducts(products).Build();

            foreach (var product in products)
            {
                Assert.Equal(product.Price, sut.GetPrice(product.Code));
            }
        }

        [Fact]
        public void SetPricing_DuplicateProductCodes_ThrowsException()
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    var products = new[]
                    {
                        new Product("A", 1.25m),
                        new Product("B", 4.25m),
                        new Product("B", 1m),
                    };

                    new TerminalBuilder().WithMultipleProducts(products).Build();
                }
            );
        }

        [Fact]
        public void GetPrice_UnsetProduct_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    var sut = new TerminalBuilder().WithSingleProduct("X", 0.01m).Build();

                    sut.GetPrice("Y");
                }
            );
        }
    }
}
