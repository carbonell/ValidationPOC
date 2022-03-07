using System.Collections.Generic;
using System.Linq;

namespace ValidationExperiments;
public class ValidationResult
{
    protected IReadOnlyDictionary<string, RuleResult> Errors { get { return _errors; } }

    protected Dictionary<string, RuleResult> _errors = new();
    public bool IsValid { get { return _errors.Values.All(r => r.IsValid); } }

}
