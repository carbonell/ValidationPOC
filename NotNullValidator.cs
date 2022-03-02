namespace ValidationExperiments;

public class NotNullValidator : IValidator
{
    public string ErrorCode => "NotNull";

    public bool Validate(object? o)
    {
        return o != null;
    }
}