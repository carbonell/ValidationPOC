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
}