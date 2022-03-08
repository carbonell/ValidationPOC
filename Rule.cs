using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;

public interface IRule
{
    string Name { get; }
    RuleResult Validate(object? value);
}

public interface IRule<T> : IRule
{
    RuleResult Validate(T value);
    IRule<T> AddValidator(IValidator<T> validator);
}
public class Rule<T> : IRule, IRule<T>
{
    public Rule(string name)
    {
        Name = name;
    }
    public string Name { get; set; }

    protected ICollection<IValidator<T>> _validators = new List<IValidator<T>>();

    public IRule<T> AddValidator(IValidator<T> validator)
    {
        _validators.Add(validator);
        return this;
    }



    public RuleResult Validate(T value)
    {
        var errorCodes = new List<string>();
        var ruleResult = new RuleResult();
        if (!_validators.Any())
            throw new InvalidOperationException($"The {Name} doesn't have any validators set up.");


        bool isValid = false;
        foreach (var validator in _validators)
        {
            isValid = validator.Validate(value!);
            if (!isValid)
                ruleResult.AddError(validator.ErrorCode, validator.AdditionalMessageParameters);
        }

        return ruleResult;
    }

    // TODO: Discuss how to handle casting errors (casting errors shouldn't silently fail)
    public RuleResult Validate(object? value)
    {
        T? val = default;
        if (value != null)
            val = (T)value;
        return Validate(val!);
    }


}
