using System;
using System.Collections.Generic;
using Terminal.Contracts;

namespace Terminal
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private IDictionary<string, decimal> _productPriceMap = new Dictionary<string, decimal>();

        public void SetPricing(IDictionary<string, decimal> priceMap)
        {
            _productPriceMap = priceMap;
        }

        public decimal GetPrice(string productCode)
        {
            var found = _productPriceMap.TryGetValue(productCode, out var price);
            if (found)
            {
                return price;
            }

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
