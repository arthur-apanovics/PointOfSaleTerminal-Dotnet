using System;
using Terminal.Models;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.Models.BulkPriceTests;

public class ConstructorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void ThrowsWhenThresholdValueNotValid(int threshold)
    {
        // Arrange
        var actual = () => new BulkPrice(threshold, price: 3m);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [MemberData(
        nameof(ProductValueProviders.InvalidProductPrices),
        MemberType = typeof(ProductValueProviders)
    )]
    public void ThrowsWhenBulkPriceValueNotValid(decimal price)
    {
        // Arrange
        var actual = () => new BulkPrice(threshold: 3, price);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}