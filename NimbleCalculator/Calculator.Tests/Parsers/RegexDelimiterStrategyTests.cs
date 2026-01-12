namespace Calculator.Tests.Parsers;

using Calculator.Parsers;

public class RegexDelimiterStrategyTests
{
    private readonly RegexDelimiterStrategy _strategy = new();

    // Default format tests
    [Fact]
    public void CanHandle_ReturnsTrue_WhenInputDoesNotStartWithDoubleSlash()
    {
        Assert.True(_strategy.CanHandle("1,2,3"));
    }

    [Fact]
    public void Extract_ReturnsDefaultDelimiters_WhenNoCustomDelimiterDefined()
    {
        var (inputToParse, delimiters) = _strategy.Extract("1,2,3");

        Assert.Equal("1,2,3", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    [Fact]
    public void Extract_HandlesCommaAndNewlineDelimiters()
    {
        var (inputToParse, delimiters) = _strategy.Extract("1,2\n3");

        Assert.Equal("1,2\n3", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    // Single character delimiter tests
    [Fact]
    public void CanHandle_ReturnsTrue_WhenInputHasSingleCharacterDelimiter()
    {
        Assert.True(_strategy.CanHandle("//;\n1;2"));
    }

    [Fact]
    public void Extract_ReturnsSingleCharacterDelimiter_WhenProperlyFormatted()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//#\n2#5");

        Assert.Equal("2#5", inputToParse);
        Assert.Equal(["#", "\n"], delimiters);
    }

    [Fact]
    public void Extract_IncludesNewlineAsAdditionalDelimiter_ForSingleCharFormat()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//;\n1;2\n3");

        Assert.Equal("1;2\n3", inputToParse);
        Assert.Contains(";", delimiters);
        Assert.Contains("\n", delimiters);
    }

    // Single bracketed delimiter tests
    [Fact]
    public void CanHandle_ReturnsTrue_WhenInputHasBracketedDelimiter()
    {
        Assert.True(_strategy.CanHandle("//[***]\n1***2"));
    }

    [Fact]
    public void Extract_ReturnsMultiCharacterDelimiter_WhenProperlyFormatted()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[***]\n11***22***33");

        Assert.Equal("11***22***33", inputToParse);
        Assert.Equal(["***", "\n"], delimiters);
    }

    [Fact]
    public void Extract_IncludesNewlineAsAdditionalDelimiter_ForBracketedFormat()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[###]\n1###2\n3");

        Assert.Equal("1###2\n3", inputToParse);
        Assert.Contains("###", delimiters);
        Assert.Contains("\n", delimiters);
    }

    // Multiple bracketed delimiters tests
    [Fact]
    public void Extract_ReturnsMultipleDelimiters_WhenSeveralBracketsProvided()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[*][!!][r9r]\n11r9r22*hh*33!!44");

        Assert.Equal("11r9r22*hh*33!!44", inputToParse);
        Assert.Equal(["*", "!!", "r9r", "\n"], delimiters);
    }

    [Fact]
    public void Extract_HandlesVariableLengthDelimiters_InMultiBracketFormat()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[a][bb][ccc]\n1a2bb3ccc4");

        Assert.Equal("1a2bb3ccc4", inputToParse);
        Assert.Equal(["a", "bb", "ccc", "\n"], delimiters);
    }

    [Fact]
    public void Extract_HandlesTwoDelimiters()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[*][%]\n1*2%3");

        Assert.Equal("1*2%3", inputToParse);
        Assert.Equal(["*", "%", "\n"], delimiters);
    }

    // Edge cases and malformed input tests
    [Fact]
    public void Extract_FallsBackToDefaults_WhenBracketNotClosed()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[***\n1***2");

        Assert.Equal("//[***\n1***2", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    [Fact]
    public void Extract_FallsBackToDefaults_WhenEmptyBrackets()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[]\n1,2");

        Assert.Equal("//[]\n1,2", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    [Fact]
    public void Extract_FallsBackToDefaults_WhenNoNewlineAfterDelimiter()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[***]11***22");

        Assert.Equal("//[***]11***22", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    [Fact]
    public void Extract_HandlesEmptyInput()
    {
        var (inputToParse, delimiters) = _strategy.Extract("");

        Assert.Equal("", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    [Fact]
    public void Extract_HandlesNullInput()
    {
        var (inputToParse, delimiters) = _strategy.Extract(null!);

        Assert.Null(inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    [Fact]
    public void Extract_HandlesInputWithOnlyDelimiterDeclaration()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[***]\n");

        Assert.Equal("", inputToParse);
        Assert.Equal(["***", "\n"], delimiters);
    }

    [Fact]
    public void Extract_HandlesSingleCharDelimiterDeclarationOnly()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//#\n");

        Assert.Equal("", inputToParse);
        Assert.Equal(["#", "\n"], delimiters);
    }

    [Fact]
    public void Extract_FallsBackToDefaults_WhenOnlyDoubleSlashProvided()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//");

        Assert.Equal("//", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    [Fact]
    public void Extract_FallsBackToDefaults_WhenDoubleSlashWithoutNewline()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//#");

        Assert.Equal("//#", inputToParse);
        Assert.Equal([",", "\n"], delimiters);
    }

    [Fact]
    public void Extract_PrioritizesBracketedOverSingleChar_WhenBothCouldMatch()
    {
        var (inputToParse, delimiters) = _strategy.Extract("//[#]\n1#2");

        Assert.Equal("1#2", inputToParse);
        Assert.Equal(["#", "\n"], delimiters);
    }
}

