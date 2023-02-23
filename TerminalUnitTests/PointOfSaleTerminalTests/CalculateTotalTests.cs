using TerminalUnitTests.Builders;
using TerminalUnitTests.Builders.Services;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.PointOfSaleTerminalTests;

public class CalculateTotalTests
{
    [Theory]
    [MemberData(
        nameof(BulkPricingProviders.ProductCodeAndTotalsProvider),
        MemberType = typeof(BulkPricingProviders)
    )]
    public void ReturnsExpectedTotalForScannedProducts(
        BulkPricingProviders.ProductCodesAndTotalScenario scenario
    )
    {
        // Arrange
        var terminal = PointOfSaleTerminalBuilder.Build(
            withPricingService: PricingServiceBuilder.Build(
                withPricing: scenario.Pricing
            )
        );

        // Act
        foreach (var productCode in scenario.ProductCodes)
            terminal.ScanProduct(productCode);

        var actualTotal = terminal.CalculateTotal();

        // Assert
        actualTotal.Should().Be(scenario.ExpectedTotal);
    }
}