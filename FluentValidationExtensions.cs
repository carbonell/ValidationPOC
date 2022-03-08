using System.Globalization;
using FluentValidation;
using FluentValidation.Results;

namespace ValidationExperiments;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptionsConditions<TModel, TProperty> WithDomainRule<TModel, TProperty>(this IRuleBuilder<TModel, TProperty> ruleBuilder, IRule rule, string propertyName)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            var ruleResult = rule.Validate(value!);
            var errorResolver = new ValidationErrorMessageResolver();
            if (!ruleResult.IsValid)
            {
                foreach (var errorCode in ruleResult.ErrorCodes)
                {
                    var fieldName = propertyName;
                    var message = errorResolver.GetErrorMessage(CultureInfo.CurrentCulture, fieldName, errorCode.Key, errorCode.Value);

                    var failure = new ValidationFailure(fieldName, message);
                    context.AddFailure(failure);
                }
            }
        });
    }
}