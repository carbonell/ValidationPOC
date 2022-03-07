namespace ValidationExperiments;

// TODO: Consider using a TupleValue for this
public class MessageParameter
{
    public MessageParameter(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }
    public string Value { get; set; }



}