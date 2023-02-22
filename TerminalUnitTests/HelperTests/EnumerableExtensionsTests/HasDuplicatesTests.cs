using System.Collections.Generic;
using Terminal.Helpers;

namespace TerminalUnitTests.CommonTests.EnumerableExtensionsTests;

public class HasDuplicatesTests
{
    [Fact]
    public void ReturnsTrueWhenDuplicateValuesPresentIsList()
    {
        // Arrange
        var duplicatedValues = new List<string> { "A", "B", "B", "C" };

        // Act
        var actual = duplicatedValues.HasDuplicates(x => x);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void ReturnsFalseWhenNoDuplicateValuesPresentIsList()
    {
        // Arrange
        var duplicatedValues = new List<string> { "A", "B", "C", "D" };

        // Act
        var actual = duplicatedValues.HasDuplicates(x => x);

        // Assert
        actual.Should().BeFalse();
    }
}