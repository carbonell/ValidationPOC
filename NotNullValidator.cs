using System.Collections.Generic;

namespace ValidationExperiments;

public class NotNullValidator<T> : IValidator<T>
{
    public string ErrorCode => "NotNull";

    public Dictionary<string, string> AdditionalValidationMessageArguments => new();

    public bool Validate(T value)
    {
        return value != null;
    }
}