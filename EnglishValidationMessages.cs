using System.Collections.Generic;
using System.Globalization;

namespace ValidationExperiments;

public interface IErrorMessageProvider
{
    CultureInfo[] Cultures { get; }
    Dictionary<string, string> Messages { get; }

}

public class EnglishValidationMessages : IErrorMessageProvider
{
    public CultureInfo[] Cultures => new[]{
            new CultureInfo("en-US")
    };


    public Dictionary<string, string> Messages => new()
    {
        { "NotNull", "{FieldOrPropertyName} should not be empty." },
        { "Default", "{FieldOrPropertyName} has an invalid value." },
    };

    public static EnglishValidationMessages Load()
    {
        return new EnglishValidationMessages();
    }


}