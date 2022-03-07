using System.Collections.Generic;
using ValidationExperiments;

public abstract class AbstractErrorMessageResolver
{
    protected ITokenReplacer _tokenReplacer = new DefaultTokenReplacer();

    protected ICollection<IErrorMessageProvider> _validationProviders = new List<IErrorMessageProvider>();

    public AbstractErrorMessageResolver()
    {
    }
}