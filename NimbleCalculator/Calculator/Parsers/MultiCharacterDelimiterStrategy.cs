namespace Calculator.Parsers;

public class MultiCharacterDelimiterStrategy : IDelimiterStrategy
{
    private static readonly string[] DefaultDelimiters = [",", "\n"];

    public bool CanHandle(string input)
    {
        return input.StartsWith("//[");
    }

    public (string inputToParse, string[] delimiters) Extract(string input)
    {
        var newlineIndex = input.IndexOf('\n');
        if (newlineIndex < 0)
            return (input, DefaultDelimiters);

        var delimiterSection = input.Substring(2, newlineIndex - 2);
        var delimiters = ParseDelimiters(delimiterSection);

        if (delimiters.Count == 0)
            return (input, DefaultDelimiters);

        delimiters.Add("\n");
        var inputToParse = input.Substring(newlineIndex + 1);
        return (inputToParse, delimiters.ToArray());
    }

    private static List<string> ParseDelimiters(string section)
    {
        var delimiters = new List<string>();
        var index = 0;

        while (index < section.Length)
        {
            if (section[index] != '[')
                return new List<string>();

            var closingBracketIndex = section.IndexOf(']', index + 1);
            if (closingBracketIndex < 0)
                return new List<string>();

            var delimiter = section.Substring(index + 1, closingBracketIndex - index - 1);
            if (delimiter.Length == 0)
                return new List<string>();

            delimiters.Add(delimiter);
            index = closingBracketIndex + 1;
        }

        return delimiters;
    }
}