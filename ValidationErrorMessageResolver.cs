using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ValidationExperiments;

public class ValidationErrorMessageResolver : AbstractErrorMessageResolver
{
    public ValidationErrorMessageResolver()
    {
        _validationProviders.Add(EnglishValidationMessages.Load());
    }

    public ValidationErrorMessageResolver(ITokenReplacer tokenReplacer)
    {
        _tokenReplacer = tokenReplacer;
    }

    public ValidationErrorMessageResolver(ICollection<IErrorMessageProvider> validationProviders)
    {
        _validationProviders = validationProviders;
    }

    public string GetErrorMessage(CultureInfo culture, string fieldOrPropertyName, string errorCode, IEnumerable<MessageParameter> additionalMessageParameters)
    {
        return BuildTemplate(GetErrorTemplate(culture, errorCode), fieldOrPropertyName, additionalMessageParameters);
    }

    protected string GetErrorTemplate(CultureInfo culture, string errorCode)
    {
        var provider = _validationProviders.FirstOrDefault(p => p.Cultures.Any(c => c.Name == culture.Name));
        if (provider == null)
        {
            provider = _validationProviders.First(p => p.Cultures.Any(c => c.Name == "en-US"));
        }
        if (provider.Messages.ContainsKey(errorCode))
        {
            return provider.Messages[errorCode];
        }
        else if (provider.Messages.ContainsKey("Default"))
            return provider.Messages["Default"];
        else
        {
            return "{FieldOrPropertyName} has an invalid value.";
        }
    }

    private string BuildTemplate(string errorTemplate, string fieldOrPropertyName, IEnumerable<MessageParameter> messageParameters)
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("FieldOrPropertyName", fieldOrPropertyName);
        foreach (var p in messageParameters)
        {
            parameters.Add(p.Name, p.Value);
        }
        errorTemplate = _tokenReplacer.BuildStringFromTemplate(errorTemplate, parameters);
        return errorTemplate;
    }
}

