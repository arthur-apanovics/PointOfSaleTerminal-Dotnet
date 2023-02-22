using System;
using Terminal.Models;
using TerminalUnitTests.Builders;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.Models.BulkProductTests;

public class CreateTests
{
    [Theory]
    [MemberData(
        nameof(ProductValueProviders.InvalidProductCodes),
        MemberType = typeof(ProductValueProviders)
    )]
    public void ThrowsWhenProductCodeValueNotValid(string productCode)
    {
        // Arrange
        var actual = () => BulkProduct.Create(
            productCode,
            BulkPriceBuilder.Build()
        );

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}