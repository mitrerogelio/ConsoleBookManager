namespace ConsoleBookManager.ConsoleUi.Utilities;

public static class ConsoleHelper
{
    public static int GetInt(string prompt, string errorMsg)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine()?.Trim();
            if (int.TryParse(input, out int result) && result >= 0)
            {
                return result;
            }

            DisplayError(errorMsg);
        }
    }

    public static string GetValidStr(string msg)
    {
        while (true)
        {
            Console.WriteLine(msg);
            string? input = Console.ReadLine()?.Trim();
            if (input is not null && input.Length > 0)
            {
                return input;
            }

            DisplayError("Invalid input.");
        }
    }

    public static DateTime GetDate(string prompt, string errorMsg)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"Setting the date to: {DateTime.Today.ToShortDateString()}");
                return DateTime.Today;
            }

            if (DateTime.TryParse(input, out DateTime result))
            {
                if (result >= DateTime.Today)
                {
                    return result;
                }

                DisplayError("Error: Date cannot be in the past.");
            }
            else
            {
                DisplayError(errorMsg);
            }
        }
    }

    private static void WriteFieldPrompt(string label, string current)
    {
        Console.Write(label);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"[{current}] ");
        Console.ResetColor();
    }

    private static string? ReadOptionalLine()
    {
        string? input = Console.ReadLine()?.Trim();
        return string.IsNullOrWhiteSpace(input) ? null : input;
    }

    public static string? GetOptionalStr(string label, string current)
    {
        WriteFieldPrompt(label, current);
        return ReadOptionalLine();
    }

    public static int? GetOptionalInt(string label, string current, string errorMsg)
    {
        while (true)
        {
            WriteFieldPrompt(label, current);
            string? input = ReadOptionalLine();

            if (input is null)
            {
                return null;
            }

            if (int.TryParse(input, out int result) && result >= 0)
            {
                return result;
            }

            DisplayError(errorMsg);
        }
    }

    public static DateTime? GetOptionalDate(string label, string current, string errorMsg)
    {
        while (true)
        {
            WriteFieldPrompt(label, current);
            string? input = ReadOptionalLine();

            if (input is null)
            {
                return null;
            }

            if (DateTime.TryParse(input, out DateTime result))
            {
                if (result >= DateTime.Today)
                {
                    return result;
                }

                DisplayError("Error: Date cannot be in the past.");
            }
            else
            {
                DisplayError(errorMsg);
            }
        }
    }

    public static void DisplaySuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nSUCCESS: {message}");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void DisplayError(string message, bool pause = false)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nERROR: {message}");
        Console.ResetColor();

        if (pause)
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}