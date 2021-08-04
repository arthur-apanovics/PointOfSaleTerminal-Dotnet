namespace Terminal.Contracts
{
    public interface IProduct
    {
        public string Code { get; init; }
        public decimal Price { get; init; }
    }
}
