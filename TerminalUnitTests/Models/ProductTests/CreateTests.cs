using System;
using Terminal.Models;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.Models.ProductTests;

public class CreateTests
{
    [Theory]
    [MemberData(
        nameof(ProductValueProviders.InvalidProductCodes),
        MemberType = typeof(ProductValueProviders)
    )]
    public void ThrowsWhenProductCodeNotValid(string code)
    {
        // Arrange
        var actual = () => Product.Create(code, price: 3m);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [MemberData(
        nameof(ProductValueProviders.InvalidProductPrices),
        MemberType = typeof(ProductValueProviders)
    )]
    public void ThrowsWhenProductPriceNotValid(decimal price)
    {
        // Arrange
        var actual = () => Product.Create(code: "X", price);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}