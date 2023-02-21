using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Terminal;
using Terminal.Contracts;
using Terminal.Models;
using Terminal.Pricing;

namespace TerminalUnitTests.Builders;

[ExcludeFromCodeCoverage]
internal class TerminalBuilder
{
    private readonly List<Product> _products = new();
    private IPricingStrategy? _pricingStrategy;

    public PointOfSaleTerminal Build()
    {
        var terminal = new PointOfSaleTerminal();

        terminal.SetPricing(
            _pricingStrategy ?? new StandardPricingStrategy(_products)
        );

        return terminal;
    }

    public TerminalBuilder WithSingleProduct(Product product)
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

    public TerminalBuilder WithMultipleProducts(IEnumerable<Product> products)
    {
        _products.Clear();
        _products.AddRange(products);

        return this;
    }

    public TerminalBuilder WithPricingStrategy(IPricingStrategy pricingStrategy)
    {
        _pricingStrategy = pricingStrategy;

        return this;
    }

    public static List<Product> MakeValidPricing()
    {
        return new List<Product>
        {
            new("A", 1.25m), new("B", 4.25m), new("C", 1m), new("D", 0.75m)
        };
    }
}