using System.Collections.Generic;
using Terminal;
using Terminal.Contracts;

namespace TerminalUnitTests
{
    internal class TerminalBuilder
    {
        private readonly List<IProduct> _products = new();

        public PointOfSaleTerminal Build()
        {
            var terminal = new PointOfSaleTerminal();

            if (_products.Count > 0)
            {
                terminal.SetPricing(_products);
            }

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
    }
}
