using ConsoleBookManager.ConsoleUi.Utilities;
using ConsoleBookManager.ConsoleUi.Views;
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
        int pages = ConsoleHelper.GetInt("Enter the total amount of pages in this book: ",
            "Invalid. Please try again.");
        return (title, author, pages);
    }

    public static BookUpdateOptions SelectPropertyToUpdate()
    {
        BookView.ShowPropertiesToUpdate();
        while (true)
        {
            int input = ConsoleHelper.GetInt("Enter a choice: ", "Invalid input. Please try again.");
            try
            {
                return input switch
                {
                    0 => BookUpdateOptions.All,
                    1 => BookUpdateOptions.Title,
                    2 => BookUpdateOptions.Author,
                    3 => BookUpdateOptions.TotalPages,
                    4 => BookUpdateOptions.CurrentPage,
                    5 => BookUpdateOptions.TotalChapters,
                    6 => BookUpdateOptions.CurrentChapter,
                    7 => BookUpdateOptions.DueDate,
                    8 => BookUpdateOptions.Cancel,
                    _ => throw new InvalidOperationException("Unreachable")
                };
            }
            catch (Exception e)
            {
                ConsoleHelper.DisplayError($"Error updating the book: {e.Message}");
            }
        }
    }

    public static (string? title, string? author, int? totalPages, int? currentPage,
        int? totalChapters, int? currentChapter, DateTime? dueDate) GetUpdatedBookDetails(Book book)
    {
        Console.Clear();
        Console.WriteLine("=== Update Book (leave blank to keep current value) ===");

        string? title = ConsoleHelper.GetOptionalStr("New title ", book.Title);
        string? author = ConsoleHelper.GetOptionalStr("New author ", book.Author);
        int? totalPages = ConsoleHelper.GetOptionalInt("New total pages ",
            book.TotalPages.ToString(), "Invalid. Please try again.");
        int? currentPage = ConsoleHelper.GetOptionalInt("New current page ",
            book.CurrentPage?.ToString() ?? "none", "Invalid. Please try again.");
        int? totalChapters = ConsoleHelper.GetOptionalInt("New total chapters ",
            book.TotalChapters?.ToString() ?? "none", "Invalid. Please try again.");
        int? currentChapter = ConsoleHelper.GetOptionalInt("New current chapter ",
            book.CurrentChapter?.ToString() ?? "none", "Invalid. Please try again.");
        DateTime? dueDate = ConsoleHelper.GetOptionalDate("New due date (MM/DD/YYYY) ",
            book.DueDate?.ToShortDateString() ?? "none", "Invalid. Please try again.");

        return (title, author, totalPages, currentPage, totalChapters, currentChapter, dueDate);
    }

    public static DateTime PromptForDate()
    {
        Console.Clear();
        Console.WriteLine("=== Get a daily reading goal ===");
        return ConsoleHelper.GetDate("Please provide a due date for the book. (MM/DD/YYYY)",
            "Error getting date. Please try again");
    }
}
