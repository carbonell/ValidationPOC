using System.Collections.Generic;

namespace ValidationExperiments;
public class RuleSet
{
    public string Name { get; set; }

    // TODO: Evaluate using a ReadOnly Dictionary
    protected Dictionary<string, Rule> _rules = new Dictionary<string, Rule>();

    public RuleSet(string name)
    {
        Name = name;
    }

    public RuleSet AddRule(Rule r)
    {
        // TODO: Validate duplicate dictionary keys
        _rules.Add(r.Name, r);
        return this;
    }

    public bool Validate(string ruleName, object? value)
    {
        if (!_rules.ContainsKey(ruleName))
            throw new KeyNotFoundException();
        return _rules[ruleName].Validate(value!);
    }


}