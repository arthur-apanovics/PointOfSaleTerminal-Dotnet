using System.Collections.Generic;

namespace Terminal.Contracts
{
    public interface IPricingStrategy
    {
        public bool HasPricing(string code);
        public decimal GetPrice(string code);
        public decimal CalculateTotal(string code, int quantity);
        public decimal CalculateTotal(IEnumerable<string> codes);
    }
}
