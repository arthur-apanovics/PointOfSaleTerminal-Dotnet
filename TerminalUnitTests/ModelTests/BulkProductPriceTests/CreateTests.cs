using System;
using Terminal.Models;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.ModelTests.BulkProductPriceTests;

public class ConstructorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void ThrowsWhenThresholdValueNotValid(int threshold)
    {
        // Arrange
        var actual = () =>
            BulkProductPrice.Create("Foo", threshold, bulkPrice: 3m);

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
        var actual = () =>
            BulkProductPrice.Create("Foo", bulkThreshold: 3, price);

        // Act/Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }
}