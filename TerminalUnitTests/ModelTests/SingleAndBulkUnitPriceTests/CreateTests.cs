using System;
using Terminal.Models;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.ModelTests.SingleAndBulkUnitPriceTests;

public class CreateTests
{
    [Theory]
    [MemberData(
        nameof(ProductValueProviders.InvalidProductCodes),
        MemberType = typeof(ProductValueProviders)
    )]
    public void ThrowsWhenInvalidProductCodeProvided(string invalidCode)
    {
        // Arrange/Act
        var actual = () => SingleAndBulkUnitPricing.Create(
            productCode: invalidCode,
            singleUnitPrice: 1m,
            bulkUnitSize: 2,
            bulkUnitPrice: 3m
        );

        // Assert
        actual.Should().Throw<ArgumentException>();
    }

    [Theory]
    [MemberData(
        nameof(ProductValueProviders.ValidProductCodes),
        MemberType = typeof(ProductValueProviders)
    )]
    public void DoesNotThrowWhenValidProductCodeProvided(string validCode)
    {
        // Arrange/Act
        var actual = () => SingleAndBulkUnitPricing.Create(
            productCode: validCode,
            singleUnitPrice: 1m,
            bulkUnitSize: 2,
            bulkUnitPrice: 3m
        );

        // Assert
        actual.Should().NotThrow();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ThrowsWhenSingleUnitPriceNotValid(decimal value)
    {
        // Arrange/Act
        var actual = () => SingleAndBulkUnitPricing.Create(
            productCode: "_",
            singleUnitPrice: value,
            bulkUnitSize: 2,
            bulkUnitPrice: 3m
        );

        // Assert
        actual.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    public void ThrowsWhenBulkUnitSizeNotValid(int value)
    {
        // Arrange/Act
        var actual = () => SingleAndBulkUnitPricing.Create(
            productCode: "_",
            singleUnitPrice: 1m,
            bulkUnitSize: value,
            bulkUnitPrice: 3m
        );

        // Assert
        actual.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ThrowsWhenSingleUnitPriceIsEqualToBulkUnitPrice()
    {
        // Arrange/Act
        var actual = () => SingleAndBulkUnitPricing.Create(
            productCode: "_",
            singleUnitPrice: 20m,
            bulkUnitSize: 2,
            bulkUnitPrice: 20m
        );

        // Assert
        actual.Should().Throw<ArgumentException>();
    }
}