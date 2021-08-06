using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Terminal;
using Terminal.Contracts;
using Terminal.Models;

namespace TerminalUnitTests
{
    [ExcludeFromCodeCoverage]
    internal class TerminalBuilder
    {
        private readonly List<IProduct> _products = new();
        private IDiscountStrategy? _promotionStrategy = null;

        public PointOfSaleTerminal Build()
        {
            var terminal = new PointOfSaleTerminal();
            terminal.SetPricing(_products, _promotionStrategy);

            return terminal;
        }

        public TerminalBuilder WithSingleProduct(IProduct product)
        {
            _products.Clear();
            _products.Add(product);

            return this;
        }

        public TerminalBuilder WithSingleProduct(string code, decimal price)
        {
            var product = new Product(code, price);

            return WithSingleProduct(product);
        }

        public TerminalBuilder WithMultipleProducts(IEnumerable<IProduct> products)
        {
            _products.Clear();
            _products.AddRange(products);

            return this;
        }

        public TerminalBuilder WithPromotionStrategy(IDiscountStrategy discountStrategy)
        {
            _promotionStrategy = discountStrategy;

            return this;
        }
    }
}
