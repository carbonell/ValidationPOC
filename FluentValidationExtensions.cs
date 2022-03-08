using System.Globalization;
using FluentValidation;
using FluentValidation.Results;

namespace ValidationExperiments;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptionsConditions<TModel, TProperty> WithDomainRule<TModel, TProperty>(this IRuleBuilder<TModel, TProperty> ruleBuilder, IRule rule, IValidationErrorMessageResolver errorResolver, string propertyName)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            var ruleResult = rule.Validate(value!);
            if (!ruleResult.IsValid)
            {
                foreach (var errorCode in ruleResult.ErrorCodes)
                {
                    var message = errorResolver.GetErrorMessage(CultureInfo.CurrentCulture, propertyName, errorCode.Key, errorCode.Value);

                    var failure = new ValidationFailure(context.PropertyName, message);
                    context.AddFailure(failure);
                }
            }
        });
    }
}