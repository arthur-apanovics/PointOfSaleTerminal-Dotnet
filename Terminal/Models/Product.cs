using Terminal.Common;

namespace Terminal.Models;

public record Product
{
    private Product(string code, decimal price)
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
    public static Product Create(string code, decimal price)
    {
        ProductValidationHelper.ValidateProductCodeOrThrow(code);
        ProductValidationHelper.ValidatePriceOrThrow(price);

        return new Product(code, price);
    }
}