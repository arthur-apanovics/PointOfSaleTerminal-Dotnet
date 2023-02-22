using System;
using Terminal.Common;

namespace Terminal.Models;

public readonly struct BulkProductPrice
{
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
    public BulkProductPrice(int bulkThreshold, decimal bulkPrice)
    {
        if (bulkThreshold <= 0)
            throw new ArgumentException(
                "Bulk threshold cannot be less than or equal to zero"
            );

        ProductValidationHelper.ValidatePriceOrThrow(bulkPrice);

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
}