using Terminal.Common;

namespace Terminal.Models
{
    public readonly struct BulkProduct
    {
        /// <summary>
        /// Represents a map from a product code to a bulk price definition.
        /// </summary>
        /// <param name="code"><see cref="Code"/></param>
        /// <param name="bulkPrice"><see cref="BulkPrice"/></param>
        public BulkProduct(string code, BulkPrice bulkPrice)
        {
            ProductValidationHelper.ValidateProductCodeOrThrow(code);

            Code = code;
            BulkPrice = bulkPrice;
        }

        /// <summary>
        /// Product code
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Bulk price entry
        /// </summary>
        public BulkPrice BulkPrice { get; }
    }
}
