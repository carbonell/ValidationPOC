namespace ValidationExperiments;


public static class RuleExtensions
{
    public static IRule<T> NotNull<T>(this IRule<T> rule)
    {
        rule.AddValidator(new NotNullValidator<T>());
        return rule;
    }

    public static IRule<T> GreaterThan<T>(this IRule<T> rule, T value)
    {
        rule.AddValidator(new GreaterThanValidator<T>(value));
        return rule;
    }
}