using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Common;
using Terminal.Contracts;
using Terminal.Models;

namespace Terminal.Pricing
{
    public class BulkPricingStrategy : StandardPricingStrategy, IDiscountablePricingStrategy
    {
        private readonly Dictionary<string, BulkPrice> _codeToBulkPriceMap;

        public BulkPricingStrategy(IEnumerable<Product> pricing, IEnumerable<BulkProduct> bulkPrices)
            : base(pricing)
        {
            var bulkPriceList = bulkPrices.ToList();
            ValidatePricingOrThrow(bulkPriceList);
            _codeToBulkPriceMap = bulkPriceList.ToDictionary(b => b.Code, b => b.BulkPrice);
        }

        public bool HasDiscountedPricing(string code)
        {
            return _codeToBulkPriceMap.ContainsKey(code);
        }

        protected override decimal CalculateTotalWithoutGuards(string code, int quantity)
        {
            decimal result;
            var remaining = quantity;

            (result, remaining) = TotalWithDiscount(code, remaining);
            result += GetPrice(code) * remaining;

            return result;
        }

        private (decimal result, int remainder) TotalWithDiscount(string code, int quantity)
        {
            var result = 0m;
            var remainder = quantity;

            if (!HasDiscountedPricing(code))
                return (result, remainder);

            // opting to use while loop instead of arithmetic for simplicity/readability
            var bulkPrice = _codeToBulkPriceMap[code];
            while (remainder >= bulkPrice.Threshold)
            {
                result += bulkPrice.Price;
                remainder -= bulkPrice.Threshold;
            }

            return (result, remainder);
        }

        private static void ValidatePricingOrThrow(IEnumerable<BulkProduct> bulkPrices)
        {
            if (bulkPrices.HasDuplicates(b => b.Code))
                throw new ArgumentException("Bulk pricing list cannot contain duplicate products");
        }
    }
}
