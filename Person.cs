namespace ValidationExperiments;


public class Person
{
    public Person()
    {
    }

    public Person(string? name, int? age)
    {
        Name = name;
        Age = age;
    }

    public string? Name { get; set; }
    public int? Age { get; set; }

    public static RuleSet GetValidationRules()
    {
        // TODO: Create intuitive Fluent Interface for this
        var nameRule = new Rule(nameof(Name)).AddValidator(new NotNullValidator());
        var ageRule = new Rule(nameof(Age)).AddValidator(new GreaterThanValidator<int>(18));
        return new RuleSet(nameof(Person))
                .AddRule(nameRule)
                .AddRule(ageRule);
    }

}