using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Terminal;
using Terminal.Models;
using Terminal.PricingStrategies;
using TerminalUnitTests.Builders.Models;

namespace TerminalUnitTests.Builders;

[ExcludeFromCodeCoverage]
internal class TerminalBuilder
{
    private readonly List<ProductPrice> _products = new();
    private IPricingStrategy? _pricingStrategy;

    public PointOfSaleTerminal Build()
    {
        var terminal = new PointOfSaleTerminal();

        terminal.SetPricing(
            _pricingStrategy ?? new StandardPricingStrategy(_products)
        );

        return terminal;
    }

    public TerminalBuilder WithSingleProduct(ProductPrice productPrice)
    {
        _products.Clear();
        _products.Add(productPrice);

        return this;
    }

    public TerminalBuilder WithSingleProduct(string code, decimal price)
    {
        var product = ProductPriceBuilder.Build(withCode: code, withPrice: price);

        return WithSingleProduct(product);
    }

    public TerminalBuilder WithMultipleProducts(IEnumerable<ProductPrice> products)
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

    public static List<ProductPrice> MakeValidPricing()
    {
        return new List<ProductPrice>
        {
            ProductPriceBuilder.Build(withCode: "A", withPrice: 1.25m),
            ProductPriceBuilder.Build(withCode: "B", withPrice: 4.25m),
            ProductPriceBuilder.Build(withCode: "C", withPrice: 1m),
            ProductPriceBuilder.Build(withCode: "D", withPrice: 0.75m)
        };
    }
}