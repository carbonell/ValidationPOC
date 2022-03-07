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
        var resolver = new ErrorMessageResolver();
        var culture = new CultureInfo("en-US");

        // Act
        var errorMessage = resolver.GetErrorMessage(culture, propertyName, errorCode);

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
        var resolver = new ErrorMessageResolver(errorProvider);
        var culture = new CultureInfo("en-US");

        // Act
        var errorMessage = resolver.GetErrorMessage(culture, propertyName, errorCode);

        // Assert
        Assert.Equal("Name has an invalid value.", errorMessage);

    }
}

internal class TestErrorProvider : IErrorMessageProvider
{
    public CultureInfo[] Cultures => new[] { new CultureInfo("en-US") };

    public Dictionary<string, string> Messages => new();
}