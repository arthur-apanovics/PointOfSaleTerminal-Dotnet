using System;
using Terminal.Models;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.ModelTests.SingleUnitPriceTests;

public class CreateTests
{
    [Theory]
    [MemberData(
        nameof(ProductValueProviders.InvalidProductCodes),
        MemberType = typeof(ProductValueProviders)
    )]
    public void ThrowsWhenInvalidProductCodeProvided(string invalidCode)
    {
        // Arrange/Act
        var actual = () => SingleUnitPricing.Create(
            productCode: invalidCode,
            unitPrice: 0.01m
        );

        // Assert
        actual.Should().Throw<ArgumentException>();
    }

    [Theory]
    [MemberData(
        nameof(ProductValueProviders.ValidProductCodes),
        MemberType = typeof(ProductValueProviders)
    )]
    public void DoesNotThrowWhenValidProductCodeProvided(string validCode)
    {
        // Arrange/Act
        var actual = () => SingleUnitPricing.Create(
            productCode: validCode,
            unitPrice: 0.01m
        );

        // Assert
        actual.Should().NotThrow();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ThrowsWhenSingleUnitPriceNotValid(decimal value)
    {
        // Arrange/Act
        var actual = () =>
            SingleUnitPricing.Create(productCode: "_", unitPrice: value);

        // Assert
        actual.Should().Throw<ArgumentException>();
    }
}