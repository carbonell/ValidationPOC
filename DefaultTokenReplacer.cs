using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ValidationExperiments;


// <summary>Uses a regular expression to perform all token replacements for a given template string in one go</summary>
public class DefaultTokenReplacer : ITokenReplacer
{
    public string BuildStringFromTemplate(string template, Dictionary<string, string> templateParameters)
    {
        template = Regex.Replace(template, @"\{(.+?)\}", m => templateParameters[m.Groups[1].Value]);
        return template;
    }
}