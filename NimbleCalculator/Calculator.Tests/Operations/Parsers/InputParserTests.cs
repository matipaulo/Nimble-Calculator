namespace Calculator.Tests.Operations.Parsers;

using Calculator.Exceptions;
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
    public void ParseInput_ThrowsException_WhenInputExceedsMaxArguments()
    {
        Assert.Throws<MaximumArgumentsReachedException>(() => InputParser.ParseInput("1, 2, 3"));
    }

    [Fact]
    public void ParseInput_AddsZero_WhenInputContainsSingleNumber()
    {
        var result = InputParser.ParseInput("5");
        Assert.Equal([5, 0], result);
    }
}