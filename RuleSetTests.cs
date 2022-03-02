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
        var nameValidationResult = validationRules.Validate(nameof(Person.Name), person.Name);
        var ageValidationResult = validationRules.Validate(nameof(Person.Age), person.Age);

        // Assert
        Assert.True(nameValidationResult);
        Assert.True(ageValidationResult);
    }
}