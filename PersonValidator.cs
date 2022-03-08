using FluentValidation;

namespace ValidationExperiments;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        var ruleSet = Person.GetValidationRules();
        var nameRule = ruleSet.GetRule<string>(nameof(Person.Name));
        RuleFor(r => r.Name).WithDomainRule(nameRule, "Name");
        RuleFor(r => r.Age).WithDomainRule(ruleSet.GetRule(nameof(Person.Age)), "Age");
    }
}