using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;

public class Rule
{
    public Rule(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
    protected ICollection<string> _errorCodes = new List<string>();


    protected ICollection<IValidator> _validators = new List<IValidator>();

    public Rule AddValidator(IValidator validator)
    {
        _validators.Add(validator);
        return this;
    }

    // TODO: Discuss how to handle intended vs unintended nulls, and casting errors
    // TODO: Return ValidationResult object with an error code aggregation.
    public bool Validate(object? value)
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
}
