using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;

public interface IRule
{
    public string Name { get; }
    bool Validate(object? value);
}

public interface IRule<T>
{
    bool Validate(T value);
}
public class Rule<T> : IRule, IRule<T>
{
    public Rule(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
    protected ICollection<string> _errorCodes = new List<string>();


    protected ICollection<IValidator<T>> _validators = new List<IValidator<T>>();

    public Rule<T> AddValidator(IValidator<T> validator)
    {
        _validators.Add(validator);
        return this;
    }

    // TODO: Return ValidationResult object with an error code aggregation.
    public bool Validate(T value)
    {
        if (!_validators.Any())
            throw new InvalidOperationException($"The {Name} doesn't have any validators set up.");


        bool isValid = false;
        foreach (var validator in _validators)
        {
            isValid = validator.Validate(value!);
            if (!isValid)
                _errorCodes.Add(validator.ErrorCode);
        }

        return !_errorCodes.Any();
    }

    // TODO: Discuss how to handle intended vs unintended nulls, and casting errors
    public bool Validate(object? value)
    {
        var val = (T)value!;
        return Validate(val);
    }
}
