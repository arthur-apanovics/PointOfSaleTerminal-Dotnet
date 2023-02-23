using TerminalUnitTests.Builders.Services;
using TerminalUnitTests.TestDataProviders;

namespace TerminalUnitTests.ServiceTests.PricingServiceTests;

public class CalculateTotalForTests
{
    [Theory]
    [MemberData(
        nameof(BulkPricingProviders.ProductCodeAndTotalsProvider),
        MemberType = typeof(BulkPricingProviders)
    )]
    public void ReturnsExpectedTotals(
        BulkPricingProviders.ProductCodesAndTotalScenario scenario
    )
    {
        // Arrange
        var service =
            PricingServiceBuilder.Build(withPricing: scenario.Pricing);

        // Act
        var actualTotal = service.CalculateTotalFor(scenario.ProductCodes);

        // Assert
        actualTotal.Should().Be(scenario.ExpectedTotal);
    }
}