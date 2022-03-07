using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;

public interface IRule
{
    string Name { get; }
    string FieldOrPropertyName { get; }
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
        FieldOrPropertyName = name;
    }

    public Rule(string name, string fieldOrPropertyName)
    {
        Name = name;
        FieldOrPropertyName = fieldOrPropertyName;
    }
    public string Name { get; set; }
    public string FieldOrPropertyName { get; set; }

    protected ICollection<IValidator<T>> _validators = new List<IValidator<T>>();

    public IRule<T> AddValidator(IValidator<T> validator)
    {
        _validators.Add(validator);
        return this;
    }



    // TODO: Return ValidationResult object with an error code aggregation.
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

    // TODO: Discuss how to handle intended vs unintended nulls, and casting errors
    public RuleResult Validate(object? value)
    {
        T? val = default;
        if (value != null)
            val = (T)value;
        return Validate(val!);
    }


}
