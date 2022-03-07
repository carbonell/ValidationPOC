using System.Collections.Generic;

namespace ValidationExperiments;

public interface ITokenReplacer
{
    string BuildStringFromTemplate(string template, Dictionary<string, string> templateParameters);
}
