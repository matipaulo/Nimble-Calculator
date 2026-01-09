namespace Calculator.Parsers;

public static class InputParser
{
    private const char Delimiter = ',';

    public static IReadOnlyList<int> ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return [];

        var splitValues = input.Split(Delimiter).Select(x => x.Trim()).ToList();
        var result = new List<int>(splitValues.Count);
        foreach (var splitValue in splitValues)
            if (int.TryParse(splitValue, out var value))
                result.Add(value);

        if (result.Count == 1)
            result.Add(0);

        return result;
    }
}