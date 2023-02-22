using System;
using Terminal.Models;
using TerminalUnitTests.Builders.Models;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.ModelTests.BulkProductTests;

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