using Xunit;

namespace ValidationExperiments;

public class GreaterThanValidatorTests
{
    [Fact]
    public void Can_ReturnValid()
    {
        // Arrange
        var minValue = 18;
        var validator = new GreaterThanValidator<int>(minValue);
        var value = 21;

        // Act
        var isValid = validator.Validate(value);
        // Assert
        Assert.True(isValid);
    }


    [Fact]
    public void Can_ReturnInvalid()
    {
        // Arrange
        var minValue = 18;
        var validator = new GreaterThanValidator<int>(minValue);
        var value = 17;

        // Act
        var isValid = validator.Validate(value);
        // Assert
        Assert.False(isValid);
    }
}
