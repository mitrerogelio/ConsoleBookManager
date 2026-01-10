using ConsoleBookManager.ConsoleUi.Menus;
using ConsoleBookManager.ConsoleUi.Utilities;
using ConsoleBookManager.ConsoleUi.Views;
using ConsoleBookManager.Models;
using ConsoleBookManager.Services;

namespace ConsoleBookManager.ConsoleUi.Controllers;

public class BookController(BookService service)
{
    private bool _running = true;

    public void Run()
    {
        while (_running)
        {
            MainMenu.Show();

            int choice = MainMenu.GetChoice();

            switch (choice)
            {
                case 1:
                    AddBook();
                    break;
                case 2:
                    ListBooks();
                    break;
                case 3:
                    UpdateBook();
                    break;
                case 4:
                    DeleteBook();
                    break;
                case 0:
                    _running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void AddBook()
    {
        (string title, string author, int totalPages) userInput = BookMenu.GetNewBookDetails();

        try
        {
            service.AddBook(userInput.title, userInput.author, userInput.totalPages);
            ConsoleHelper.DisplaySuccess($"{userInput.title} has been added to the book list.");
        }
        catch (Exception e)
        {
            ConsoleHelper.DisplayError(e.Message);
        }
    }

    private void ListBooks(IEnumerable<BookStatus>? status = null)
    {
        List<Book> books = service.GetBooks(status).ToList();
        if (books.Count == 0)
        {
            ConsoleHelper.DisplayError($"No books found.");
            return;
        }

        BookView.ShowBooks(books);
    }

    private void UpdateBook()
    {
        int bookId = ConsoleHelper.GetInt("Enter the book ID: ", "Invalid Id. Please try again.");
        try
        {
            Book book = service.GetRequiredBook(bookId);
            BookView.DisplayBookDetails(book);
        }
        catch (Exception e)
        {
            ConsoleHelper.DisplayError($"Unable to locate your book: {e.Message}");
            return;
        }

        BookUpdateOptions userChoice = BookMenu.SelectPropertyToUpdate();
        switch (userChoice)
        {
            case BookUpdateOptions.Title:
                string newTitle = ConsoleHelper.GetValidStr("Enter the title: ");
                service.UpdateTitle(bookId, newTitle);
                break;
            case BookUpdateOptions.Author:
                string newAuthor = ConsoleHelper.GetValidStr("Enter the author name: ");
                service.UpdateAuthor(bookId, newAuthor);
                break;
            case BookUpdateOptions.TotalPages:
                int newTotalPgs = ConsoleHelper.GetInt("Enter the total number of pages: ",
                    "Invalid Page Number: Please try again.");
                service.UpdateTotalPages(bookId, newTotalPgs);
                break;
            case BookUpdateOptions.CurrentPage:
                int newCurrentPg = ConsoleHelper.GetInt("Enter the current page: ", "Invalid. Please try again.");
                service.UpdateCurrentPage(bookId, newCurrentPg);
                break;
            case BookUpdateOptions.TotalChapters:
                int newTotalChp =
                    ConsoleHelper.GetInt("Enter the total amount of chapters: ", "Invalid. Please try again.");
                service.UpdateTotalChapters(bookId, newTotalChp);
                break;
            case BookUpdateOptions.CurrentChapter:
                int newCurrentChp =
                    ConsoleHelper.GetInt("Enter the current chapter: ", "Invalid. Please try again.");
                service.UpdateCurrentChapter(bookId, newCurrentChp);
                break;
            case BookUpdateOptions.DueDate:
                DateTime newDate = ConsoleHelper.GetDate("Enter the due date (MM/DD/YYYY): ", "Invalid. Please try again.");
                service.SetDueDate(bookId, newDate);
                break;
            case BookUpdateOptions.Cancel:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void DeleteBook()
    {

    }
}