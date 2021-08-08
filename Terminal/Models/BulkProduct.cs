using Terminal.Common;

namespace Terminal.Models
{
    public readonly struct BulkProduct
    {
        public BulkProduct(string code, BulkPrice bulkPrice)
        {
            ProductValidationHelper.ValidateProductCodeOrThrow(code);

            Code = code;
            BulkPrice = bulkPrice;
        }

        public string Code { get; }
        public BulkPrice BulkPrice { get; }
    }
}
