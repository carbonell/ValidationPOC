using Xunit;

namespace ValidationExperiments;

public class ValidationResultTests
{

    [Fact]
    public void Can_ReturnValidOnSuccess()
    {
        var result = ValidationResult.Success();
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Can_ReturnInvalidOnFailure()
    {
        var result = ValidationResult.Failed("Some Error");
        Assert.False(result.IsValid);
        Assert.Equal(1, result.ErrorCodes.Count);
    }

    [Fact]
    public void Can_AppendErrorCodes()
    {
        var result = ValidationResult.Failed("Some Error");
        var result2 = ValidationResult.Failed("Error 2");
        var result3 = ValidationResult.Failed("Error 3");
        result.Append(result2).Append(result3);
        Assert.Equal(3, result.ErrorCodes.Count);

    }
}