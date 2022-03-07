using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;

// TODO: USE THIS ON RULE RESULT
public class RuleResult
{


    public IReadOnlyDictionary<string, IEnumerable<MessageParameter>> ErrorCodes => _errorCodes;

    protected Dictionary<string, IEnumerable<MessageParameter>> _errorCodes = new();
    public RuleResult(Dictionary<string, IEnumerable<MessageParameter>> errorCodes)
    {
        _errorCodes = errorCodes;
    }

    public string? FieldOrPropertyName { get; set; }
    public Dictionary<string, string> AdditionalValidationMessageArguments => new();
    public RuleResult()
    {
    }

    public RuleResult(string fieldOrPropertyName)
    {
        FieldOrPropertyName = fieldOrPropertyName;
    }

    public RuleResult(IRule rule) : this(rule.FieldOrPropertyName)
    {
    }

    public RuleResult AddError(string errorCode)
    {
        _errorCodes.Add(errorCode, new List<MessageParameter>());
        return this;
    }
    public RuleResult AddError(string errorCode, IEnumerable<MessageParameter> messageParameters)
    {
        _errorCodes.Add(errorCode, messageParameters);
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
    public static RuleResult Failed(string errorCode, IEnumerable<MessageParameter> messageParameters) => new RuleResult().AddError(errorCode, messageParameters);
    public static RuleResult Failed(string errorCode) => new RuleResult().AddError(errorCode);
}