using System.Collections.Generic;
using Terminal.Models;

namespace TerminalUnitTests.Models.SingleAndBulkUnitPriceTests;

public class CalculateTotalForTests
{
    [Theory]
    [MemberData(nameof(BulkPriceScenarioProvider))]
    public void ReturnsExpectedTotalPrice(BulkPriceScenario scenario)
    {
        // Arrange
        var price = SingleAndBulkUnitPrice.Create(
            productCode: "_",
            singleUnitPrice: scenario.SingleUnitPrice,
            bulkUnitSize: scenario.BulkUnitSize,
            bulkUnitPrice: scenario.BulkUnitPrice
        );

        // Act
        var actualTotal = price.CalculateTotalFor(scenario.UnitQuantity);

        // Assert
        actualTotal.Should().Be(scenario.ExpectedTotalPrice);
    }

    public static IEnumerable<object[]> BulkPriceScenarioProvider()
    {
        var scenarios = new BulkPriceScenario[]
        {
            new(
                SingleUnitPrice: 1.25m,
                BulkUnitSize: 3,
                BulkUnitPrice: 3m,
                UnitQuantity: 3,
                ExpectedTotalPrice: 3m
            ),
            new(
                SingleUnitPrice: 1.25m,
                BulkUnitSize: 3,
                BulkUnitPrice: 3m,
                UnitQuantity: 5,
                ExpectedTotalPrice: 5.5m
            ),
            new(
                SingleUnitPrice: 1m,
                BulkUnitSize: 6,
                BulkUnitPrice: 5m,
                UnitQuantity: 6,
                ExpectedTotalPrice: 5m
            ),
            new(
                SingleUnitPrice: 1m,
                BulkUnitSize: 6,
                BulkUnitPrice: 5m,
                UnitQuantity: 12,
                ExpectedTotalPrice: 10m
            ),
            new(
                SingleUnitPrice: 1m,
                BulkUnitSize: 6,
                BulkUnitPrice: 5m,
                UnitQuantity: 13,
                ExpectedTotalPrice: 11m
            ),
        };

        foreach (var scenario in scenarios)
            yield return new object[] { scenario };
    }

    public record BulkPriceScenario(
        decimal SingleUnitPrice,
        int BulkUnitSize,
        decimal BulkUnitPrice,
        int UnitQuantity,
        decimal ExpectedTotalPrice
    );
}