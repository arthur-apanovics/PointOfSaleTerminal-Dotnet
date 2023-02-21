using System;
using Terminal.Models;

namespace TerminalUnitTests;

public class ProductTests
{
    [Theory]
    [InlineData("X", 0)]
    [InlineData("Y", -1)]
    [InlineData("", 1.5)]
    [InlineData(" ", 1.5)]
    [InlineData(" Z", 1.5)]
    [InlineData("A B", 1.5)]
    [InlineData(null, 1.5)]
    public void Product_Properties_ThrowsOnInvalidValues(
        string code,
        decimal price
    )
    {
        Action sut = () => new Product(code, price);

        Assert.Throws<ArgumentException>(sut);
    }
}