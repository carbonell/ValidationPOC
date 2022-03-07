using System.Collections.Generic;
using System.Globalization;

namespace ValidationExperiments;

public class ErrorMessageResolver
{

    protected ITokenReplacer _tokenReplacer = new DefaultTokenReplacer();
    protected Dictionary<string, string> _errorMessages = new()
    {
        { "NotNull", "{FieldOrPropertyName} should not be empty" }
    };

    public ErrorMessageResolver()
    {
    }

    public ErrorMessageResolver(ITokenReplacer tokenReplacer)
    {
        _tokenReplacer = tokenReplacer;
    }

    public string GetErrorMessage(CultureInfo culture, string fieldOrPropertyName, string errorCode)
    {
        return BuildTemplate(fieldOrPropertyName, _errorMessages[errorCode]);
    }

    private string BuildTemplate(string fieldOrPropertyName, string errorTemplate)
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("FieldOrPropertyName", fieldOrPropertyName);
        errorTemplate = _tokenReplacer.BuildStringFromTemplate(errorTemplate, parameters);
        return errorTemplate;
    }
}