namespace Calculator.Parsers;

public static class InputParser
{
    private const int MaxValueAllowed = 1000;
    private static readonly char[] Delimiters = [',', '\n'];

    public static IReadOnlyList<int> ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return [];

        var result = input.Replace("\\n", "\n")
            .Split(Delimiters)
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