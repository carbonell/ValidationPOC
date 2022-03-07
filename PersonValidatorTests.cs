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
        .WithErrorMessage("'Name' must not be empty.");
    }
}