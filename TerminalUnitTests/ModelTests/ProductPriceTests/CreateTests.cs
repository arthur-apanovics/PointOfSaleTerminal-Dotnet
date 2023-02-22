﻿using System;
using Terminal.Models;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.ModelTests.ProductPriceTests;

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
        var actual = () => ProductPrice.Create(code, price: 3m);

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
        var actual = () => ProductPrice.Create(code: "X", price);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}