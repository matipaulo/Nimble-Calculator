namespace Calculator.Parsers;

public class SingleCharacterDelimiterStrategy : IDelimiterStrategy
{
    public bool CanHandle(string input) => 
        input.StartsWith("//") && (input.Length <= 2 || input[2] != '[');

    public (string inputToParse, string[] delimiters) Extract(string input)
    {
        var newlineIndex = input.IndexOf('\n');
        if (newlineIndex <= 2)
        {
             return (input, [",", "\n"]);
        }

        var delimiter = input[2].ToString();
        var inputToParse = input.Substring(newlineIndex + 1);
        return (inputToParse, [delimiter, "\n"]);
    }
}
