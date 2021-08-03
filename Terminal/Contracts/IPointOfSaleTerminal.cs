using System.Collections.Generic;

namespace Terminal.Contracts
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing(IDictionary<string, decimal> productPriceMap);
        void ScanProduct(string code);
        decimal CalculateTotal();
    }
}
