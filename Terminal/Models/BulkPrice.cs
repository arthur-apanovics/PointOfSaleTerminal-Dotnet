using System;
using Terminal.Common;

namespace Terminal.Models
{
    public readonly struct BulkPrice
    {
        /// <summary>
        /// Represents a quantity at which a special price is applied.
        /// </summary>
        /// <param name="threshold"><see cref="Threshold"/></param>
        /// <param name="price"><see cref="Price"/></param>
        /// <exception cref="ArgumentException">If values are not valid</exception>
        public BulkPrice(int threshold, decimal price)
        {
            if (threshold <= 0)
                throw new ArgumentException("Bulk threshold cannot be less than or equal to zero");

            ProductValidationHelper.ValidatePriceOrThrow(price);

            Threshold = threshold;
            Price = price;
        }

        /// <summary>
        /// Bulk quantity threshold at which the special price applies.
        /// </summary>
        /// <example>3 for $3.00</example>
        public int Threshold { get; }

        /// <summary>
        /// Special price for bulk threshold.
        /// </summary>
        public decimal Price { get; }
    }
}
