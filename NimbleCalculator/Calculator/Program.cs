using Calculator.Operations;
using Calculator.Parsers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IInputParser, InputParser>();
        services.AddSingleton<IOperation, SumOperation>();
        services.AddSingleton<IOperationExecutor, OperationExecutor>();
    })
    .Build();

var parser = host.Services.GetRequiredService<IInputParser>();
var executor = host.Services.GetRequiredService<IOperationExecutor>();

Console.Clear();

while (true)
{
    Console.Write(">> ");
    var input = Console.ReadLine();

    if (input is null || string.IsNullOrWhiteSpace(input))
        continue;

    try
    {
        var numbers = parser.ParseInput(input);
        var result = executor.ExecuteOnCollection(numbers);

        Console.WriteLine(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}