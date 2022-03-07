using Xunit;

namespace ValidationExperiments;

public class RuleResultTests
{

    [Fact]
    public void Can_ReturnValidOnSuccess()
    {
        var result = RuleResult.Success();
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Can_ReturnInvalidOnFailure()
    {
        var result = RuleResult.Failed("Some Error");
        Assert.False(result.IsValid);
        Assert.Equal(1, result.ErrorCodes.Count);
    }
}