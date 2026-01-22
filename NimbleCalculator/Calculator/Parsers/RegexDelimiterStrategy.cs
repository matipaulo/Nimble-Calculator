using System.Text.RegularExpressions;

namespace Calculator.Parsers;

/// <summary>
/// Unified delimiter strategy that uses regex patterns to detect and extract
/// all delimiter formats: default (comma/newline), single character, and bracketed.
/// </summary>
public class RegexDelimiterStrategy : IDelimiterStrategy
{
    // Matches multiple bracketed delimiters: //[delim1][delim2]...\n
    private static readonly Regex MultipleBracketedRegex = new(@"^//(\[.+?\])+\n", RegexOptions.Compiled);
    
    // Extracts individual delimiters from brackets: [delimiter]
    private static readonly Regex BracketContentRegex = new(@"\[(.+?)\]", RegexOptions.Compiled);
    
    // Matches single character delimiter: //c\n where c is any single character except '['
    private static readonly Regex SingleCharRegex = new(@"^//(.)\n", RegexOptions.Compiled);

    public bool CanHandle(string input)
    {
        // This unified strategy can handle all input formats
        return true;
    }

    public (string inputToParse, string[] delimiters) Extract(string input)
    {
        if (string.IsNullOrEmpty(input))
            return (input, DelimiterDefaults.DefaultDelimiters);

        // Priority 1: Check for multiple bracketed delimiters format
        var multiMatch = MultipleBracketedRegex.Match(input);
        if (multiMatch.Success)
        {
            return ExtractBracketedDelimiters(input, multiMatch);
        }

        // Priority 2: Check for single character delimiter format
        // But only if it's not a bracket (to avoid conflict with bracketed format)
        if (input.StartsWith("//") && input.Length > 2 && input[2] != '[')
        {
            var singleMatch = SingleCharRegex.Match(input);
            if (singleMatch.Success)
            {
                return ExtractSingleCharDelimiter(input, singleMatch);
            }
        }

        // Priority 3: Default format (no custom delimiter declaration)
        return (input, DelimiterDefaults.DefaultDelimiters);
    }

    private static (string inputToParse, string[] delimiters) ExtractBracketedDelimiters(
        string input, Match match)
    {
        var delimiterSection = match.Value;
        var delimiters = new List<string>();

        // Extract all delimiters within brackets
        var bracketMatches = BracketContentRegex.Matches(delimiterSection);
        foreach (Match bracketMatch in bracketMatches)
        {
            var delimiter = bracketMatch.Groups[1].Value;
            if (string.IsNullOrEmpty(delimiter))
            {
                // Empty brackets found - fallback to defaults
                return (input, DelimiterDefaults.DefaultDelimiters);
            }
            delimiters.Add(delimiter);
        }

        if (delimiters.Count == 0)
        {
            // No valid delimiters found - fallback to defaults
            return (input, DelimiterDefaults.DefaultDelimiters);
        }

        // Always include newline as an additional delimiter
        delimiters.Add("\n");

        var inputToParse = input.Substring(match.Length);
        return (inputToParse, delimiters.ToArray());
    }

    private static (string inputToParse, string[] delimiters) ExtractSingleCharDelimiter(
        string input, Match match)
    {
        var delimiter = match.Groups[1].Value;
        var inputToParse = input.Substring(match.Length);
        
        // Include newline as an additional delimiter
        return (inputToParse, new[] { delimiter, "\n" });
    }
}

