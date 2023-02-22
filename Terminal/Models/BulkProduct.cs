using Terminal.Common;

namespace Terminal.Models;

public record BulkProduct
{
    private BulkProduct(string productCode, BulkProductPrice bulkPrice)
    {
        ProductCode = productCode;
        BulkPrice = bulkPrice;
    }

    /// <summary>
    ///     Product code
    /// </summary>
    public string ProductCode { get; }

    /// <summary>
    ///     Bulk price entry
    /// </summary>
    public BulkProductPrice BulkPrice { get; }

    /// <summary>
    ///     Represents a map from a product code to a bulk price definition.
    /// </summary>
    /// <param name="productCode">
    ///     <see cref="ProductCode" />
    /// </param>
    /// <param name="bulkPrice">
    ///     <see cref="BulkPrice" />
    /// </param>
    public static BulkProduct Create(
        string productCode,
        BulkProductPrice bulkPrice
    )
    {
        ProductValidationHelper.ValidateProductCodeOrThrow(productCode);

        return new BulkProduct(productCode, bulkPrice);
    }
}