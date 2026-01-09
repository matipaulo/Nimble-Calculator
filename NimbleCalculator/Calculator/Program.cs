using Calculator.Operations;
using Calculator.Parsers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IInputParser, InputParser>();
        services.AddSingleton<IOperation, SumOperation>();
        services.AddSingleton<IOperationExecutor, OperationExecutor>();
    })
    .Build();

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    if (!cts.IsCancellationRequested)
        cts.Cancel();
};

var parser = host.Services.GetRequiredService<IInputParser>();
var executor = host.Services.GetRequiredService<IOperationExecutor>();
var token = cts.Token;

Console.Clear();

while (!token.IsCancellationRequested)
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

Console.WriteLine("Application terminated. Press Enter to exit.");
Console.ReadLine();
