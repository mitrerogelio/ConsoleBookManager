using ConsoleBookManager.ConsoleUi.Menus;
using ConsoleBookManager.Models;

namespace ConsoleBookManager.ConsoleUi.Views;

public static class BookView
{
    public static void ShowBooks(IEnumerable<Book> books)
    {
        Console.Clear();
        Console.WriteLine("=== Your Library ===\n");
        Console.WriteLine("{0,-5} {1,-30} {2,-20} {3,-15}", "ID", "Title", "Author", "Status");
        Console.WriteLine(new string('-', 75));

        foreach (Book book in books)
        {
            string title = (book.Title.Length > 27) ? book.Title[..27] + "..." : book.Title;
            string author = book.Author;
            string status = book.Status.ToString();
            Console.WriteLine("{0,-5} {1,-30} {2,-20} {3,-15}",
                book.Id, title, author, status);
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }

    public static void DisplayBookDetails(Book book)
    {
        Console.WriteLine("========================================");
        Console.WriteLine($" {book.Title.ToUpper()}");
        Console.WriteLine("========================================");

        // Basic Details
        Console.WriteLine($" Author:        {book.Author}");
        Console.WriteLine($" Status:        {book.Status}");

        // Page Progress
        Console.WriteLine($" Pages:         {book.CurrentPage ?? 0} / {book.TotalPages}");

        // Chapter Progress - if data is available
        if (book.TotalChapters.HasValue)
        {
            Console.WriteLine($" Chapters:      {book.CurrentChapter ?? 0} / {book.TotalChapters}");
        }

        // Due Date - if data is available
        if (book.DueDate.HasValue)
        {
            int daysLeft = (int)(book.DueDate.Value - DateTime.Today).TotalDays;

            string dueContext = daysLeft switch
            {
                < 0 => $"({Math.Abs(daysLeft)} days overdue)",
                0 => "(Due today)",
                _ => $"({daysLeft} days left)"
            };

            Console.WriteLine($" Due Date:      {book.DueDate.Value.ToShortDateString()} {dueContext}");
        }

        // Visual Progress Bar
        int percent = book.Progress ?? 0;
        DrawProgressBar(percent);

        Console.WriteLine("========================================");
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void DrawProgressBar(int percent)
    {
        const int totalBlocks = 20; // Length of the bar
        int filledBlocks = (int)Math.Round((double)percent / 100 * totalBlocks);

        Console.Write(" Progress:      [");
        Console.Write(new string('█', filledBlocks)); // Filled part
        Console.Write(new string('-', totalBlocks - filledBlocks)); // Empty part
        Console.WriteLine($"] {percent}%");
    }

    public static void ShowPropertiesToUpdate()
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
    }
}