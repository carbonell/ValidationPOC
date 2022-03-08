using System;
using Xunit;

namespace ValidationExperiments;

public class NotDefaultValidatorTests
{
    [Fact]
    public void Can_ReturnValid()
    {
        // Arrange
        var value = DateTime.Today;
        var validator = new NotDefaultValidator<DateTime>();
        // Act
        var isValid = validator.Validate(value);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Can_ReturnInValid()
    {
        // Arrange
        var value = DateTime.MinValue;
        var validator = new NotDefaultValidator<DateTime>();
        // Act
        var isValid = validator.Validate(value);

        // Assert
        Assert.False(isValid);
    }
}