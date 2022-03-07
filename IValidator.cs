
using System.Collections.Generic;

namespace ValidationExperiments;

public interface IValidator<T> : IValidator
{
    bool Validate(T value);
}


public interface IValidator
{
    public string ErrorCode { get; }
    public Dictionary<string, string> AdditionalValidationMessageArguments { get; }



}


