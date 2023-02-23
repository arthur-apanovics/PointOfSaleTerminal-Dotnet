namespace Terminal.Models;

public record BulkPrice : IProductPrice
{
    public string ProductCode { get; }

    private decimal SingleUnitPrice { get; }
    private int BulkUnitSize { get; }
    private decimal BulkUnitPrice { get; }

    private BulkPrice(
        string productCode,
        decimal singleUnitPrice,
        int bulkUnitSize,
        decimal bulkUnitPrice
    )
    {
        ProductCode = productCode;
        SingleUnitPrice = singleUnitPrice;
        BulkUnitSize = bulkUnitSize;
        BulkUnitPrice = bulkUnitPrice;
    }


    public decimal CalculateTotalFor(int productQuantity)
    {
        var result = 0m;
        var remainder = productQuantity;
        while (remainder >= BulkUnitSize)
        {
            result += BulkUnitPrice;
            remainder -= BulkUnitSize;
        }

        if (remainder > 0)
            result += remainder * SingleUnitPrice;

        return result;
    }

    public static BulkPrice Create(
        string productCode,
        decimal singleUnitPrice,
        int bulkUnitSize,
        decimal bulkUnitPrice
    ) =>
        new(
            productCode,
            singleUnitPrice,
            bulkUnitSize,
            bulkUnitPrice
        );
}