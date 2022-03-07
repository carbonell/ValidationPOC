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
            var errorResolver = new ValidationErrorMessageResolver();
            if (!ruleResult.IsValid)
            {

                foreach (var errorCode in ruleResult.ErrorCodes)
                {
                    var fieldName = ruleResult.FieldOrPropertyName ?? "";
                    var message = errorResolver.GetErrorMessage(System.Globalization.CultureInfo.CurrentCulture, fieldName, errorCode.Key, errorCode.Value);

                    var failure = new ValidationFailure(fieldName, message);
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