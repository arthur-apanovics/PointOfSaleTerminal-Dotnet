using System;
using Terminal.Models;
using TerminalUnitTests.Builders;

namespace TerminalUnitTests.Models.BulkProductTests;

public class ConstructorTests
{
    [Theory]
    [MemberData(
        nameof(TestDataProviders.InvalidProductCodes),
        MemberType = typeof(TestDataProviders)
    )]
    public void ThrowsWhenProductCodeValueNotValid(string productCode)
    {
        // Arrange
        var actual = () => new BulkProduct(
            productCode,
            BulkPriceBuilder.Build()
        );

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}