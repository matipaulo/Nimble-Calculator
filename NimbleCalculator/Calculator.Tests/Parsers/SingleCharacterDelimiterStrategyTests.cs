namespace Calculator.Tests.Parsers;

using Calculator.Parsers;

public class SingleCharacterDelimiterStrategyTests
{
    [Fact]
    public void CanHandle_ReturnsTrue_WhenInputUsesSingleCharacterDelimiter()
    {
        var strategy = new SingleCharacterDelimiterStrategy();
        Assert.True(strategy.CanHandle("//;\n1;2"));
    }

    [Fact]
    public void CanHandle_ReturnsFalse_WhenInputUsesBracketedDelimiter()
    {
        var strategy = new SingleCharacterDelimiterStrategy();
        Assert.False(strategy.CanHandle("//[***]\n1***2"));
    }

    [Fact]
    public void Extract_ReturnsDefaultDelimiters_WhenHeaderIsIncomplete()
    {
        var strategy = new SingleCharacterDelimiterStrategy();
        var (inputToParse, delimiters) = strategy.Extract("//\n");

        Assert.Equal("//\n", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    [Fact]
    public void Extract_ReturnsCustomDelimiter_WhenProperlyFormatted()
    {
        var strategy = new SingleCharacterDelimiterStrategy();
        var (inputToParse, delimiters) = strategy.Extract("//;\n1;2;3");

        Assert.Equal("1;2;3", inputToParse);
        Assert.Equal([";", "\n"], delimiters);
    }
}