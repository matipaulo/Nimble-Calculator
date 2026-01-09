namespace Calculator.Exceptions;

public sealed class NegativeNumbersException : Exception
{
    public NegativeNumbersException(IEnumerable<int> negativeNumbers) : base(
        $"Negative numbers: {string.Join(", ", negativeNumbers)}")
    {
    }
}