using System;
using System.Collections.Generic;
using Xunit;

namespace ValidationExperiments;
public class RuleSetTests
{
    [Fact]
    public void Can_ValidateRuleInRuleSet()
    {
        // Arrange 
        var person = new Person("John", 20);
        var validationRules = Person.GetValidationRules();

        // Act
        var nameValidationResult = validationRules.ValidateRule(nameof(Person.Name), person.Name);
        var ageValidationResult = validationRules.ValidateRule(nameof(Person.Age), person.Age);

        // Assert
        Assert.True(nameValidationResult.IsValid);
        Assert.True(ageValidationResult.IsValid);
    }

    [Fact]
    public void Can_ValidateRuleInRuleSetWithGenericInterface()
    {
        // Arrange 
        var person = new Person("John", 20);
        var validationRules = Person.GetValidationRules();

        // Act
        var nameValidationResult = validationRules.Validate(nameof(Person.Name), person.Name);
        var ageValidationResult = validationRules.Validate(nameof(Person.Age), person.Age);

        // Assert
        Assert.True(nameValidationResult.IsValid);
        Assert.True(ageValidationResult.IsValid);
    }

    [Fact]
    public void Cant_AddDuplicateRule()
    {
        var ruleSet = new RuleSet(nameof(Person));
        var rule = new Rule<string>("Name").AddValidator(new NotNullValidator<string>());
        ruleSet.AddRule(rule);
        Assert.Throws<InvalidOperationException>(() => ruleSet.AddRule(rule));
    }

    [Fact]
    public void Cant_ValidateMissingRule()
    {
        var ruleSet = new RuleSet(nameof(Person));
        var rule = new Rule<string>("Name").AddValidator(new NotNullValidator<string>());
        ruleSet.AddRule(rule);
        Assert.Throws<KeyNotFoundException>(() => ruleSet.ValidateRule("MissingRule", "Name"));
        Assert.Throws<KeyNotFoundException>(() => ruleSet.Validate("MissingRule", "Name"));
    }
}