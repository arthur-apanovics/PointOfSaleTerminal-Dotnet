using System;
using Terminal.Helpers;

namespace Terminal.Models;

public record BulkProductPrice
{
    private BulkProductPrice(
        string productCode,
        int bulkThreshold,
        decimal bulkPrice
    )
    {
        ProductCode = productCode;
        BulkThreshold = bulkThreshold;
        BulkPrice = bulkPrice;
    }

    /// <summary>
    ///     Product code for which the bulk price will be applied to
    /// </summary>
    public string ProductCode { get; }

    /// <summary>
    ///     Bulk quantity threshold at which the special price applies
    /// </summary>
    /// <example>3 for $3.00</example>
    public int BulkThreshold { get; }

    /// <summary>
    ///     Special price for each bulk unit
    /// </summary>
    public decimal BulkPrice { get; }

    public static BulkProductPrice Create(
        string productCode,
        int bulkThreshold,
        decimal bulkPrice
    )
    {
        ProductValidationHelper.ValidateProductCodeOrThrow(productCode);
        ThrowIfInvalidBulkThreshold(bulkThreshold);
        ProductValidationHelper.ValidatePriceOrThrow(bulkPrice);

        return new BulkProductPrice(productCode, bulkThreshold, bulkPrice);
    }

    private static void ThrowIfInvalidBulkThreshold(int bulkThreshold)
    {
        if (bulkThreshold <= 0)
            throw new ArgumentException(
                "Bulk threshold cannot be less than or equal to zero"
            );
    }
}