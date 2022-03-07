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
        Assert.Equal("Name should not be empty", errorMessage);
    }
}