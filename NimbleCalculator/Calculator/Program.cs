using Calculator.Operations;
using Calculator.Parsers;

Console.Clear();

while (true)
{
    Console.Write(">> ");
    var input = Console.ReadLine();

    if (input is null || string.IsNullOrWhiteSpace(input))
        continue;

    try
    {
        var numbers = InputParser.ParseInput(input);

        var operation = new SumOperation();
        var executor = new OperationExecutor(operation);
        var result = executor.ExecuteOnCollection(numbers);

        Console.WriteLine(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}