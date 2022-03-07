
namespace ValidationExperiments;

public interface IValidator<T>
{
    public string ErrorCode { get; }
    bool Validate(T value);
}


