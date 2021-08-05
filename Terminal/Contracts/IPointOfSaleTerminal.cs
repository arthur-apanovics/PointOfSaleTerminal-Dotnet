using System.Collections.Generic;

namespace Terminal.Contracts
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing(IEnumerable<IProduct> products, IPromotionStrategy? promotionStrategy = null);
        void ScanProduct(string code);
        decimal CalculateTotal();
    }
}
