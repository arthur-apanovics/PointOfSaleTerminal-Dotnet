using System;
using Terminal.Helpers;

namespace Terminal.Models;

public record SingleAndBulkUnitPrice : IProductPrice
{
    public string ProductCode { get; }

    private decimal SingleUnitPrice { get; }
    private int BulkUnitSize { get; }
    private decimal BulkUnitPrice { get; }

    private SingleAndBulkUnitPrice(
        string productCode,
        decimal singleUnitPrice,
        int bulkUnitSize,
        decimal bulkUnitPrice
    )
    {
        ProductCode = productCode;
        SingleUnitPrice = singleUnitPrice;
        BulkUnitSize = bulkUnitSize;
        BulkUnitPrice = bulkUnitPrice;
    }

    public decimal CalculateTotalFor(int productQuantity)
    {
        var result = 0m;
        var remainder = productQuantity;
        while (remainder >= BulkUnitSize)
        {
            result += BulkUnitPrice;
            remainder -= BulkUnitSize;
        }

        if (remainder > 0)
            result += remainder * SingleUnitPrice;

        return result;
    }

    public static SingleAndBulkUnitPrice Create(
        string productCode,
        decimal singleUnitPrice,
        int bulkUnitSize,
        decimal bulkUnitPrice
    )
    {
        ThrowIfArgumentsInvalid(
            productCode,
            singleUnitPrice,
            bulkUnitSize,
            bulkUnitPrice
        );

        return new SingleAndBulkUnitPrice(
            productCode,
            singleUnitPrice,
            bulkUnitSize,
            bulkUnitPrice
        );
    }

    private static void ThrowIfArgumentsInvalid(
        string productCode,
        decimal singleUnitPrice,
        int bulkUnitSize,
        decimal bulkUnitPrice
    )
    {
        ProductValidationHelper.ThrowIfProductCodeNotValid(productCode);
        ProductValidationHelper.ThrowIfProductPriceNotValid(singleUnitPrice);
        ThrowIfBulkUnitSizeNotValid(bulkUnitSize);
        ThrowIfPricingIsEqual(singleUnitPrice, bulkUnitPrice);
        ProductValidationHelper.ThrowIfProductPriceNotValid(bulkUnitPrice);
    }

    private static void ThrowIfPricingIsEqual(
        decimal singleUnitPrice,
        decimal bulkUnitPrice
    )
    {
        if (singleUnitPrice == bulkUnitPrice)
            throw new ArgumentException(
                $"{nameof(SingleUnitPrice)} cannot be equal to {nameof(BulkUnitPrice)}"
            );
    }

    private static void ThrowIfBulkUnitSizeNotValid(int value)
    {
        if (value is <= 0 or 1)
        {
            throw new ArgumentException(
                $"Invalid value provided - \"{value}\"",
                nameof(BulkUnitSize)
            );
        }
    }
}