using System.Collections.Generic;

namespace Terminal.Contracts
{
    public interface IDiscountStrategy
    {
        public decimal CalculateTotalWithDiscounts(IEnumerable<IProduct> cart);
    }
}
