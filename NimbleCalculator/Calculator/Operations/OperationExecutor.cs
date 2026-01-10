namespace Calculator.Operations;

using Calculator.Exceptions;

public sealed class OperationExecutor : IOperationExecutor
{
    private readonly IOperation _operation;

    public OperationExecutor(IOperation operation)
    {
        _operation = operation;
    }

    public int ExecuteOnCollection(IReadOnlyList<int> numbers)
    {
        ValidateNumbers(numbers);

        if (numbers.Count == 1)
            return numbers[0];

        // Apply the binary operation sequentially over the collection.
        var result = numbers[0];
        for (var i = 1; i < numbers.Count; i++)
        {
            result = _operation.Execute(result, numbers[i]);
        }

        return result;
    }

    /// <summary>
    /// Validates the input collection and throws when it is empty or contains negative numbers.
    /// </summary>
    private static void ValidateNumbers(IReadOnlyList<int> numbers)
    {
        if (numbers.Count == 0)
            throw new InvalidOperationException("No numbers to process.");

        var negativeNumbers = numbers.Where(n => n < 0).ToList();
        if (negativeNumbers.Count > 0)
            throw new NegativeNumbersException(negativeNumbers);
    }
}