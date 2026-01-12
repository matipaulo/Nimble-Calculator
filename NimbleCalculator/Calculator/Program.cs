﻿using Calculator;
using Calculator.Operations;
using Calculator.Parsers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Configure and build the generic host with all required services.
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // Parsing
        services.AddSingleton<IDelimiterStrategy, RegexDelimiterStrategy>();
        services.AddSingleton<IInputParser, InputParser>();

        // Operations
        services.AddSingleton<IOperation, SumOperation>();
        services.AddSingleton<IOperationExecutor, OperationExecutor>();

        // Application
        services.AddSingleton<CalculatorConsoleApp>();
    })
    .Build();

// Ensure the application terminates immediately when Ctrl+C is pressed.
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    Environment.Exit(0);
};

// Resolve and run the main console application loop.
var app = host.Services.GetRequiredService<CalculatorConsoleApp>();
app.Run();
