using System;

namespace Terminal.Helpers;

public static class ProductValidationHelper
{
    public static void ValidateProductCodeOrThrow(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Contains(' '))
            throw new ArgumentException(
                "Product code must not contain spaces and not be empty"
            );
    }

    public static void ValidatePriceOrThrow(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Price cannot be zero or negative");
    }
}