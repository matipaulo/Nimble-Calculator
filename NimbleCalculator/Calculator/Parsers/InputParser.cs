namespace Calculator.Parsers;

public class InputParser : IInputParser
{
    private const int MaxValueAllowed = 1000;

    private readonly IReadOnlyList<IDelimiterStrategy> _strategies;

    public InputParser(IEnumerable<IDelimiterStrategy> strategies)
    {
        // Materialize strategies so the order is stable when selecting a handler.
        _strategies = strategies.ToList();
    }

    public IReadOnlyList<int> ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return [];

        var normalizedInput = NormalizeInput(input);
        var strategy = _strategies.First(s => s.CanHandle(normalizedInput));
        var (inputToParse, delimiters) = strategy.Extract(normalizedInput);

        var result = inputToParse.Split(delimiters, StringSplitOptions.None)
            .Select(ParseValue)
            .Where(v => v.HasValue)
            .Select(v => v!.Value)
            .ToList();

        return result;
    }

    /// <summary>
    /// Normalizes the raw input so it can be parsed consistently.
    /// Currently replaces escaped newlines ("\\n") with real newlines.
    /// </summary>
    private static string NormalizeInput(string input) => input.Replace("\\n", "\n");

    private static int? ParseValue(string value)
    {
        if (!int.TryParse(value.Trim(), out var result))
            return null;

        return result > MaxValueAllowed ? null : result;
    }
}