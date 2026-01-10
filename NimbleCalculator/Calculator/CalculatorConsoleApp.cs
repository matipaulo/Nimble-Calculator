using Calculator.Exceptions;
using Calculator.Operations;
using Calculator.Parsers;

namespace Calculator;

/// <summary>
/// Handles the console-based interaction loop for the calculator application.
/// </summary>
public sealed class CalculatorConsoleApp
{
    private readonly IInputParser _parser;
    private readonly IOperationExecutor _executor;

    public CalculatorConsoleApp(IInputParser parser, IOperationExecutor executor)
    {
        _parser = parser;
        _executor = executor;
    }

    /// <summary>
    /// Runs the main console loop, reading user input and writing results.
    /// </summary>
    public void Run()
    {
        Console.Clear();
        WritePrompt();

        while (true)
        {
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                WritePrompt();
                continue;
            }

            try
            {
                var numbers = _parser.ParseInput(input);
                var result = _executor.ExecuteOnCollection(numbers);

                Console.WriteLine(result);
            }
            catch (NegativeNumbersException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            WritePrompt();
        }
    }

    private static void WritePrompt()
    {
        // Keep the prompt format centralized for consistency.
        Console.Write(">> ");
    }
}
