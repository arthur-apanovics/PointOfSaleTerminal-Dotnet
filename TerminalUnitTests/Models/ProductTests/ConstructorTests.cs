using System;
using Terminal.Models;

namespace TerminalUnitTests.Models.ProductTests;

public class ConstructorTests
{
    [Theory]
    [MemberData(
        nameof(TestDataProviders.InvalidProductCodes),
        MemberType = typeof(TestDataProviders)
    )]
    public void ThrowsWhenProductCodeNotValid(string code)
    {
        // Arrange
        var actual = () => new Product(code, price: 3m);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [MemberData(
        nameof(TestDataProviders.InvalidProductPrices),
        MemberType = typeof(TestDataProviders)
    )]
    public void ThrowsWhenProductPriceNotValid(decimal price)
    {
        // Arrange
        var actual = () => new Product(code: "X", price);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}