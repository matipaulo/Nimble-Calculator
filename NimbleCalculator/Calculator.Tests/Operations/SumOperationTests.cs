namespace Calculator.Tests.Operations;

using Calculator.Operations;

public class SumOperationTests
{
    [Fact]
    public void Execute_ReturnsSumOfTwoPositiveNumbers()
    {
        // Arrange
        var operation = new SumOperation();

        // Act
        var result = operation.Execute(3, 5);

        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void Execute_ReturnsSumWhenOneNumberIsNegative()
    {
        // Arrange
        var operation = new SumOperation();

        // Act
        var result = operation.Execute(-3, 5);

        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void Execute_ReturnsSumWhenBothNumbersAreNegative()
    {
        // Arrange
        var operation = new SumOperation();

        // Act
        var result = operation.Execute(-3, -5);

        // Assert
        Assert.Equal(-8, result);
    }

    [Fact]
    public void Execute_ReturnsSumWhenOneNumberIsZero()
    {
        // Arrange
        var operation = new SumOperation();

        // Act
        var result = operation.Execute(0, 5);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void Execute_ReturnsZeroWhenBothNumbersAreZero()
    {
        // Arrange
        var operation = new SumOperation();

        // Act
        var result = operation.Execute(0, 0);

        // Assert
        Assert.Equal(0, result);
    }
}