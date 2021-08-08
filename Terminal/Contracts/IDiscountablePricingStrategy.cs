namespace Terminal.Contracts
{
    public interface IDiscountablePricingStrategy : IPricingStrategy
    {
        public bool HasDiscountedPricing(string code);
    }
}
