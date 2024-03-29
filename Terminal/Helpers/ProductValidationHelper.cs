﻿using System;

namespace Terminal.Helpers;

public static class ProductValidationHelper
{
    public static void ThrowIfProductCodeNotValid(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Contains(' '))
            throw new ArgumentException(
                "Product code must not contain spaces and not be empty"
            );
    }

    public static void ThrowIfProductPriceNotValid(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Price cannot be zero or negative");
    }
}