using System.Globalization;

namespace ConsoleBookManager.ConsoleUi.Utilities;

public static class ConsoleHelper
{
    public static int GetInt(string prompt, string errorMsg)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine()?.Trim();
            if (int.TryParse(input, out int result) && result > 0)
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
            if (input is not null || input.Length > 0)
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

    public static void DisplaySuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nSUCCESS: {message}");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void DisplayError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nERROR: {message}");
        Console.ResetColor();
    }
}