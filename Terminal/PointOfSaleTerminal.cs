using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Contracts;

namespace Terminal
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private readonly List<IProduct> _loadedProducts = new();
        private readonly List<IProduct> _scannedProducts = new();

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

        public decimal GetPrice(string code)
        {
            var product = TryGetProductFromPricing(code);
            if (product is null)
            {
                throw new ArgumentOutOfRangeException($"No price found for product with code '{code}'");
            }

            return product.Price;
        }

        public void ScanProduct(string code)
        {
            var product = TryGetProductFromPricing(code);
            if (product is null)
            {
                throw new ArgumentException($"Product with code '{code}' not found in pricing list");
            }

            _scannedProducts.Add(product);
        }

        public decimal CalculateTotal()
        {
            throw new NotImplementedException();
        }

        public ICollection<IProduct> GetCart()
        {
            return _scannedProducts;
        }

        private IProduct? TryGetProductFromPricing(string code)
        {
            return _loadedProducts.FirstOrDefault(p => p.Code == code);
        }
    }
}
