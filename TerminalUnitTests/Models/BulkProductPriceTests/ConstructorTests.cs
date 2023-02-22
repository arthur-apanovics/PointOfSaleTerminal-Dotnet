using System;
using Terminal.Models;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.Models.BulkProductPriceTests;

public class ConstructorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void ThrowsWhenThresholdValueNotValid(int threshold)
    {
        // Arrange
        var actual = () => new BulkProductPrice(threshold, bulkPrice: 3m);

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
        var actual = () => new BulkProductPrice(bulkThreshold: 3, price);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}