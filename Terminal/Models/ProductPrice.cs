﻿using Terminal.Helpers;

namespace Terminal.Models;

public record ProductPrice
{
    private ProductPrice(string code, decimal price)
    {
        Code = code;
        Price = price;
    }

    /// <summary>
    ///     Product code
    /// </summary>
    public string Code { get; }

    /// <summary>
    ///     Product price
    /// </summary>
    public decimal Price { get; }

    /// <summary>
    ///     Represents a regular product code to price mapping.
    /// </summary>
    /// <param name="code">
    ///     <see cref="Code" />
    /// </param>
    /// <param name="price">
    ///     <see cref="Price" />
    /// </param>
    public static ProductPrice Create(string code, decimal price)
    {
        ProductValidationHelper.ValidateProductCodeOrThrow(code);
        ProductValidationHelper.ValidatePriceOrThrow(price);

        return new ProductPrice(code, price);
    }
}