namespace Calculator.Parsers;

public static class InputParser
{
    private const int MaxValueAllowed = 1000;
    private static readonly char[] DefaultDelimiters = [',', '\n'];

    public static IReadOnlyList<int> ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return [];

        var normalizedInput = input.Replace("\\n", "\n");

        // Check for custom delimiter format: //{delimiter}\n{numbers}
        var delimiters = DefaultDelimiters;
        if (normalizedInput.StartsWith("//") && normalizedInput.Length > 3)
        {
            var newlineIndex = normalizedInput.IndexOf('\n');
            if (newlineIndex > 2)
            {
                var customDelimiter = normalizedInput[2];
                delimiters = [customDelimiter, '\n'];
                normalizedInput = normalizedInput.Substring(newlineIndex + 1);
            }
        }

        var result = normalizedInput
            .Split(delimiters)
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