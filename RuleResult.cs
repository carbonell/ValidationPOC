using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;

// TODO: USE THIS ON RULE RESULT
public class RuleResult
{
    public IReadOnlyCollection<string> ErrorCodes
    {
        get
        {
            return _errorCodes;
        }
        protected set { }
    }

    protected List<string> _errorCodes = new List<string>();
    public RuleResult(List<string> errorCodes)
    {
        ErrorCodes = errorCodes;
    }

    public string? RuleName { get; set; }
    public string? FieldOrPropertyName { get; set; }
    public RuleResult()
    {
    }

    public RuleResult(string ruleName)
    {
        RuleName = ruleName;
    }

    public RuleResult(string ruleName, string fieldOrPropertyName) : this(ruleName)
    {
        FieldOrPropertyName = fieldOrPropertyName;
    }

    public RuleResult(IRule rule) : this(rule.Name, rule.FieldOrPropertyName)
    {
    }

    public RuleResult AddError(params string[] errorCodes)
    {
        _errorCodes.AddRange(errorCodes.ToList());
        return this;
    }

    public bool IsValid { get { return !ErrorCodes.Any(); } }

    public override string ToString()
    {
        return ToString(',');
    }

    public string ToString(char separator)
    {
        return string.Join(separator, ErrorCodes);
    }

    public static RuleResult Success() => new RuleResult();
    public static RuleResult Failed(params string[] errorMessages) => new RuleResult().AddError(errorMessages);
}