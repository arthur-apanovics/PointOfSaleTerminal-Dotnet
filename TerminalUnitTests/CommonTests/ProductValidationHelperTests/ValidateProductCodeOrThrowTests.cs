using System;
using Terminal.Common;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.CommonTests.ProductValidationHelperTests;

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
            ProductValidationHelper.ValidateProductCodeOrThrow(productCode);

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
            ProductValidationHelper.ValidateProductCodeOrThrow(productCode);

        // Act/Assert
        actual.Should().NotThrow();
    }
}