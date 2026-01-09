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
        Console.WriteLine(operation.Execute(numbers[0], numbers[1]));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}