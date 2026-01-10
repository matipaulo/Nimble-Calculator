namespace Calculator.Parsers;

public class DefaultDelimiterStrategy : IDelimiterStrategy
{
    public bool CanHandle(string input) => !input.StartsWith("//");

    public (string inputToParse, string[] delimiters) Extract(string input)
    {
        return (input, DelimiterDefaults.DefaultDelimiters);
    }
}
