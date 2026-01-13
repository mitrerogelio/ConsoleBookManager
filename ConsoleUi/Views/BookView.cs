using ConsoleBookManager.Models;

namespace ConsoleBookManager.ConsoleUi.Views;

public static class BookView
{
    public static void ShowBooks(IEnumerable<Book> books)
    {
        Console.Clear();
        Console.WriteLine("=== Your Library ===\n");

        IEnumerable<Book> bookList = books.ToList();
        if (!bookList.Any())
        {
            Console.WriteLine("No books found in library.");
        }
        else
        {
            foreach (Book book in bookList)
            {
                PrintBookCard(book);
                Console.WriteLine();
            }
        }

        Console.WriteLine("Press any key to return to menu...");
        Console.ReadKey();
    }

    private static void PrintBookCard(Book book)
    {
        Console.WriteLine("========================================");
        Console.Write(" [");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"#{book.Id}");
        Console.ResetColor();
        Console.WriteLine($"] {book.Title.ToUpper()}");
        Console.WriteLine("========================================");

        Console.WriteLine($" Author:        {book.Author}");
        Console.WriteLine($" Status:        {book.Status}");

        Console.WriteLine($" Pages:         {book.CurrentPage ?? 0} / {book.TotalPages}");

        if (book.TotalChapters.HasValue)
        {
            Console.WriteLine($" Chapters:      {book.CurrentChapter ?? 0} / {book.TotalChapters}");
        }

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

        int percent = book.Progress ?? 0;
        DrawProgressBar(percent);

        Console.WriteLine("========================================");
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

    public static void DisplayReadingGoal(Book book, int pagesPerDay, DateTime targetDate)
    {
        Console.Clear();
        PrintBookCard(book);

        Console.WriteLine("             READING PLAN");
        Console.WriteLine("----------------------------------------");
        Console.ResetColor();

        int pagesLeft = book.TotalPages - (book.CurrentPage ?? 0);
        int daysLeft = (targetDate.Date - DateTime.Today).Days;

        Console.WriteLine($" Target Date:    {targetDate.ToShortDateString()}");
        Console.WriteLine($" Pages Left:     {pagesLeft}");
        Console.WriteLine($" Days to Goal:   {Math.Max(0, daysLeft)} days");

        Console.WriteLine("----------------------------------------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($" GOAL: Read {pagesPerDay} pages per day");
        Console.ResetColor();
        Console.WriteLine("========================================");

        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }
}