using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Terminal.Models;
using Terminal.Pricing;

namespace TerminalUnitTests.Builders;

[ExcludeFromCodeCoverage]
internal class StandardPricingBuilder
{
    private List<Product> _products = new();

    public StandardPricingStrategy Build()
    {
        return new StandardPricingStrategy(_products);
    }

    public StandardPricingBuilder WithPricing(string code, decimal price)
    {
        return WithPricing(new Product(code, price));
    }

    public StandardPricingBuilder WithPricing(Product pricing)
    {
        return WithPricing(new[] { pricing });
    }

    public StandardPricingBuilder WithPricing(IEnumerable<Product> pricing)
    {
        _products.Clear();
        _products.AddRange(pricing);

        return this;
    }

    public StandardPricingBuilder WithValidPricing()
    {
        _products = new List<Product>
        {
            new("A", 1.25m), new("B", 4.25m), new("C", 1m), new("D", 0.75m)
        };

        return this;
    }
}