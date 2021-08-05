using System.Collections.Generic;

namespace Terminal.Contracts
{
    public interface IPromotionStrategy
    {
        public decimal CalculateTotalWithDiscounts(IEnumerable<IProduct> cart);
    }
}
