namespace Calculator.Tests.Parsers;

using Calculator.Parsers;

public class MultiCharacterDelimiterStrategyTests
{
    [Fact]
    public void CanHandle_ReturnsTrue_WhenInputStartsWithBracketedDelimiter()
    {
        var strategy = new MultiCharacterDelimiterStrategy();
        Assert.True(strategy.CanHandle("//[***]\n1***2"));
    }

    [Fact]
    public void Extract_ReturnsMultipleDelimiters_WhenSectionContainsSeveral()
    {
        var strategy = new MultiCharacterDelimiterStrategy();
        var (inputToParse, delimiters) = strategy.Extract("//[*][!!][r9]\n11r922*hh*33!!44");

        Assert.Equal("11r922*hh*33!!44", inputToParse);
        Assert.Equal(["*", "!!", "r9", "\n"], delimiters);
    }

    [Fact]
    public void Extract_FallsBackToDefaults_WhenSectionIsMalformed()
    {
        var strategy = new MultiCharacterDelimiterStrategy();
        var (inputToParse, delimiters) = strategy.Extract("//[***\n1***2");

        Assert.Equal("//[***\n1***2", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }
}