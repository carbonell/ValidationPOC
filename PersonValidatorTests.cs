using FluentValidation.TestHelper;
using Xunit;

namespace ValidationExperiments;

public class PersonValidatorTests
{
    [Fact]
    public void Can_UseDomainRule()
    {
        var validator = new PersonValidator();
        var model = new Person { Name = null };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(person => person.Name)
        .WithErrorMessage("Name should not be empty.");

        result.ShouldHaveValidationErrorFor(p => p.Age)
        .WithErrorMessage("Age should be greater than 18.");
    }
}