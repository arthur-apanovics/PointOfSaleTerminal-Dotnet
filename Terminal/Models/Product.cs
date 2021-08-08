using System;
using Terminal.Common;

namespace Terminal.Models
{
    public readonly struct Product
    {
        /// <summary>
        /// Represents a regular product code to price mapping.
        /// </summary>
        /// <param name="code"><see cref="Code"/></param>
        /// <param name="price"><see cref="Price"/></param>
        public Product(string code, decimal price)
        {
            ProductValidationHelper.ValidateProductCodeOrThrow(code);
            ProductValidationHelper.ValidatePriceOrThrow(price);

            Code = code;
            Price = price;
        }

        /// <summary>
        /// Product code
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; }
    }
}
