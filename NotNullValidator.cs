using System.Collections.Generic;

namespace ValidationExperiments;

public class NotNullValidator<T> : IValidator<T>
{
    public string ErrorCode => "NotNull";

    public IEnumerable<MessageParameter> AdditionalMessageParameters => new List<MessageParameter>();

    public bool Validate(T value)
    {
        return value != null;
    }
}