using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Contracts;

namespace Terminal
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private IDictionary<string, decimal> _productPriceMap = new Dictionary<string, decimal>();

        public void SetPricing(IDictionary<string, decimal> priceMap)
        {
            if (priceMap.Any(x => x.Key == string.Empty))
            {
                throw new ArgumentException("Pricing contains invalid product code");
            }

            if (priceMap.Any(x => x.Value <= 0))
            {
                throw new ArgumentException("Pricing contains invalid product price");
            }

            _productPriceMap = priceMap;
        }

        public decimal GetPrice(string productCode)
        {
            var found = _productPriceMap.TryGetValue(productCode, out var price);
            if (found)
            {
                return price;
            }

            // could throw a custom exception but this one does the job
            throw new KeyNotFoundException($"No price found for product with code '{productCode}'");
        }

        public void ScanProduct(string code)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateTotal()
        {
            throw new NotImplementedException();
        }
    }
}
