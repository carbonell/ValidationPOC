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
        // TODO: Enable easy generic type setting through the fluent interface

        var ruleBuilder = new RuleSetBuilder<Person>();

        ruleBuilder.RuleFor(r => r.Name).NotNull();
        ruleBuilder.RuleFor(r => r.Age).GreaterThan(18);
        return ruleBuilder.Build();

    }
}