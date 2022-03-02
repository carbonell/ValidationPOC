
namespace ValidationExperiments;

public interface IValidator
{
    public string ErrorCode { get; }
    bool Validate(object o);
}