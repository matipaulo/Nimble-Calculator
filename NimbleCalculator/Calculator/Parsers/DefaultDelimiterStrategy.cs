namespace Calculator.Parsers;

public class DefaultDelimiterStrategy : IDelimiterStrategy
{
    private static readonly string[] DefaultDelimiters = [",", "\n"];

    public bool CanHandle(string input) => !input.StartsWith("//");

    public (string inputToParse, string[] delimiters) Extract(string input)
    {
        return (input, DefaultDelimiters);
    }
}
