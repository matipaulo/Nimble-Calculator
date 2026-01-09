namespace Calculator.Tests.Parsers;

using Calculator.Parsers;

public class DefaultDelimiterStrategyTests
{
    [Fact]
    public void CanHandle_ReturnsTrue_WhenInputDoesNotDeclareCustomDelimiter()
    {
        var strategy = new DefaultDelimiterStrategy();
        Assert.True(strategy.CanHandle("1,2,3"));
    }

    [Fact]
    public void CanHandle_ReturnsFalse_WhenInputDeclaresCustomDelimiter()
    {
        var strategy = new DefaultDelimiterStrategy();
        Assert.False(strategy.CanHandle("//;\n1;2"));
    }

    [Fact]
    public void Extract_ReturnsInputAndDefaultDelimiters()
    {
        var strategy = new DefaultDelimiterStrategy();
        var (inputToParse, delimiters) = strategy.Extract("1,2\n3");

        Assert.Equal("1,2\n3", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }
}