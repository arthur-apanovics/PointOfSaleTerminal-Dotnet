using System;
using Terminal.Contracts;

namespace Terminal.Models
{
    public class Product : IProduct
    {
        private readonly decimal _price;
        private readonly string _code = string.Empty;

        public Product(string code, decimal price)
        {
            Code = code;
            Price = price;
        }

        public string Code
        {
            get => _code;
            init
            {
                if (string.IsNullOrWhiteSpace(value) || value.Contains(' '))
                {
                    throw new ArgumentException("Product code must not contain spaces and not be empty");
                }

                _code = value;
            }
        }

        public decimal Price
        {
            get => _price;
            init
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Product price cannot be zero or negative");
                }

                _price = value;
            }
        }
    }
}
