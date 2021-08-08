using System;
using Terminal.Common;

namespace Terminal.Models
{
    public readonly struct Product
    {
        public Product(string code, decimal price)
        {
            ProductValidationHelper.ValidateProductCodeOrThrow(code);
            ProductValidationHelper.ValidatePriceOrThrow(price);

            Code = code;
            Price = price;
        }

        public string Code { get; }
        public decimal Price { get; }
    }
}
