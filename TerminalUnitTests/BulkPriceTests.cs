using System;
using Terminal.Models;
using Xunit;

namespace TerminalUnitTests;

public class BulkPriceTests
{
    [Theory]
    [InlineData(3, 0)]
    [InlineData(-3, 0)]
    [InlineData(0, 0)]
    [InlineData(0, -3)]
    [InlineData(0, 3)]
    public void BulkPrice_Properties_ThrowsOnInvalidValues(
        int threshold,
        decimal price
    )
    {
        Action sut = () => new BulkPrice(threshold, price);

        Assert.Throws<ArgumentException>(sut);
    }
}