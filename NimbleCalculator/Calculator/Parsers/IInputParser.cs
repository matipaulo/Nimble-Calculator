namespace Calculator.Parsers;

public interface IInputParser
{
    IReadOnlyList<int> ParseInput(string input);
}