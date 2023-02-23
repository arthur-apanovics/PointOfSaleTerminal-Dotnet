using System;
using Terminal.Helpers;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.HelperTests.ProductValidationHelperTests;

public class ValidateProductCodeOrThrowTests
{
    [Theory]
    [MemberData(
        nameof(ProductValueProviders.InvalidProductCodes),
        MemberType = typeof(ProductValueProviders)
    )]
    public void ThrowsWhenProductCodeIsNotValid(string productCode)
    {
        // Arrange
        var actual = () =>
            ProductValidationHelper.ThrowIfProductCodeNotValid(productCode);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [MemberData(
        nameof(ProductValueProviders.ValidProductCodes),
        MemberType = typeof(ProductValueProviders)
    )]
    public void DoesNotThrowWhenProductCodeIsValid(string productCode)
    {
        // Arrange
        var actual = () =>
            ProductValidationHelper.ThrowIfProductCodeNotValid(productCode);

        // Act/Assert
        actual.Should().NotThrow();
    }
}