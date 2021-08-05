using System;
using System.Collections.Generic;
using Terminal;
using Terminal.Contracts;
using Terminal.Discounts;
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

        [Fact]
        public void ScanProduct_MultipleProducts_StoresInCart()
        {
            var products = new[]
            {
                new Product("A", 1.25m),
                new Product("B", 4.25m),
            };
            var sut = new TerminalBuilder().WithMultipleProducts(products).Build();

            foreach (var product in products)
            {
                sut.ScanProduct(product.Code);
            }

            Assert.Equal(products.Length, sut.GetCart().Count);
        }

        [Fact]
        public void ScanProduct_SingleProduct_ThrowsWhenCodeNotInPricingList()
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    var sut = new TerminalBuilder().WithSingleProduct("A", 1.25m).Build();

                    sut.ScanProduct("X");
                });
        }

        [Theory]
        [InlineData(new[]{"A","B","C","D","A","B","A",}, 13.25)]
        [InlineData(new[]{"C","C","C","C","C","C","C",}, 6)]
        [InlineData(new[]{"A","B","C","D",}, 7.25)]
        [InlineData(new[]{"B","B","B","B","B",}, 21.25)]
        [InlineData(new[]{"B","B","B","D","D", "D",}, 15)]
        [InlineData(new[]{"D",}, 0.75)]
        [InlineData(new string[]{}, 0)]
        public void CalculateTotal_AppliesDiscount_BulkDiscountStrategy(string[] codes, decimal expected)
        {
            var pricing = new List<IProduct>
            {
                new Product("A", 1.25m),
                new Product("B", 4.25m),
                new Product("C", 1m),
                new Product("D", 0.75m),
            };

            var bulkPricing = new BulkPromotionStrategy();
            bulkPricing.LoadBulkPricing(new Dictionary<string, KeyValuePair<int, decimal>>
            {
                {"A", new KeyValuePair<int, decimal>(3, 3m)},
                {"C", new KeyValuePair<int, decimal>(6, 5m)}
            });

            var sut = new TerminalBuilder().WithMultipleProducts(pricing)
                .WithPromotionStrategy(bulkPricing)
                .Build();

            foreach (var code in codes)
            {
                sut.ScanProduct(code);
            }

            Assert.Equal(expected, sut.CalculateTotal());
        }
    }
}
