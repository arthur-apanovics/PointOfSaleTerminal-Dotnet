using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Contracts;

namespace Terminal.Discounts
{
    public class BulkPromotionStrategy : IPromotionStrategy
    {
        private Dictionary<string, KeyValuePair<int, decimal>> _bulkPricing = new();

        public void LoadBulkPricing(Dictionary<string, KeyValuePair<int, decimal>> productCodeToQuantityAndPriceMap)
        {
            _bulkPricing = productCodeToQuantityAndPriceMap;
        }

        public decimal CalculateTotalWithDiscounts(IEnumerable<IProduct> cart)
        {
            static IProduct GetProductFromCart(IEnumerable<IProduct> products, string productCode)
            {
                return products.FirstOrDefault(x => x.Code == productCode)
                       // "This should never happen" scenario. Handle to stop IDE complaining
                       ?? throw new ArgumentOutOfRangeException($"No prodcut with code '{productCode}' in cart");
            }

            static decimal TotalForProductWithoutDiscount(int amount, IProduct product)
            {
                var total = 0m;
                for (var i = 0; i < amount; i++)
                {
                    total += product.Price;
                }

                return total;
            }

            KeyValuePair<string, KeyValuePair<int, decimal>>? GetBulkPriceEntry(string productCode)
            {
                var entry = _bulkPricing.FirstOrDefault(x => x.Key == productCode);

                return string.IsNullOrEmpty(entry.Key) ? null : entry;
            }

            var cartList = cart.ToList();
            var quantities = cartList.GroupBy(p => p.Code)
                .ToDictionary(g => g.Key, g => g.Count());

            decimal priceTotal = 0;
            foreach (var (productCode, amountInCart) in quantities)
            {
                var remainingAmount = amountInCart;
                var product = GetProductFromCart(cartList, productCode);

                var entry = GetBulkPriceEntry(productCode);
                if (entry is null)
                {
                    priceTotal += TotalForProductWithoutDiscount(remainingAmount, product);
                    continue;
                }

                var (step, stepPrice) = entry.Value.Value;
                while (remainingAmount >= step)
                {
                    priceTotal += stepPrice;
                    remainingAmount -= step;
                }

                priceTotal += TotalForProductWithoutDiscount(remainingAmount, product);
            }

            return priceTotal;
        }
    }
}
