using ConsoleBookManager.ConsoleUi.Utilities;
using ConsoleBookManager.Models;

namespace ConsoleBookManager.ConsoleUi.Menus;

public static class BookMenu
{
    public static (string title, string author, int totalPages) GetNewBookDetails()
    {
        Console.Clear();
        Console.WriteLine("=== Add a New Book ===");

        string title = ConsoleHelper.GetValidStr("Enter the book title: ");
        string author = ConsoleHelper.GetValidStr("Enter the book author: ");
        int pages = ConsoleHelper.GetValidPageCount("Enter the total amount of pages in this book: ");
        return (title, author, pages);
    }

    public static int SelectPropertyToUpdate()
    {
        Console.WriteLine("\n----------------------------------------");
        Console.WriteLine(" SELECT PROPERTY TO UPDATE");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(" 1. Title");
        Console.WriteLine(" 2. Author");
        Console.WriteLine(" 3. Total Pages");
        Console.WriteLine(" 4. Current Page");
        Console.WriteLine(" 5. Total Chapters");
        Console.WriteLine(" 6. Current Chapter");
        Console.WriteLine(" 7. Due Date");
        Console.WriteLine(" 0. Cancel / Go Back");
        Console.WriteLine("----------------------------------------");

        while (true)
        {
            int input = ConsoleHelper.GetInt("Enter a choice: ", "Invalid input. Please try again.");

            if (input is >= 0 and <= 7)
            {
                return input;
            }

            ConsoleHelper.DisplayError("Invalid selection. Please choose 0-7.");
        }
    }
}