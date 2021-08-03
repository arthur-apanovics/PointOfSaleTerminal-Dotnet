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
    }
}
