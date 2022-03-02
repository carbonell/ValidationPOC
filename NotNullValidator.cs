namespace ValidationExperiments;

public class NotNullValidator<T> : IValidator<T>
{
    public string ErrorCode => "NotNull";

    public bool Validate(T value)
    {
        return value != null;
    }
}