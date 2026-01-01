namespace ConsoleBookManager.Services;

public static class InputService
{
    static int CheckStringInput(string prompt)
    {
        string input = string.Empty;
        bool isValidInput = false;
        while (!isValidInput)
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"Please enter a valid string. Please try again:\n {prompt}");
            }
            else
            {
                isValidInput = true;
            }
        }

        return int.Parse(input);
    }
}