namespace Terminal.Models;

public interface IProductPrice
{
    public string ProductCode { get; }
    decimal CalculateTotalFor(int productQuantity);
}

