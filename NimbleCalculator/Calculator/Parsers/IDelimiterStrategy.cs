namespace Calculator.Parsers;

public interface IDelimiterStrategy
{
    bool CanHandle(string input);
    (string inputToParse, string[] delimiters) Extract(string input);
}
