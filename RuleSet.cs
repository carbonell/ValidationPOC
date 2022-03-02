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
        _rules.Add(r.Name, r);
        return this;
    }

    public bool ValidateRule(string ruleName, object? value)
    {
        if (!_rules.ContainsKey(ruleName))
            throw new KeyNotFoundException();
        return _rules[ruleName].Validate(value!);
    }

    public bool Validate<T>(string ruleName, T value)
    {
        if (!_rules.ContainsKey(ruleName))
            throw new KeyNotFoundException();
        return ((IRule<T>)_rules[ruleName]).Validate(value);
    }


}