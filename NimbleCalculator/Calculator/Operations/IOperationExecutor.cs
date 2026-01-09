namespace Calculator.Operations;

public interface IOperationExecutor
{
    int ExecuteOnCollection(IReadOnlyList<int> numbers);
}