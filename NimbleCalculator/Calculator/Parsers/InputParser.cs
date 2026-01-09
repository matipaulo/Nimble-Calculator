namespace Calculator.Parsers;

public static class InputParser
{
    private const int MaxValueAllowed = 1000;
    private static readonly IDelimiterStrategy[] Strategies = 
    [
        new MultiCharacterDelimiterStrategy(),
        new SingleCharacterDelimiterStrategy(),
        new DefaultDelimiterStrategy()
    ];

    public static IReadOnlyList<int> ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return [];

        var normalizedInput = input.Replace("\\n", "\n");
        var strategy = Strategies.First(s => s.CanHandle(normalizedInput));
        var (inputToParse, delimiters) = strategy.Extract(normalizedInput);

        var result = inputToParse.Split(delimiters, StringSplitOptions.None)
            .Select(ParseValue)
            .Where(v => v.HasValue)
            .Select(v => v!.Value)
            .ToList();

        return result;
    }

    private static int? ParseValue(string value)
    {
        if (!int.TryParse(value.Trim(), out var result))
            return null;

        return result > MaxValueAllowed ? null : result;
    }
}