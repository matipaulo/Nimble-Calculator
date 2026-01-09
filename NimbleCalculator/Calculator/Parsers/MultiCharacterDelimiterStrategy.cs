namespace Calculator.Parsers;

public class MultiCharacterDelimiterStrategy : IDelimiterStrategy
{
    public bool CanHandle(string input) => 
        input.StartsWith("//[");

    public (string inputToParse, string[] delimiters) Extract(string input)
    {
        var newlineIndex = input.IndexOf('\n');
        var closingBracketIndex = input.IndexOf(']');

        if (closingBracketIndex > 3 && closingBracketIndex < newlineIndex)
        {
            var delimiter = input.Substring(3, closingBracketIndex - 3);
            var inputToParse = input.Substring(newlineIndex + 1);
            return (inputToParse, [delimiter, "\n"]);
        }

        return (input, [",", "\n"]);
    }
}
