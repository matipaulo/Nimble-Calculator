namespace Calculator.Tests.Parsers;

using Calculator.Parsers;

public class InputParserTests
{
    [Fact]
    public void ParseInput_ReturnsEmptyList_WhenInputIsNullOrWhitespace()
    {
        var result = InputParser.ParseInput("   ");
        Assert.Empty(result);
    }

    [Fact]
    public void ParseInput_IgnoresInvalidNumbers_WhenInputContainsNonNumericValues()
    {
        var result = InputParser.ParseInput("1, abc, 3");
        Assert.Equal([1, 3], result);
    }

    [Fact]
    public void ParseInput_AddsZero_WhenInputContainsSingleNumber()
    {
        var result = InputParser.ParseInput("5");
        Assert.Equal([5], result);
    }

    [Fact]
    public void ParseInput_AddsNumbers_WhenInputContainsMultipleNumbers()
    {
        var result = InputParser.ParseInput("1, 2, 3");
        Assert.Equal([1, 2, 3], result);
    }

    [Fact]
    public void ParseInput_ReturnsEmptyList_WhenInputContainsOnlyCommas()
    {
        var result = InputParser.ParseInput(",");
        Assert.Empty(result);
    }

    [Fact]
    public void ParseInput_ReturnsValidValues_WhenInputContainsNewLine()
    {
        var result = InputParser.ParseInput("1\\n2,3");
        Assert.Equal([1, 2, 3], result);
    }

    [Fact]
    public void ParseInput_ReturnsValidValues_WhenInputContainsValueGreaterThan1000()
    {
        var result = InputParser.ParseInput("1\\n2,3000");
        Assert.Equal([1, 2], result);
    }
}