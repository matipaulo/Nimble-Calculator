namespace Calculator.Exceptions;

public sealed class MaximumArgumentsReachedException : Exception
{
    public MaximumArgumentsReachedException(int maxArguments) : base(
        $"Maximum number of arguments ({maxArguments}) reached.")
    {
    }
}