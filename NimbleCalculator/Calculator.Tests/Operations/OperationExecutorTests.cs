namespace Calculator.Tests.Operations;

using Calculator.Operations;

public class OperationExecutorTests
{
    [Fact]
    public void ExecuteOnCollection_ReturnsSumOfAllNumbers()
    {
        var operation = new SumOperation();
        var executor = new OperationExecutor(operation);

        var result = executor.ExecuteOnCollection(new List<int> { 1, 2, 3 });

        Assert.Equal(6, result);
    }

    [Fact]
    public void ExecuteOnCollection_ThrowsException_WhenCollectionIsEmpty()
    {
        var operation = new SumOperation();
        var executor = new OperationExecutor(operation);

        Assert.Throws<InvalidOperationException>(() => executor.ExecuteOnCollection(new List<int>()));
    }

    [Fact]
    public void ExecuteOnCollection_ReturnsSingleNumber_WhenCollectionHasOneElement()
    {
        var operation = new SumOperation();
        var executor = new OperationExecutor(operation);

        var result = executor.ExecuteOnCollection(new List<int> { 5 });

        Assert.Equal(5, result);
    }

    [Fact]
    public void ExecuteOnCollection_HandlesNegativeNumbersCorrectly()
    {
        var operation = new SumOperation();
        var executor = new OperationExecutor(operation);

        var result = executor.ExecuteOnCollection(new List<int> { -1, -2, -3 });

        Assert.Equal(-6, result);
    }

    [Fact]
    public void ExecuteOnCollection_HandlesMixedPositiveAndNegativeNumbers()
    {
        var operation = new SumOperation();
        var executor = new OperationExecutor(operation);

        var result = executor.ExecuteOnCollection(new List<int> { -1, 2, -3, 4 });

        Assert.Equal(2, result);
    }
}