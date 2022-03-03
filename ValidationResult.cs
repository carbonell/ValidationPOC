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


    public ValidationResult()
    {
    }

    public ValidationResult(params string[] errorMessages)
    {
        _errorCodes = errorMessages.ToList();
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

    public ValidationResult Append(ValidationResult result)
    {
        _errorCodes.AddRange(result.ErrorCodes);
        return this;
    }

    public static ValidationResult Success() => new ValidationResult();
    public static ValidationResult Failed(params string[] errorMessages) => new ValidationResult(errorMessages);
}