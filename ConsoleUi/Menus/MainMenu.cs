namespace ConsoleBookManager.ConsoleUi.Menus;

public static class MainMenu
{
    public static void Show()
    {
        Console.Clear();
        Console.WriteLine("=== Book Manager ===");
        Console.WriteLine("1. Add book");
        Console.WriteLine("2. List books");
        Console.WriteLine("3. Update a book");
        Console.WriteLine("4. Delete a book");
        Console.WriteLine("0. Exit");
        Console.WriteLine();
        Console.Write("Select an option: ");
    }

    public static int GetChoice()
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
                return choice;

            Console.Write("Invalid input. Please enter a valid option from above: ");
        }
    }
}