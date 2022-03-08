using System.Collections.Generic;

namespace ValidationExperiments;

public class GreaterThanValidator<T> : IValidator<T>
{
    T _value;
    public GreaterThanValidator(T value)
    {
        _value = value;
    }

    public string ErrorCode => "GreaterThan";


    public IEnumerable<MessageParameter> AdditionalMessageParameters => new List<MessageParameter>{
        new MessageParameter("GreaterThanValue", _value?.ToString() ?? "")
    };

    public bool Validate(T value)
    {
        return Compare(value);
    }

    private bool Compare(T val)
    {
        int comparisonResult = Comparer<T>.Default.Compare(_value, val);
        return IsGreaterThan(comparisonResult);
    }


    private bool IsGreaterThan(int comparisonResult)
    {
        return comparisonResult < 0;
    }
}