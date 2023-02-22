using System.Collections.Generic;
using System.Linq;

namespace TerminalUnitTests.TestDataProviders;

public class ProductValueProviders
{
    private static readonly string[] ValidProductCodeValues =
    {
        "A",
        "B",
        "C",
        "D",
        "Coke",
        "ProductThatHasSomewhatOfALengthyNameThatProbablyShouldNotBeThatLongInTheFirstPlace",
    };

    private static readonly string?[] InvalidProductCodeValues =
    {
        null, "", " ", " A", " A ", "A ", "A B"
    };

    private static readonly decimal[] InvalidProductPriceValues =
    {
        0m, -1m, decimal.MinValue
    };

    public static IEnumerable<object[]> ValidProductCodes() =>
        ValidProductCodeValues.Select(code => new object[] { code });

    public static IEnumerable<object[]> InvalidProductCodes() =>
        InvalidProductCodeValues.Select(code => new object[] { code! });

    public static IEnumerable<object[]> InvalidProductPrices() =>
        InvalidProductPriceValues.Select(price => new object[] { price });
}