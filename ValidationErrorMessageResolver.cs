using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ValidationExperiments;

public interface IValidationErrorMessageResolver
{
    string GetErrorMessage(CultureInfo culture, string fieldOrPropertyName, string errorCode, IEnumerable<MessageParameter> additionalMessageParameters);
}

public class ValidationErrorMessageResolver : AbstractErrorMessageResolver, IValidationErrorMessageResolver
{
    private readonly Dictionary<string, string> _cache = new();

    public ValidationErrorMessageResolver(Dictionary<string, string> cache, ITokenReplacer tokenReplacer)
    {
        _cache = cache;
        _tokenReplacer = tokenReplacer;
    }

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
        var key = GetErrorKey(culture, fieldOrPropertyName, errorCode, additionalMessageParameters);
        if (!_cache.ContainsKey(key))
        {
            var template = BuildTemplate(GetErrorTemplate(culture, errorCode), fieldOrPropertyName, additionalMessageParameters);
            _cache.Add(key, string.Intern(template));
        }
        return _cache[key];
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

    private string GetErrorKey(CultureInfo culture, string fieldOrPropertyName, string errorCode, IEnumerable<MessageParameter> additionalMessageParameters)
    {
        var parameters = string.Join('-', additionalMessageParameters.Select(s => s.Value));
        return $"{culture.Name}-{fieldOrPropertyName}-{errorCode}" + (!string.IsNullOrEmpty(parameters) ? $"-{parameters}" : "");
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

