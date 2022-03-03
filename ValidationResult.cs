using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;

// TODO: USE THIS ON RULE RESULT
public class ValidationResult
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
    public ValidationResult(List<string> errorCodes)
    {
        ErrorCodes = errorCodes;
    }

    public string? RuleName { get; set; }
    public string? FieldOrPropertyName { get; set; }
    public ValidationResult()
    {
    }

    public ValidationResult(string ruleName)
    {
        RuleName = ruleName;
    }

    public ValidationResult(string ruleName, string fieldOrPropertyName) : this(ruleName)
    {
        FieldOrPropertyName = fieldOrPropertyName;
    }

    public ValidationResult(IRule rule) : this(rule.Name, rule.FieldOrPropertyName)
    {
    }

    public ValidationResult AddError(params string[] errorCodes)
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

    public static ValidationResult Success() => new ValidationResult();
    public static ValidationResult Failed(params string[] errorMessages) => new ValidationResult().AddError(errorMessages);
}