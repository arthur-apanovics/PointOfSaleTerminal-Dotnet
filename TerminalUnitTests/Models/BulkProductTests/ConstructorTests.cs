using System;
using Terminal.Models;
using TerminalUnitTests.Builders;

namespace TerminalUnitTests.Models.BulkProductTests;

public class ConstructorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(" A")]
    [InlineData(" A ")]
    [InlineData("A ")]
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