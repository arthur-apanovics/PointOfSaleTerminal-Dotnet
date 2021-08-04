using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Contracts;

namespace Terminal
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private readonly List<IProduct> _loadedProducts = new();

        public void SetPricing(IEnumerable<IProduct> products)
        {
            var productArray = products as IProduct[] ?? products.ToArray();
            if (productArray.GroupBy(p => p.Code).Any(g => g.Count() > 1))
            {
                throw new ArgumentException("Pricing cannot contain duplicate product codes");
            }

            _loadedProducts.Clear();
            _loadedProducts.AddRange(productArray);
        }

        public decimal GetPrice(string productCode)
        {
            var product = _loadedProducts.FirstOrDefault(p => p.Code == productCode);
            if (product is not null)
            {
                return product.Price;
            }

            throw new ArgumentOutOfRangeException($"No price found for product with code '{productCode}'");
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
