﻿namespace Calculator.Tests.Parsers;

using Calculator.Parsers;

public class InputParserTests
{
    private readonly InputParser _inputParser = new(
        new IDelimiterStrategy[]
        {
            new RegexDelimiterStrategy()
        });

    [Fact]
    public void ParseInput_ReturnsEmptyList_WhenInputIsNullOrWhitespace()
    {
        var result = _inputParser.ParseInput("   ");
        Assert.Empty(result);
    }

    [Fact]
    public void ParseInput_IgnoresInvalidNumbers_WhenInputContainsNonNumericValues()
    {
        var result = _inputParser.ParseInput("1, abc, 3");
        Assert.Equal([1, 3], result);
    }

    [Fact]
    public void ParseInput_ReturnsSingleValue_WhenInputContainsSingleNumber()
    {
        var result = _inputParser.ParseInput("5");
        Assert.Equal([5], result);
    }

    [Fact]
    public void ParseInput_ReturnsValidValues_WhenInputContainsCustomDelimiter()
    {
        var result = _inputParser.ParseInput("//#\\n2#5");
        Assert.Equal([2, 5], result);
    }

    [Fact]
    public void ParseInput_ReturnsValidValues_WhenInputContainsCustomDelimiterWithInvalidNumbers()
    {
        var result = _inputParser.ParseInput("//,\\n2,ff,100");
        Assert.Equal([2, 100], result);
    }

    [Fact]
    public void ParseInput_SupportsDefaultDelimiters_WhenNoCustomDelimiterProvided()
    {
        var result = _inputParser.ParseInput("1,2\\n3");
        Assert.Equal([1, 2, 3], result);
    }

    [Fact]
    public void ParseInput_ReturnsValidValues_WhenInputContainsCustomDelimiterAndNewlines()
    {
        var result = _inputParser.ParseInput("//#\\n2#5\\n3");
        Assert.Equal([2, 5, 3], result);
    }

    [Fact]
    public void ParseInput_ReturnsValidValues_WhenInputContainsMultiCharacterCustomDelimiter()
    {
        var result = _inputParser.ParseInput("//[***]\\n11***22***33");
        Assert.Equal([11, 22, 33], result);
    }

    [Fact]
    public void ParseInput_ReturnsValidValues_WhenInputContainsMultiCharacterDelimiterWithInvalidNumbers()
    {
        var result = _inputParser.ParseInput("//[###]\\n1###abc###100");
        Assert.Equal([1, 100], result);
    }

    [Fact]
    public void ParseInput_SupportsMultiCharacterDelimiterWithNewlines()
    {
        var result = _inputParser.ParseInput("//[***]\\n11***22\\n33");
        Assert.Equal([11, 22, 33], result);
    }

    [Fact]
    public void ParseInput_SupportsSingleCharacterDelimiterFormat()
    {
        var result = _inputParser.ParseInput("//#\\n2#5");
        Assert.Equal([2, 5], result);
    }

    [Fact]
    public void ParseInput_AddsZero_WhenInputContainsSingleNumber()
    {
        var result = _inputParser.ParseInput("5");
        Assert.Equal([5], result);
    }

    [Fact]
    public void ParseInput_AddsNumbers_WhenInputContainsMultipleNumbers()
    {
        var result = _inputParser.ParseInput("1, 2, 3");
        Assert.Equal([1, 2, 3], result);
    }

    [Fact]
    public void ParseInput_ReturnsEmptyList_WhenInputContainsOnlyCommas()
    {
        var result = _inputParser.ParseInput(",");
        Assert.Empty(result);
    }

    [Fact]
    public void ParseInput_ReturnsValidValues_WhenInputContainsNewLine()
    {
        var result = _inputParser.ParseInput("1\\n2,3");
        Assert.Equal([1, 2, 3], result);
    }

    [Fact]
    public void ParseInput_ReturnsValidValues_WhenInputContainsValueGreaterThan1000()
    {
        var result = _inputParser.ParseInput("1\\n2,3000");
        Assert.Equal([1, 2], result);
    }
}