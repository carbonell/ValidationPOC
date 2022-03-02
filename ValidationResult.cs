using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;

// TODO: USE THIS ON RULE RESULT
public class ValidationResult
{
    public List<string> ErrorCodes { get; set; } = new List<string>();
    public ValidationResult(string errorCode)
    {

    }

    public ValidationResult()
    {
    }

    public bool IsValid { get { return !ErrorCodes.Any(); } }
    private string? _errorMessage;

    public override string ToString()
    {
        return string.Concat(ErrorCodes, ',');
    }

    public static ValidationResult Success() => new ValidationResult();
    public static ValidationResult Failed(string errorMessage) => new ValidationResult(errorMessage);
}