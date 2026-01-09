namespace Calculator.Tests.Operations;

using Calculator.Exceptions;
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
    public void ExecuteOnCollection_ThrowsException_WhenNegativeNumbersProvided()
    {
        var operation = new SumOperation();
        var executor = new OperationExecutor(operation);

        var exception = Assert.Throws<NegativeNumbersException>(() =>
            executor.ExecuteOnCollection(new List<int> { -1, -2, -3 }));

        Assert.Contains("-1", exception.Message);
        Assert.Contains("-2", exception.Message);
        Assert.Contains("-3", exception.Message);
    }

    [Fact]
    public void ExecuteOnCollection_ThrowsException_WhenMixedWithNegativeNumbers()
    {
        var operation = new SumOperation();
        var executor = new OperationExecutor(operation);

        var exception = Assert.Throws<NegativeNumbersException>(() =>
            executor.ExecuteOnCollection(new List<int> { -1, 2, -3, 4 }));

        Assert.Contains("-1", exception.Message);
        Assert.Contains("-3", exception.Message);
        Assert.DoesNotContain("2", exception.Message);
        Assert.DoesNotContain("4", exception.Message);
    }
}