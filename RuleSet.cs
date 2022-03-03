using System;
using System.Collections.Generic;

namespace ValidationExperiments;
public class RuleSet
{
    public string Name { get; set; }

    // TODO: Evaluate using a ReadOnly Dictionary
    protected Dictionary<string, IRule> _rules = new Dictionary<string, IRule>();

    public RuleSet(string name)
    {
        Name = name;
    }

    public RuleSet(string name, Dictionary<string, IRule> rules) : this(name)
    {
        _rules = rules;
    }

    public RuleSet AddRule(IRule r)
    {
        // TODO: Validate duplicate dictionary keys
        if (_rules.ContainsKey(r.Name))
            throw new InvalidOperationException();
        _rules.Add(r.Name, r);
        return this;
    }

    public IRule GetRule(string ruleName)
    {
        if (!_rules.ContainsKey(ruleName))
            throw new KeyNotFoundException();
        return _rules[ruleName];
    }

    public ValidationResult ValidateRule(string ruleName, object? value)
    {
        return GetRule(ruleName).Validate(value!);
    }

    public ValidationResult Validate<T>(string ruleName, T value)
    {
        return ((IRule<T>)GetRule(ruleName)).Validate(value);
    }


}