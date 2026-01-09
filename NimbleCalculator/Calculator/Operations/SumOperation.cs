namespace Calculator.Operations;

public sealed class SumOperation : IOperation
{
    public int Execute(int a, int b)
    {
        return a + b;
    }
}