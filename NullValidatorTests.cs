using Xunit;

namespace ValidationExperiments;

public class NullValidatorTests
{
    [Fact]
    public void Can_ReturnValidWhenNotNull()
    {
        // Arrange
        var value = "value";
        var validator = new NotNullValidator<string>();
        // Act
        var isValid = validator.Validate(value);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Can_ReturnInValidWhenNull()
    {
        // Arrange
        string? value = null;
        var validator = new NotNullValidator<string>();
        // Act
        var isValid = validator.Validate(value!);

        // Assert
        Assert.False(isValid);
    }
}