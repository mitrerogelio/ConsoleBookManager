using ConsoleBookManager.ConsoleUi.Utilities;
using ConsoleBookManager.ConsoleUi.Views;

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

    public static DateTime PromptForDate()
    {
        Console.Clear();
        Console.WriteLine("=== Get a daily reading goal ===");
        return ConsoleHelper.GetDate("Please provide a due date for the book. (MM/DD/YYYY)",
            "Error getting date. Please try again");
    }
}
