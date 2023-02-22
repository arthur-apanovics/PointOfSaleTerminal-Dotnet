using System;
using Terminal.Helpers;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.CommonTests.ProductValidationHelperTests;

public class ValidatePriceOrThrowTests
{
    [Theory]
    [MemberData(
        nameof(ProductValueProviders.InvalidProductPrices),
        MemberType = typeof(ProductValueProviders)
    )]
    public void ThrowsWhenProductPriceIsNotValid(decimal productPrice)
    {
        // Arrange
        var actual = () =>
            ProductValidationHelper.ValidatePriceOrThrow(productPrice);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [MemberData(
        nameof(ProductValueProviders.ValidProductPrices),
        MemberType = typeof(ProductValueProviders)
    )]
    public void DoesNotThrowWhenProductPriceIsValid(decimal productPrice)
    {
        // Arrange
        var actual = () =>
            ProductValidationHelper.ValidatePriceOrThrow(productPrice);

        // Act/Assert
        actual.Should().NotThrow();
    }
}