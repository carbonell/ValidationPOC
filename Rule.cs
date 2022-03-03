using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;

public interface IRule
{
    string Name { get; }
    string FieldOrPropertyName { get; }
    ValidationResult Validate(object? value);
}

public interface IRule<T> : IRule
{
    ValidationResult Validate(T value);
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
    protected ICollection<string> _errorCodes = new List<string>();


    protected ICollection<IValidator<T>> _validators = new List<IValidator<T>>();

    public IRule<T> AddValidator(IValidator<T> validator)
    {
        _validators.Add(validator);
        return this;
    }



    // TODO: Return ValidationResult object with an error code aggregation.
    public ValidationResult Validate(T value)
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

        return new ValidationResult(this).AddError(_errorCodes.ToArray());
    }

    // TODO: Discuss how to handle intended vs unintended nulls, and casting errors
    public ValidationResult Validate(object? value)
    {
        T? val = default;
        if (value != null)
            val = (T)value;
        return Validate(val!);
    }


}
