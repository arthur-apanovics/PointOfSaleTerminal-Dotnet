using System;
using Terminal.Models;
using Terminal.Services;
using TerminalUnitTests.Builders.Models;

namespace TerminalUnitTests.ServiceTests.PricingServiceTests;

public class ConstructorTests
{
    [Fact]
    public void ThrowsWhenDuplicateProductCodePresentInPricing()
    {
        // Arrange
        var invalidPricing = new IProductPrice[]
        {
            SingleUnitPriceBuilder.Build("Foo"),
            SingleUnitPriceBuilder.Build("Bar"),
            SingleUnitPriceBuilder.Build("Baz"),
            SingleUnitPriceBuilder.Build("Baz"),
        };

        // Act
        var actual = () => new PricingService(invalidPricing);


        // Assert
        actual.Should().Throw<ArgumentException>();
    }
}