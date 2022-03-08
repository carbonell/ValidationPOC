using System.Collections.Generic;

namespace ValidationExperiments;

public class NotDefaultValidator<T> : IValidator<T>
{
    public string ErrorCode => "NotDefault";

    public IEnumerable<MessageParameter> AdditionalMessageParameters => new List<MessageParameter>();

    public bool Validate(T value)
    {
        return !EqualityComparer<T>.Default.Equals(value, default(T));
    }
}