namespace Calculator.Operations;

using System.Linq.Expressions;
using Calculator.Exceptions;

public sealed class OperationExecutor
{
    private readonly IOperation _operation;

    public OperationExecutor(IOperation operation)
    {
        _operation = operation;
    }

    public int ExecuteOnCollection(IReadOnlyList<int> numbers)
    {
        if (numbers.Count == 0)
            throw new InvalidOperationException("No numbers to process.");

        if (numbers.Count == 1)
            return numbers[0];

        var negativeNumbers = numbers.Where(n => n < 0).ToList();
        if (negativeNumbers.Count > 0)
            throw new NegativeNumbersException(negativeNumbers);

        var resultExpression = numbers
            .Select((_, i) => Expression.Constant(numbers[i]))
            .Aggregate((Expression?)null, (current, numberExpression) =>
                current == null
                    ? numberExpression
                    : Expression.Call(Expression.Constant(_operation),
                        typeof(IOperation).GetMethod("Execute")!,
                        current, numberExpression));

        return Expression.Lambda<Func<int>>(resultExpression!).Compile()();
    }
}