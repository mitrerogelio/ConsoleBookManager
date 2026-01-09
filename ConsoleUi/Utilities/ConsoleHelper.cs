namespace ConsoleBookManager.ConsoleUi.Utilities;

public static class ConsoleHelper
{
    public static int GetInt(string prompt, string errorMsg)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                return result;
            }

            DisplayError(errorMsg);
        }
    }

    public static int GetValidPageCount(string msg)
    {
        while (true)
        {
            Console.WriteLine(msg);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int result) && result > 0)
                return result;
            Console.WriteLine("Invalid page number, Please enter a positive number");
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