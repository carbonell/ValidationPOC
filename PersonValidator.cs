using FluentValidation;
using FluentValidation.Results;

namespace ValidationExperiments;

public class PersonValidator : AbstractValidator<Person>
{
    // https://blog.devgenius.io/formatting-strings-using-templates-in-c-ba74ca53c07f
    public PersonValidator()
    {
        var ruleSet = Person.GetValidationRules();
        var rule = ruleSet.GetRule<string>(nameof(Person.Name));
        RuleFor(r => r.Name).Custom((name, context) =>
        {
            var ruleResult = rule.Validate(name!);
            if (!ruleResult.IsValid)
            {
                foreach (var errorCode in ruleResult.ErrorCodes)
                {
                    var failure = new ValidationFailure(ruleResult.FieldOrPropertyName, @"{PropertyName} must not be empty");
                    failure.ErrorCode = GetErrorMessage(errorCode);
                    context.AddFailure(failure);
                }
            }
        });
    }

    private string GetErrorMessage(string estimatorErrorCode)
    {
        switch (estimatorErrorCode)
        {
            case "NotNull":
                return "NotNullValidator";
            default:
                return "";
        }
    }
}