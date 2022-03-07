using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace ValidationExperiments;

public class ErrorMessageResolverTests
{

    [Fact]
    public void Can_ResolveErrorMessage()
    {
        // Arrange
        var propertyName = "Name";
        var errorCode = "NotNull";
        var resolver = new ValidationErrorMessageResolver();
        var culture = new CultureInfo("en-US");
        var messageParameters = new List<MessageParameter>();

        // Act
        var errorMessage = resolver.GetErrorMessage(culture, propertyName, errorCode, messageParameters);

        // Assert
        Assert.Equal("Name should not be empty.", errorMessage);
    }

    [Fact]
    public void Can_ResolveErrorMessageWithDefaultValue()
    {
        // Arrange
        var propertyName = "Name";
        var errorCode = "NotNull";
        var errorProvider = new List<IErrorMessageProvider> { new TestErrorProvider() };
        var resolver = new ValidationErrorMessageResolver(errorProvider);
        var culture = new CultureInfo("en-US");
        var messageParameters = new List<MessageParameter>();

        // Act
        var errorMessage = resolver.GetErrorMessage(culture, propertyName, errorCode, messageParameters);

        // Assert
        Assert.Equal("Name has an invalid value.", errorMessage);

    }
}

internal class TestErrorProvider : IErrorMessageProvider
{
    public CultureInfo[] Cultures => new[] { new CultureInfo("en-US") };

    public Dictionary<string, string> Messages => new();
}