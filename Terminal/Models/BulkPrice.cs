using System;
using Terminal.Common;

namespace Terminal.Models
{
    public readonly struct BulkPrice
    {
        public BulkPrice(int threshold, decimal price)
        {
            if (threshold <= 0)
                throw new ArgumentException("Bulk threshold cannot be less than or equal to zero");

            ProductValidationHelper.ValidatePriceOrThrow(price);

            Threshold = threshold;
            Price = price;
        }

        public int Threshold { get; }
        public decimal Price { get; }
    }
}
