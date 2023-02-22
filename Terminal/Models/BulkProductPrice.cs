using System;
using Terminal.Helpers;

namespace Terminal.Models;

public record BulkProductPrice
{
    private BulkProductPrice(int bulkThreshold, decimal bulkPrice)
    {
        BulkThreshold = bulkThreshold;
        BulkPrice = bulkPrice;
    }

    /// <summary>
    ///     Bulk quantity threshold at which the special price applies.
    /// </summary>
    /// <example>3 for $3.00</example>
    public int BulkThreshold { get; }

    /// <summary>
    ///     Special price for bulk threshold.
    /// </summary>
    public decimal BulkPrice { get; }

    /// <summary>
    ///     Represents a quantity at which a special price is applied.
    /// </summary>
    /// <param name="bulkThreshold">
    ///     <see cref="BulkThreshold" />
    /// </param>
    /// <param name="bulkPrice">
    ///     <see cref="BulkPrice" />
    /// </param>
    /// <exception cref="ArgumentException">If values are not valid</exception>
    public static BulkProductPrice Create(int bulkThreshold, decimal bulkPrice)
    {
        if (bulkThreshold <= 0)
            throw new ArgumentException(
                "Bulk threshold cannot be less than or equal to zero"
            );

        ProductValidationHelper.ValidatePriceOrThrow(bulkPrice);

        return new BulkProductPrice(bulkThreshold, bulkPrice);
    }
}