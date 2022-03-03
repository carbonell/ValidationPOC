using System;
using Xunit;

namespace ValidationExperiments;

public class RuleTests
{
    [Fact]
    public void Can_ReturnValidWhenRuleIsValid()
    {
        // Arrange
        var rule = new Rule<string>("Rule");
        rule.AddValidator(new NotNullValidator<string>());
        var value = "value";

        // Act
        var result = rule.Validate(value);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Can_ReturnInvalidWhenRuleIsInValid()
    {
        var rule = new Rule<string>("Rule");
        rule.AddValidator(new NotNullValidator<string>());
        string? value = null;

        // Act
        var result = rule.Validate(value!);

        // Assert
        Assert.False(result.IsValid);
    }

    // [Fact] TODO: Decide on the right behavior for this
    public void Can_ReturnInvalidWhenValueIsNull()
    {
        var rule = new Rule<int>("Rule");
        rule.AddValidator(new GreaterThanValidator<int>(10));
        string? value = null;

        // Act
        var result = rule.Validate(value);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Will_ThrowExceptionOnEmptyValidators()
    {
        var rule = new Rule<string>("Rule");
        string? value = "value";
        // Assert
        Assert.Throws<InvalidOperationException>(() => rule.Validate(value));

    }



}