using System.Collections.Generic;

namespace ValidationExperiments;

public interface IErrorSource
{
    string ErrorCode { get; }
    Dictionary<string, string> AdditionalValidationMessageArguments { get; }

}