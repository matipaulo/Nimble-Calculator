namespace Calculator.Parsers;

public class InputParser : IInputParser
{
    private const int MaxValueAllowed = 1000;

    private static readonly IDelimiterStrategy[] _strategies =
    [
        new MultiCharacterDelimiterStrategy(),
        new SingleCharacterDelimiterStrategy(),
        new DefaultDelimiterStrategy()
    ];

    public IReadOnlyList<int> ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return [];

        var normalizedInput = input.Replace("\\n", "\n");
        var strategy = _strategies.First(s => s.CanHandle(normalizedInput));
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