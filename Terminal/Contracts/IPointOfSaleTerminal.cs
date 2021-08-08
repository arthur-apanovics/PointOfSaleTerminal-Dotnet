using System.Collections.Generic;

namespace Terminal.Contracts
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing(IPricingStrategy pricingStrategy);
        void ScanProduct(string code);
        decimal CalculateTotal();
    }
}
