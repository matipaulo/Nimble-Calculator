namespace Calculator.Parsers;

public class SingleCharacterDelimiterStrategy : IDelimiterStrategy
{
    private const int HeaderPrefixLength = 2; // Length of "//" prefix.

    public bool CanHandle(string input) => 
        input.StartsWith("//") && (input.Length <= HeaderPrefixLength || input[HeaderPrefixLength] != '[');

    public (string inputToParse, string[] delimiters) Extract(string input)
    {
        var newlineIndex = input.IndexOf('\n');
        if (newlineIndex <= HeaderPrefixLength)
        {
            return (input, DelimiterDefaults.DefaultDelimiters);
        }

        var delimiter = input[HeaderPrefixLength].ToString();
        var inputToParse = input.Substring(newlineIndex + 1);
        return (inputToParse, [delimiter, "\n"]);
    }
}
